using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAssembly;

/// <summary>
/// Mapping service for Extra Assembly V1 data.
/// </summary>
public class ExtraAssemblyMappingServiceV1 : ICanonicalMappingService<CanonicalExtraAssembly, CodalExtraAssemblyV1>
{
    public Task<CanonicalExtraAssembly> MapToCanonicalAsync(
        CodalExtraAssemblyV1 dto,
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

        CanonicalExtraAssembly canonical = new(
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
        canonical.ParentAssemblyInfo = new ParentAssemblyInfo
        {
            AssemblyResultType = (AssemblyResultType)dto.ParentAssembly.AssemblyResultType,
            Date = dto.ParentAssembly.Date,
            Hour = dto.ParentAssembly.Hour,
            Location = dto.ParentAssembly.Location,
            Day = dto.ParentAssembly.Day,
            LetterPublishDate = dto.ParentAssembly.LetterPublishDate,
            LetterTracingNo = dto.ParentAssembly.LetterTracingNo.HasValue && dto.ParentAssembly.LetterTracingNo.Value >= 0
                ? dto.ParentAssembly.LetterTracingNo.Value
                : null,
            SessionOrders = MapSessionOrders(dto.ParentAssembly.SessionOrders)
        };

        // Map assembly chief members (preserve JSON structure)
        if (dto.AssemblyChiefMembers != null)
        {
            canonical.AssemblyChiefMembersInfo = new AssemblyChiefMembersInfo
            {
                AssemblyChief = dto.AssemblyChiefMembers.AssemblyChief,
                AssemblySuperVisor1 = dto.AssemblyChiefMembers.AssemblySuperVisor1,
                AssemblySuperVisor2 = dto.AssemblyChiefMembers.AssemblySuperVisor2,
                AssemblySecretary = dto.AssemblyChiefMembers.AssemblySecretary
            };
        }

        // Map next session info
        if (dto.NextSession != null)
        {
            canonical.NextSessionInfo = new NextSessionInfo
            {
                BreakDesc = dto.NextSession.BreakDesc,
                Hour = dto.NextSession.Hour,
                Date = dto.NextSession.Date,
                Day = dto.NextSession.Day,
                Location = dto.NextSession.Location
            };
        }

        // Map scheduling
        if (dto.ExtraAssemblyScheduling != null)
        {
            canonical.ExtraAssemblyScheduling = new ExtraAssemblyScheduling
            {
                IsRegistered = dto.ExtraAssemblyScheduling.IsRegistered,
                YearEndToDate = dto.ExtraAssemblyScheduling.YearEndToDate
            };
        }

        // Map capital change data
        canonical.CapitalChangeState = dto.CapitalChangeState.HasValue
            ? (CapitalChangeState)dto.CapitalChangeState.Value
            : CapitalChangeState.None;
        canonical.LastShareValue = dto.LastShareValue;
        canonical.LastCapital = dto.LastCapital;
        canonical.LastShareCount = dto.LastShareCount;

        // Map decrease capital
        if (dto.ExtraAssemblyDecreaseCapital != null)
        {
            canonical.ExtraAssemblyDecreaseCapital = new ExtraAssemblyDecreaseCapital
            {
                CapitalDecreaseValue = dto.ExtraAssemblyDecreaseCapital.CapitalDecreaseValue,
                DecreasePercent = dto.ExtraAssemblyDecreaseCapital.DecreasePercent,
                IsAccept = dto.ExtraAssemblyDecreaseCapital.IsAccept ?? false,
                NewCapital = dto.ExtraAssemblyDecreaseCapital.NewCapital,
                NewShareCount = dto.ExtraAssemblyDecreaseCapital.NewShareCount,
                NewShareValue = dto.ExtraAssemblyDecreaseCapital.NewShareValue
            };
        }

        // Map share value change capital
        if (dto.ExtraAssemblyShareValueChangeCapitals != null)
        {
            canonical.ExtraAssemblyShareValueChangeCapital = new ExtraAssemblyShareValueChangeCapital
            {
                IsAccept = dto.ExtraAssemblyShareValueChangeCapitals.IsAccept ?? false,
                NewShareCount = dto.ExtraAssemblyShareValueChangeCapitals.NewShareCount,
                NewShareValue = dto.ExtraAssemblyShareValueChangeCapitals.NewShareValue
            };
        }

        // Map address change
        canonical.IsLocationChange = dto.IsLocationChange;
        canonical.OldAddress = dto.OldAddress;
        canonical.NewAddress = dto.NewAddress;

        // Map name change
        canonical.IsNameChange = dto.IsNameChange;
        canonical.OldName = dto.OldName;
        canonical.NewName = dto.NewName;

        // Map activity subject change
        canonical.IsActivitySubjectChange = dto.IsActivitySubjectChange;
        canonical.OldActivitySubject = dto.OldActivitySubject;
        canonical.NewActivitySubject = dto.NewActivitySubject;

        // Map financial year change
        canonical.IsFinancialYearChange = dto.IsFinancialYearChange;
        canonical.OldFinancialYearMonthLength = dto.OldFinancialYearMonthLength;
        canonical.OldFinancialYearEndDate = dto.OldFinancialYearEndDate;
        canonical.OldFinancialYearDayLength = dto.OldFinancialYearDayLength;
        canonical.NewFinancialYearEndDate = dto.NewFinancialYearEndDate;
        canonical.NewFinancialYearMonthLength = dto.NewFinancialYearMonthLength;
        canonical.NewFinancialYearDayLength = dto.NewFinancialYearDayLength;

        // Map clause 141 and statute approval
        canonical.IsDecidedClause141 = dto.IsDecidedClause141;
        canonical.DecidedClause141Des = dto.DecidedClause141Des;
        canonical.IsAccordWithSEOStatuteApproved = dto.IsAccordWithSEOStatuteApproved;
        canonical.CorrectionStatuteApproved = dto.CorrectionStatuteApproved;

        // Map other properties
        canonical.OtherDes = dto.OtherDes;
        canonical.PrimaryMarketTracingNo = dto.PrimaryMarketTracingNo.HasValue && dto.PrimaryMarketTracingNo.Value >= 0
            ? dto.PrimaryMarketTracingNo.Value
            : null;

        // Map collections
        canonical.ShareHolders = MapShareHolders(dto.ShareHolders);
        canonical.AssemblyBoardMembers = MapAssemblyBoardMembers(dto.AssemblyBoardMembers);
        canonical.ExtraAssemblyIncreaseCapitals = MapIncreaseCapitals(dto.ExtraAssemblyIncreaseCapitals);

        // Map attendees
        canonical.Ceo = MapAttendee(dto.Ceo);
        canonical.AuditCommitteeChairman = MapAttendee(dto.AuditCommitteeChairman);

        return Task.FromResult(canonical);
    }

    public void UpdateCanonical(CanonicalExtraAssembly existing, CanonicalExtraAssembly updated)
    {
        // Update parent assembly info
        existing.ParentAssemblyInfo = updated.ParentAssemblyInfo;

        // Update assembly chief members
        existing.AssemblyChiefMembersInfo = updated.AssemblyChiefMembersInfo;

        // Update next session info
        existing.NextSessionInfo = updated.NextSessionInfo;

        // Update scheduling
        existing.ExtraAssemblyScheduling = updated.ExtraAssemblyScheduling;

        // Update capital change data
        existing.CapitalChangeState = updated.CapitalChangeState;
        existing.LastShareValue = updated.LastShareValue;
        existing.LastCapital = updated.LastCapital;
        existing.LastShareCount = updated.LastShareCount;

        // Update decrease capital
        existing.ExtraAssemblyDecreaseCapital = updated.ExtraAssemblyDecreaseCapital;

        // Update share value change capital
        existing.ExtraAssemblyShareValueChangeCapital = updated.ExtraAssemblyShareValueChangeCapital;

        // Update address change
        existing.IsLocationChange = updated.IsLocationChange;
        existing.OldAddress = updated.OldAddress;
        existing.NewAddress = updated.NewAddress;

        // Update name change
        existing.IsNameChange = updated.IsNameChange;
        existing.OldName = updated.OldName;
        existing.NewName = updated.NewName;

        // Update activity subject change
        existing.IsActivitySubjectChange = updated.IsActivitySubjectChange;
        existing.OldActivitySubject = updated.OldActivitySubject;
        existing.NewActivitySubject = updated.NewActivitySubject;

        // Update financial year change
        existing.IsFinancialYearChange = updated.IsFinancialYearChange;
        existing.OldFinancialYearMonthLength = updated.OldFinancialYearMonthLength;
        existing.OldFinancialYearEndDate = updated.OldFinancialYearEndDate;
        existing.OldFinancialYearDayLength = updated.OldFinancialYearDayLength;
        existing.NewFinancialYearEndDate = updated.NewFinancialYearEndDate;
        existing.NewFinancialYearMonthLength = updated.NewFinancialYearMonthLength;
        existing.NewFinancialYearDayLength = updated.NewFinancialYearDayLength;

        // Update clause 141 and statute approval
        existing.IsDecidedClause141 = updated.IsDecidedClause141;
        existing.DecidedClause141Des = updated.DecidedClause141Des;
        existing.IsAccordWithSEOStatuteApproved = updated.IsAccordWithSEOStatuteApproved;
        existing.CorrectionStatuteApproved = updated.CorrectionStatuteApproved;

        // Update other properties
        existing.OtherDes = updated.OtherDes;
        existing.PrimaryMarketTracingNo = updated.PrimaryMarketTracingNo;

        // Update collections
        existing.ShareHolders = updated.ShareHolders;
        existing.AssemblyBoardMembers = updated.AssemblyBoardMembers;
        existing.ExtraAssemblyIncreaseCapitals = updated.ExtraAssemblyIncreaseCapitals;

        // Update attendees
        existing.Ceo = updated.Ceo;
        existing.AuditCommitteeChairman = updated.AuditCommitteeChairman;
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

    private List<ExtraAssemblyIncreaseCapital> MapIncreaseCapitals(List<IncreaseCapitalDto>? dtos)
    {
        if (dtos == null)
        {
            return new List<ExtraAssemblyIncreaseCapital>();
        }

        return dtos.Select(x => new ExtraAssemblyIncreaseCapital
        {
            CashIncoming = x.CashIncoming,
            RetainedEarning = x.RetainedEarning,
            Reserves = x.Reserves,
            RevaluationSurplus = x.RevaluationSurplus,
            SarfSaham = x.SarfSaham,
            IsAccept = x.IsAccept ?? false,
            CapitalIncreaseValue = x.CapitalIncreaseValue,
            IncreasePercent = x.IncreasePercent.HasValue ? (decimal?)x.IncreasePercent.Value : null,
            Type = (CapitalIncreaseApprovalType)x.Type,
            CashForceclosurePriority = x.CashForceclosurePriority,
            CashForceclosurePriorityStockPrice = x.CashForceclosurePriorityStockPrice,
            CashForceclosurePriorityStockDesc = x.CashForceclosurePriorityStockDesc,
            CashForceclosurePriorityAvalableStockCount = x.CashForceclosurePriorityAvalableStockCount,
            CashForceclosurePriorityPrizeStockCount = x.CashForceclosurePriorityPrizeStockCount
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
