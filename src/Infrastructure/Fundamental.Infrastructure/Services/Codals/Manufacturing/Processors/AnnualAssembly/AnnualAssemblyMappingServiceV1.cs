using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Dto.AnnualAssembly.V1;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.AnnualAssembly;

/// <summary>
/// Mapping service for Annual Assembly V1 data.
/// </summary>
public class AnnualAssemblyMappingServiceV1 : ICanonicalMappingService<CanonicalAnnualAssembly, CodalAnnualAssemblyV1>
{
    public Task<CanonicalAnnualAssembly> MapToCanonicalAsync(
        CodalAnnualAssemblyV1 dto,
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

        CanonicalAnnualAssembly canonical = new(
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
            LetterTracingNo = dto.ParentAssembly.LetterTracingNo.HasValue
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

    public void UpdateCanonical(CanonicalAnnualAssembly existing, CanonicalAnnualAssembly updated)
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

    private static DateTime? ParsePersianDate(string? persianDate)
    {
        if (string.IsNullOrWhiteSpace(persianDate))
        {
            return null;
        }

        try
        {
            return persianDate.ToGregorianDateTime();
        }
        catch
        {
            return null;
        }
    }

    private static decimal? ParseDecimal(long? value)
    {
        return value.HasValue ? value.Value : null;
    }

    private static VerificationStatus ParseVerificationStatus(int value)
    {
        return value switch
        {
            1 => VerificationStatus.Verified,
            2 => VerificationStatus.InProgress,
            _ => VerificationStatus.Unspecified
        };
    }

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

    private List<SessionOrder> MapSessionOrders(List<SessionOrderDto>? dtos)
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

    private List<ShareHolder> MapShareHolders(List<ShareHolderDto>? dtos)
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

    private List<AssemblyBoardMember> MapAssemblyBoardMembers(List<AssemblyBoardMemberDto>? dtos)
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

    private List<Inspector> MapInspectors(List<InspectorDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<Inspector>();
        }

        return dtos
            .Select(x => new Inspector(x.Serial, x.Name, (InspectorType)x.Type))
            .ToList();
    }

    private List<NewBoardMember> MapNewBoardMembers(List<NewBoardMemberDto>? dtos)
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

    private List<BoardMemberWageAndGift> MapWageAndGifts(List<WageAndGiftDto>? dtos)
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

    private List<NewsPaper> MapNewsPapers(List<NewsPaperDto>? dtos)
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

    private List<AssemblyInterim> MapInterims(List<InterimDto>? dtos)
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

    private List<ProportionedRetainedEarning> MapRetainedEarnings(List<RetainedEarningDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<ProportionedRetainedEarning>();
        }

        return dtos.Select(x => new ProportionedRetainedEarning
        {
            FieldName = !string.IsNullOrWhiteSpace(x.FieldName) && Enum.TryParse<ProportionedRetainedEarningFieldName>(x.FieldName, out ProportionedRetainedEarningFieldName fieldName)
                ? fieldName
                : null,
            Description = x.Description,
            YearEndToDateValue = ParseDecimal(x.YearEndToDateValue),
            RowClass = x.RowClass
        }).ToList();
    }

    private AssemblyAttendee? MapAttendee(AttendeeDto? dto)
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