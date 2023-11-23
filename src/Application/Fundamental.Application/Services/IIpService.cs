using Fundamental.Domain.ValueObjects;

namespace Fundamental.Application.Services;

public interface IIpService
{
    string? GetRawRemoteIpAddress();
    IpAddress GetRemoteIpAddress();
}