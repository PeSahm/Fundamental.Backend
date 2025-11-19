using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblyById;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

#pragma warning disable SA1118 // Parameter should not span multiple lines
public sealed class ExtraAssemblyDetailItemSpec : Specification<CanonicalExtraAssembly, GetExtraAssemblyDetailItem>
{
    public ExtraAssemblyDetailItemSpec()
    {
        Query
            .AsNoTracking()
            .AsSplitQuery()
            .Select(x => new GetExtraAssemblyDetailItem(
                x.Id,
                x.Symbol.Isin,
                x.Symbol.Name,
                x.Symbol.Title,
                x.HtmlUrl.ToString(),
                x.Version,
                x.FiscalYear.Year,
                x.YearEndMonth.Month,
                x.ReportMonth.Month,
                x.AssemblyDate,
                x.TraceNo,
                x.PublishDate,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.AssemblyResultType : default,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.AssemblyResultTypeTitle : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.Date : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.Hour : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.Location : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.Day : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.LetterPublishDate : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.LetterTracingNo : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblyChief : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblySuperVisor1 : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblySuperVisor2 : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblySecretary : null,
                x.CapitalChangeState,
                x.LastShareValue,
                x.LastCapital,
                x.LastShareCount,
                x.OldAddress,
                x.NewAddress,
                x.OldName,
                x.NewName,
                x.OldActivitySubject,
                x.NewActivitySubject,
                x.OldFinancialYearMonthLength,
                x.OldFinancialYearEndDate,
                x.OldFinancialYearDayLength,
                x.NewFinancialYearEndDate,
                x.NewFinancialYearMonthLength,
                x.NewFinancialYearDayLength,
                x.IsLocationChange,
                x.IsNameChange,
                x.IsActivitySubjectChange,
                x.IsFinancialYearChange,
                x.IsDecidedClause141,
                x.DecidedClause141Des,
                x.IsAccordWithSEOStatuteApproved,
                x.OtherDes,
                x.PrimaryMarketTracingNo,
                x.CorrectionStatuteApproved,
                x.NextSessionInfo != null ? x.NextSessionInfo.BreakDesc : null,
                x.NextSessionInfo != null ? x.NextSessionInfo.Hour : null,
                x.NextSessionInfo != null ? x.NextSessionInfo.Date : null,
                x.NextSessionInfo != null ? x.NextSessionInfo.Day : null,
                x.NextSessionInfo != null ? x.NextSessionInfo.Location : null,
                x.ExtraAssemblyScheduling != null ? x.ExtraAssemblyScheduling.IsRegistered : null,
                x.ExtraAssemblyScheduling != null ? x.ExtraAssemblyScheduling.YearEndToDate : null,
                x.ExtraAssemblyDecreaseCapital != null
                    ? new ExtraAssemblyDecreaseCapitalQueryDto(
                        x.ExtraAssemblyDecreaseCapital.CapitalDecreaseValue,
                        x.ExtraAssemblyDecreaseCapital.DecreasePercent,
                        x.ExtraAssemblyDecreaseCapital.IsAccept,
                        x.ExtraAssemblyDecreaseCapital.NewCapital,
                        x.ExtraAssemblyDecreaseCapital.NewShareCount,
                        x.ExtraAssemblyDecreaseCapital.NewShareValue
                    )
                    : null,
                x.ExtraAssemblyShareValueChangeCapital != null
                    ? new ExtraAssemblyShareValueChangeCapitalQueryDto(
                        x.ExtraAssemblyShareValueChangeCapital.IsAccept,
                        x.ExtraAssemblyShareValueChangeCapital.NewShareCount,
                        x.ExtraAssemblyShareValueChangeCapital.NewShareValue
                    )
                    : null,
                x.ParentAssemblyInfo != null && x.ParentAssemblyInfo.SessionOrders != null
                    ? x.ParentAssemblyInfo.SessionOrders.Select(item => new SessionOrderQueryDto(
                        item.Type,
                        item.Description,
                        item.FieldName
                    )).ToList()
                    : new List<SessionOrderQueryDto>(),
                x.ShareHolders.Select(item => new ShareHolderQueryDto(
                    item.ShareHolderSerial,
                    item.Name,
                    item.ShareCount,
                    item.SharePercent
                )).ToList(),
                x.AssemblyBoardMembers.Select(item => new AssemblyBoardMemberQueryDto(
                    item.BoardMemberSerial,
                    item.FullName,
                    item.NationalCode,
                    item.LegalType,
                    item.MembershipType,
                    item.AgentBoardMemberFullName,
                    item.AgentBoardMemberNationalCode,
                    item.Position,
                    item.HasDuty,
                    item.Degree,
                    item.DegreeRef,
                    item.EducationField,
                    item.EducationFieldRef,
                    item.AttendingMeeting,
                    item.Verification
                )).ToList(),
                x.ExtraAssemblyIncreaseCapitals.Select(item => new ExtraAssemblyIncreaseCapitalQueryDto(
                    item.CashIncoming,
                    item.RetainedEarning,
                    item.Reserves,
                    item.RevaluationSurplus,
                    item.SarfSaham,
                    item.IsAccept,
                    item.CapitalIncreaseValue,
                    item.IncreasePercent,
                    item.Type,
                    item.CashForceclosurePriorityStockPrice,
                    item.CashForceclosurePriorityStockDesc,
                    item.CashForceclosurePriorityAvalableStockCount,
                    item.CashForceclosurePriorityPrizeStockCount,
                    item.CashForceclosurePriority
                )).ToList(),
                x.Ceo != null
                    ? new AssemblyAttendeeQueryDto(
                        x.Ceo.FullName,
                        x.Ceo.NationalCode,
                        x.Ceo.AttendingMeeting,
                        x.Ceo.Degree,
                        x.Ceo.DegreeRef,
                        x.Ceo.EducationField,
                        x.Ceo.EducationFieldRef,
                        x.Ceo.Verification
                    )
                    : null,
                x.AuditCommitteeChairman != null
                    ? new AssemblyAttendeeQueryDto(
                        x.AuditCommitteeChairman.FullName,
                        x.AuditCommitteeChairman.NationalCode,
                        x.AuditCommitteeChairman.AttendingMeeting,
                        x.AuditCommitteeChairman.Degree,
                        x.AuditCommitteeChairman.DegreeRef,
                        x.AuditCommitteeChairman.EducationField,
                        x.AuditCommitteeChairman.EducationFieldRef,
                        x.AuditCommitteeChairman.Verification
                    )
                    : null
            ));
    }

    public ExtraAssemblyDetailItemSpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }
}
