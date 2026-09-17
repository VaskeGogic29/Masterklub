namespace MasterklubAPI.Middleware;

public class CachedIdempotentResponse
{
    public int StatusCode { get; set; }
    public string Body { get; set; } = string.Empty;
    public string? ContentType { get; set; }
}
