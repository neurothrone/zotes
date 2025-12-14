namespace Zotes.Api.Config;

public static class ApiVersioning
{
    // used as the Swagger doc/group name and in route grouping (e.g. "v1")
    public const string DocName = "v1";

    // semantic version shown in OpenAPI Info (e.g. "1.0")
    public const string SemanticName = "1.0.0";

    public const string Prefix = "/api";
    public static string RoutePrefix => $"{Prefix}/{DocName}";
}