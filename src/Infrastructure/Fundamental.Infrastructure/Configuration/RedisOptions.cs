namespace Fundamental.Infrastructure.Configuration;

public class RedisOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int DefaultDatabase { get; set; }
    public int ConnectTimeout { get; set; }
    public int ConnectRetry { get; set; }
    public int KeepAlive { get; set; }
    public bool AbortOnConnectFail { get; set; }
    public int ConfigCheckSeconds { get; set; }
}