using Fundamental.Application.Services;
using Fundamental.Domain.Common.Exceptions;
using Fundamental.Domain.Common.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Fundamental.Infrastructure.Services;

public class IpService : IIpService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IpService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetRawRemoteIpAddress()
    {
        string? ip = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(ip))
        {
            ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        }

        return ip;
    }

    public IpAddress GetRemoteIpAddress()
    {
        string? ip = GetRawRemoteIpAddress();
        IpAddress? ipAddress = null;
        bool isValidIp = !string.IsNullOrWhiteSpace(ip) && IpAddress.TryCreate(ip, out ipAddress);

        if (!isValidIp)
        {
            throw new InvalidIpAddressException(ip);
        }

        return ipAddress!;
    }
}