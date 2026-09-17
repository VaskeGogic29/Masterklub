namespace MasterklubAPI.Middleware;

[AttributeUsage(AttributeTargets.Method)]
public class RequireIdempotencyKeyAttribute : Attribute
{
}
