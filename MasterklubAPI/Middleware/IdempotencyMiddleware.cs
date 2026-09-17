using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace MasterklubAPI.Middleware;

public class IdempotencyMiddleware
{
    private const string ZaglavljeKljuca = "Idempotency-Key";
    private static readonly TimeSpan TrajanjeKesiranja = TimeSpan.FromHours(24);

    private readonly RequestDelegate _next;
    private readonly IMemoryCache _cache;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _bravePoKljucu = new();

    public IdempotencyMiddleware(RequestDelegate next, IMemoryCache cache)
    {
        _next = next;
        _cache = cache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!HttpMethods.IsPost(context.Request.Method))
        {
            await _next(context);
            return;
        }

        var idempotencyKey = context.Request.Headers[ZaglavljeKljuca].FirstOrDefault();
        var zahtevaKljuc = context.GetEndpoint()?.Metadata.GetMetadata<RequireIdempotencyKeyAttribute>() != null;

        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            if (zahtevaKljuc)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { poruka = $"{ZaglavljeKljuca} header je obavezan za ovu operaciju." });
                return;
            }

            await _next(context);
            return;
        }

        var kesKljuc = $"idempotency:{context.Request.Path}:{idempotencyKey}";
        var brava = _bravePoKljucu.GetOrAdd(kesKljuc, _ => new SemaphoreSlim(1, 1));

        await brava.WaitAsync();
        try
        {
            if (_cache.TryGetValue(kesKljuc, out CachedIdempotentResponse? kesiranOdgovor) && kesiranOdgovor != null)
            {
                context.Response.StatusCode = kesiranOdgovor.StatusCode;
                context.Response.ContentType = kesiranOdgovor.ContentType;
                context.Response.Headers["Idempotency-Replayed"] = "true";
                await context.Response.WriteAsync(kesiranOdgovor.Body);
                return;
            }

            var originalniStream = context.Response.Body;
            await using var buffer = new MemoryStream();
            context.Response.Body = buffer;

            await _next(context);

            buffer.Seek(0, SeekOrigin.Begin);
            var telo = await new StreamReader(buffer, Encoding.UTF8).ReadToEndAsync();

            context.Response.Body = originalniStream;
            buffer.Seek(0, SeekOrigin.Begin);
            await buffer.CopyToAsync(originalniStream);

            if (context.Response.StatusCode < 500)
            {
                _cache.Set(kesKljuc, new CachedIdempotentResponse
                {
                    StatusCode = context.Response.StatusCode,
                    Body = telo,
                    ContentType = context.Response.ContentType
                }, TrajanjeKesiranja);
            }
        }
        finally
        {
            brava.Release();
        }
    }
}
