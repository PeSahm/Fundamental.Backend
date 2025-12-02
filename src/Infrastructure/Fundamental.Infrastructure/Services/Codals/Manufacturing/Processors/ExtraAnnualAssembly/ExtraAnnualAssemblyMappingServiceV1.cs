using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Dto.ExtraAnnualAssembly.V1;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using Microsoft.Extensions.Logging;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAnnualAssembly;

/// <summary>
/// Mapping service for Extraordinary Annual Assembly V1 data.
/// Maps to dedicated CanonicalExtraAnnualAssembly entity.
/// </summary>
public class ExtraAnnualAssemblyMappingServiceV1 : ICanonicalMappingService<CanonicalExtraAnnualAssembly, CodalExtraAnnualAssemblyV1>
{
    private readonly ILogger<ExtraAnnualAssemblyMappingServiceV1> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtraAnnualAssemblyMappingServiceV1"/> class.
    /// </summary>
    /// <param name="logger">The logger instance for structured logging.</param>
    public ExtraAnnualAssemblyMappingServiceV1(ILogger<ExtraAnnualAssemblyMappingServiceV1> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Maps a CodalExtraAnnualAssemblyV1 DTO and related statement metadata into a CanonicalExtraAnnualAssembly using the V1 mapping rules.
    /// </summary>
    /// <param name="dto">The Codal DTO containing assembly data to map; must include ParentAssembly.</param>
    /// <param name="symbol">The symbol to assign to the resulting canonical entity.</param>
    /// <param name="statement">Statement metadata (used for publish date, tracing number, and fallback date information).</param>
    /// <returns>A CanonicalExtraAnnualAssembly populated from the DTO and statement.</returns>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="dto"/>.ParentAssembly is null.</exception>
    public Task<CanonicalExtraAnnualAssembly> MapToCanonicalAsync(
        CodalExtraAnnualAssemblyV1 dto,
        Symbol symbol,
        GetStatementResponse statement
    )
    {
        if (dto.ParentAssembly == null)
        {
            throw new InvalidOperationException("Parent assembly data is required");
        }

        // Extract fiscal year and month from parent assembly Date (Persian date string)
        DateTime? parsedDate = ParsePersianDate(dto.ParentAssembly.Date);
        ushort fiscalYear;
        ushort yearEndMonth = 12; // Default to month 12 (end of Persian year)
        ushort reportMonth;

        if (parsedDate.HasValue)
        {
            fiscalYear = (ushort)parsedDate.Value.GetPersianYear();
            reportMonth = (ushort)parsedDate.Value.GetPersianMonth();
        }
        else
        {
            // Fallback to publish date if assembly date parsing fails
            fiscalYear = (ushort)statement.PublishDateMiladi.GetPersianYear();
            reportMonth = (ushort)statement.PublishDateMiladi.GetPersianMonth();
        }

        // Parse assembly date for storage
        DateTime assemblyDate = parsedDate ?? statement.PublishDateMiladi;

        // Validate HtmlUrl before Uri construction
        if (string.IsNullOrWhiteSpace(statement.HtmlUrl))
        {
            throw new InvalidOperationException(
                $"Statement HtmlUrl is required for TracingNo {statement.TracingNo}"
            );
        }

        CanonicalExtraAnnualAssembly canonical = new(
            Guid.NewGuid(),
            symbol,
            statement.TracingNo,
            new Uri(statement.HtmlUrl),
            new FiscalYear(fiscalYear),
            new StatementMonth(yearEndMonth),
            new StatementMonth(reportMonth),
            assemblyDate.ToUniversalTime(),
            statement.PublishDateMiladi.ToUniversalTime(),
            "V1"
        );

        // Map parent assembly info (preserve JSON structure)
        canonical.ParentAssemblyInfo = new ParentAssembly
        {
            AssemblyResultType = (AssemblyResultType)dto.ParentAssembly.AssemblyResultType,
            AssemblyResultTypeTitle = dto.ParentAssembly.AssemblyResultTypeTitle,
            Hour = dto.ParentAssembly.Hour,
            Location = dto.ParentAssembly.Location,
            Day = dto.ParentAssembly.Day,
            LetterTracingNo = dto.ParentAssembly.LetterTracingNo.HasValue && dto.ParentAssembly.LetterTracingNo.Value >= 0
                ? (ulong)dto.ParentAssembly.LetterTracingNo.Value
                : null,
            SessionOrders = MapSessionOrders(dto.ParentAssembly.SessionOrders)
        };

        // Map assembly chief members (preserve JSON structure)
        if (dto.AssemblyChiefMembers != null)
        {
            canonical.AssemblyChiefMembersInfo = new AssemblyChiefMembers
            {
                AssemblyChief = dto.AssemblyChiefMembers.AssemblyChief,
                AssemblySuperVisor1 = dto.AssemblyChiefMembers.AssemblySuperVisor1,
                AssemblySuperVisor2 = dto.AssemblyChiefMembers.AssemblySuperVisor2,
                AssemblySecretary = dto.AssemblyChiefMembers.AssemblySecretary
            };
        }

        // Map other properties
        canonical.BoardMemberPeriod = dto.BoardMemberPeriod;
        canonical.PublishSecurityDescription = dto.PublishSecurityDescription;
        canonical.OtherDescription = dto.OtherDescription;
        canonical.NewHour = dto.NewHour;
        canonical.NewDay = dto.NewDay;
        canonical.NewDate = dto.NewDate;
        canonical.NewLocation = dto.NewLocation;
        canonical.BreakDescription = dto.BreakDescription;

        // Map collections
        canonical.ShareHolders = MapShareHolders(dto.ShareHolders);
        canonical.AssemblyBoardMembers = MapAssemblyBoardMembers(dto.AssemblyBoardMembers);
        canonical.Inspectors = MapInspectors(dto.Inspectors);
        canonical.NewBoardMembers = MapNewBoardMembers(dto.NewBoardMembers);
        canonical.BoardMemberWageAndGifts = MapWageAndGifts(dto.BoardMemberWageAndGifts);
        canonical.NewsPapers = MapNewsPapers(dto.NewsPapers);
        canonical.AssemblyInterims = MapInterims(dto.AssemblyInterims);
        canonical.ProportionedRetainedEarnings = MapRetainedEarnings(dto.AssemblyProportionedRetainedEarning);

        // Map attendees
        canonical.Ceo = MapAttendee(dto.Ceo);
        canonical.AuditCommitteeChairman = MapAttendee(dto.AuditCommitteeChairman);
        canonical.IndependentAuditorRepresentative = MapAttendee(dto.IndependentAuditorRepresentative);
        canonical.TopFinancialPosition = MapAttendee(dto.TopFinancialPosition);

        return Task.FromResult(canonical);
    }

    /// <summary>
    /// Apply values from an updated canonical extra-annual assembly to an existing instance.
    /// </summary>
    /// <param name="existing">The canonical instance to be updated in-place.</param>
    /// <param name="updated">The canonical instance containing new values to copy into <paramref name="existing"/>.</param>
    public void UpdateCanonical(CanonicalExtraAnnualAssembly existing, CanonicalExtraAnnualAssembly updated)
    {
        // Update parent assembly info
        existing.ParentAssemblyInfo = updated.ParentAssemblyInfo;

        // Update assembly chief members
        existing.AssemblyChiefMembersInfo = updated.AssemblyChiefMembersInfo;

        // Update other properties
        existing.BoardMemberPeriod = updated.BoardMemberPeriod;
        existing.PublishSecurityDescription = updated.PublishSecurityDescription;
        existing.OtherDescription = updated.OtherDescription;
        existing.NewHour = updated.NewHour;
        existing.NewDay = updated.NewDay;
        existing.NewDate = updated.NewDate;
        existing.NewLocation = updated.NewLocation;
        existing.BreakDescription = updated.BreakDescription;

        // Update collections
        existing.ShareHolders = updated.ShareHolders;
        existing.AssemblyBoardMembers = updated.AssemblyBoardMembers;
        existing.Inspectors = updated.Inspectors;
        existing.NewBoardMembers = updated.NewBoardMembers;
        existing.BoardMemberWageAndGifts = updated.BoardMemberWageAndGifts;
        existing.NewsPapers = updated.NewsPapers;
        existing.AssemblyInterims = updated.AssemblyInterims;
        existing.ProportionedRetainedEarnings = updated.ProportionedRetainedEarnings;

        // Update nullable owned entities
        existing.Ceo = updated.Ceo;
        existing.AuditCommitteeChairman = updated.AuditCommitteeChairman;
        existing.IndependentAuditorRepresentative = updated.IndependentAuditorRepresentative;
        existing.TopFinancialPosition = updated.TopFinancialPosition;
    }

    /// <summary>
    /// Maps a sequence of InspectorDto objects to a list of Inspector domain instances.
    /// </summary>
    /// <param name="dtos">The source list of InspectorDto objects; may be null.</param>
    /// <returns>A list of Inspector mapped from <paramref name="dtos"/>; an empty list if <paramref name="dtos"/> is null.</returns>
    private static List<Inspector> MapInspectors(List<Application.Codals.Dto.AnnualAssembly.V1.InspectorDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<Inspector>();
        }

        return dtos
            .Select(x => new Inspector(x.Serial, x.Name, (InspectorType)x.Type))
            .ToList();
    }

    /// <summary>
    /// Converts a nullable long to a nullable decimal.
    /// </summary>
    /// <param name="value">The nullable long value to convert.</param>
    /// <returns>The converted decimal when <paramref name="value"/> has a value; otherwise null.</returns>
    private static decimal? ParseDecimal(long? value)
    {
        return value.HasValue ? value.Value : null;
    }

    /// <summary>
    /// Maps an integer verification code to the corresponding VerificationStatus enum.
    /// </summary>
    /// <param name="value">Integer code representing verification status (1 = Verified, 2 = InProgress, other values = Unspecified).</param>
    /// <returns>`VerificationStatus.Verified` for 1, `VerificationStatus.InProgress` for 2, `VerificationStatus.Unspecified` otherwise.</returns>
    private static VerificationStatus ParseVerificationStatus(int value)
    {
        return value switch
        {
            1 => VerificationStatus.Verified,
            2 => VerificationStatus.InProgress,
            _ => VerificationStatus.Unspecified
        };
    }

    /// <summary>
    /// Converts a verification code represented as a string into a corresponding <see cref="VerificationStatus"/> value.
    /// </summary>
    /// <param name="value">The verification code string: "1" for verified, "2" for in-progress; may be null or whitespace.</param>
    /// <returns>
    /// `null` if <paramref name="value"/> is null or whitespace; <see cref="VerificationStatus.Verified"/> for "1";
    /// <see cref="VerificationStatus.InProgress"/> for "2"; <see cref="VerificationStatus.Unspecified"/> for any other value.
    /// </returns>
    private static VerificationStatus? ParseVerificationString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        return value switch
        {
            "1" => VerificationStatus.Verified,
            "2" => VerificationStatus.InProgress,
            _ => VerificationStatus.Unspecified
        };
    }

    /// <summary>
    /// Parses a Persian date string and converts it to the equivalent Gregorian <see cref="DateTime"/>.
    /// </summary>
    /// <param name="persianDate">The Persian date string to parse.</param>
    /// <returns>The converted Gregorian <see cref="DateTime"/>, or <c>null</c> if the input is null, whitespace, or cannot be parsed.</returns>
    private DateTime? ParsePersianDate(string? persianDate)
    {
        if (string.IsNullOrWhiteSpace(persianDate))
        {
            return null;
        }

        try
        {
            return persianDate.ToGregorianDateTime();
        }
        catch (FormatException ex)
        {
            _logger.LogError(ex, "ParsePersianDate failed for '{PersianDate}'", persianDate);
            return null;
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "ParsePersianDate failed for '{PersianDate}'", persianDate);
            return null;
        }
    }

    /// <summary>
    /// Maps a collection of SessionOrderDto objects to a list of SessionOrder domain objects.
    /// </summary>
    /// <param name="dtos">The DTOs to map; may be null.</param>
    /// <returns>A list of mapped <see cref="SessionOrder"/> instances; empty if <paramref name="dtos"/> is null.</returns>
    private List<SessionOrder> MapSessionOrders(List<Application.Codals.Dto.AnnualAssembly.V1.SessionOrderDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<SessionOrder>();
        }

        return dtos.Select(x => new SessionOrder
        {
            Type = (SessionOrderType)x.Type,
            Description = x.Description,
            FieldName = x.FieldName
        }).ToList();
    }

    /// <summary>
    /// Maps a sequence of ShareHolderDto objects into ShareHolder domain objects.
    /// </summary>
    /// <param name="dtos">The DTOs to map; may be null.</param>
    /// <returns>A list of ShareHolder instances corresponding to the input DTOs; an empty list if <paramref name="dtos"/> is null.</returns>
    private List<ShareHolder> MapShareHolders(List<Application.Codals.Dto.AnnualAssembly.V1.ShareHolderDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<ShareHolder>();
        }

        return dtos.Select(x => new ShareHolder
        {
            ShareHolderSerial = x.ShareHolderSerial,
            Name = x.Name,
            ShareCount = x.ShareCount,
            SharePercent = x.SharePercent
        }).ToList();
    }

    /// <summary>
    /// Convert a sequence of assembly board member DTOs into domain AssemblyBoardMember instances.
    /// </summary>
    /// <param name="dtos">The source DTO list to map; may be null.</param>
    /// <returns>A list of mapped <see cref="AssemblyBoardMember"/> objects; empty if <paramref name="dtos"/> is null or contains no items.</returns>
    private List<AssemblyBoardMember> MapAssemblyBoardMembers(List<Application.Codals.Dto.AnnualAssembly.V1.AssemblyBoardMemberDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<AssemblyBoardMember>();
        }

        return dtos.Select(x => new AssemblyBoardMember
        {
            BoardMemberSerial = x.BoardMemberSerial,
            FullName = x.FullName,
            NationalCode = x.NationalCode,
            LegalType = x.LegalType.HasValue ? (LegalCompanyType)x.LegalType.Value : null,
            MembershipType = (BoardMembershipType)x.MembershipType,
            AgentBoardMemberFullName = x.AgentBoardMemberFullName,
            AgentBoardMemberNationalCode = x.AgentBoardMemberNationalCode,
            Position = (BoardPosition)x.Position,
            HasDuty = x.HasDuty == 1,
            Degree = x.Degree,
            DegreeRef = x.DegreeRef,
            EducationField = x.EducationField,
            EducationFieldRef = x.EducationFieldRef,
            AttendingMeeting = x.AttendingMeeting,
            Verification = ParseVerificationStatus(x.Verification)
        }).ToList();
    }

    /// <summary>
    /// Map a list of DTOs into domain NewBoardMember objects.
    /// </summary>
    /// <param name="dtos">Source DTO list to map; may be null.</param>
    /// <returns>A list of NewBoardMember instances; empty when <paramref name="dtos"/> is null. LegalType is converted to a nullable <see cref="LegalCompanyType"/>, and MembershipType is converted to <see cref="BoardMembershipType"/>.</returns>
    private List<NewBoardMember> MapNewBoardMembers(List<Application.Codals.Dto.AnnualAssembly.V1.NewBoardMemberDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<NewBoardMember>();
        }

        return dtos.Select(x => new NewBoardMember
        {
            Name = x.Name,
            IsLegal = x.IsLegal,
            NationalCode = x.NationalCode,
            BoardMemberSerial = x.BoardMemberSerial,
            LegalType = x.LegalType.HasValue ? (LegalCompanyType)x.LegalType.Value : null,
            MembershipType = (BoardMembershipType)x.MembershipType
        }).ToList();
    }

    /// <summary>
    /// Maps wage-and-gift DTOs to domain BoardMemberWageAndGift objects.
    /// </summary>
    /// <param name="dtos">Sequence of wage-and-gift DTOs to map; may be null.</param>
    /// <returns>A list of mapped BoardMemberWageAndGift instances; empty if <paramref name="dtos"/> is null.</returns>
    private List<BoardMemberWageAndGift> MapWageAndGifts(List<Application.Codals.Dto.AnnualAssembly.V1.WageAndGiftDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<BoardMemberWageAndGift>();
        }

        return dtos.Select(x => new BoardMemberWageAndGift
        {
            Type = (WageAndGiftFieldType)x.Type,
            FieldName = x.FieldName,
            CurrentYearValue = ParseDecimal(x.CurrentYearValue),
            PastYearValue = ParseDecimal(x.PastYearValue),
            Description = x.Description
        }).ToList();
    }

    /// <summary>
    /// Maps a collection of NewsPaperDto objects to domain NewsPaper instances.
    /// </summary>
    /// <param name="dtos">The source DTOs to map; may be null.</param>
    /// <returns>A list of NewsPaper objects corresponding to the input DTOs; empty if <paramref name="dtos"/> is null.</returns>
    private List<NewsPaper> MapNewsPapers(List<Application.Codals.Dto.AnnualAssembly.V1.NewsPaperDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<NewsPaper>();
        }

        return dtos.Select(x => new NewsPaper
        {
            NewsPaperId = x.NewsPaperId,
            Name = x.Name
        }).ToList();
    }

    /// <summary>
    /// Maps a collection of Codal interim DTOs to domain AssemblyInterim objects.
    /// </summary>
    /// <param name="dtos">The list of InterimDto instances to map; may be null.</param>
    /// <returns>A list of mapped AssemblyInterim objects. Returns an empty list if <paramref name="dtos"/> is null.</returns>
    private List<AssemblyInterim> MapInterims(List<Application.Codals.Dto.AnnualAssembly.V1.InterimDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<AssemblyInterim>();
        }

        return dtos.Select(x => new AssemblyInterim
        {
            FieldName = x.FieldName,
            Description = x.Description,
            YearEndToDateValue = ParseDecimal(x.YearEndToDateValue),
            Percent = x.Percent,
            ChangesReason = x.ChangesReason,
            RowClass = x.RowClass
        }).ToList();
    }

    /// <summary>
    /// Maps a list of retained-earnings DTOs into domain ProportionedRetainedEarning instances.
    /// </summary>
    /// <param name="dtos">The source DTOs; may be null.</param>
    /// <returns>
    /// A list of ProportionedRetainedEarning objects. Returns an empty list if <paramref name="dtos"/> is null.
    /// Each item's <see cref="ProportionedRetainedEarning.FieldName"/> is set by parsing the DTO string to the corresponding
    /// <see cref="ProportionedRetainedEarningFieldName"/> value; if parsing fails or the string is empty, the field is null.
    /// Numeric values are converted via the service's decimal parsing helper.
    /// </returns>
    private List<ProportionedRetainedEarning> MapRetainedEarnings(List<Application.Codals.Dto.AnnualAssembly.V1.RetainedEarningDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<ProportionedRetainedEarning>();
        }

        return dtos.Select(x => new ProportionedRetainedEarning
        {
            FieldName = !string.IsNullOrWhiteSpace(x.FieldName) &&
                        Enum.TryParse<ProportionedRetainedEarningFieldName>(x.FieldName, out ProportionedRetainedEarningFieldName fieldName)
                ? fieldName
                : null,
            Description = x.Description,
            YearEndToDateValue = ParseDecimal(x.YearEndToDateValue),
            RowClass = x.RowClass
        }).ToList();
    }

    /// <summary>
    /// Maps an AttendeeDto to an AssemblyAttendee instance; returns null when the input is null.
    /// </summary>
    /// <param name="dto">The attendee DTO to map.</param>
    /// <returns>The mapped AssemblyAttendee, or null if <paramref name="dto"/> is null. AttendingMeeting is set to true when dto.AttendingMeeting equals 1; Verification is derived from dto.Verification.</returns>
    private AssemblyAttendee? MapAttendee(Application.Codals.Dto.AnnualAssembly.V1.AttendeeDto? dto)
    {
        if (dto == null)
        {
            return null;
        }

        return new AssemblyAttendee
        {
            FullName = dto.FullName,
            NationalCode = dto.NationalCode,
            AttendingMeeting = dto.AttendingMeeting == 1,
            Degree = dto.Degree,
            DegreeRef = dto.DegreeRef,
            EducationField = dto.EducationField,
            EducationFieldRef = dto.EducationFieldRef,
            Verification = ParseVerificationString(dto.Verification)
        };
    }
}