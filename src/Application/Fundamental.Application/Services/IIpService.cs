using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Application.Services;

public interface IIpService
{
    string? GetRawRemoteIpAddress();
    IpAddress GetRemoteIpAddress();
}