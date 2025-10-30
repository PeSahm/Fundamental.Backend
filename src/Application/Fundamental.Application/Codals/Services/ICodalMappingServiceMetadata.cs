using Fundamental.Application.Codals.Enums;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Services;

/// <summary>
/// Interface for DTOs that provide metadata about their mapping requirements.
/// DTOs implementing this interface are self-describing and can be used to
/// resolve the appropriate mapping services.
/// </summary>
public interface ICodalMappingServiceMetadata
{
    /// <summary>
    /// Gets the reporting type for this DTO.
    /// </summary>
    ReportingType ReportingType { get; }

    /// <summary>
    /// Gets the letter type for this DTO.
    /// </summary>
    LetterType LetterType { get; }

    /// <summary>
    /// Gets the CODAL version for this DTO.
    /// </summary>
    CodalVersion CodalVersion { get; }

    /// <summary>
    /// Gets the letter part for this DTO.
    /// </summary>
    LetterPart LetterPart { get; }
}