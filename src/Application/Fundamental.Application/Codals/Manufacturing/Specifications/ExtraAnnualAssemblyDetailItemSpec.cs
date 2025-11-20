using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblyById;
using Fundamental.Application.Codals.Manufacturing.Queries.Shared;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

#pragma warning disable SA1118 // Parameter should not span multiple lines
public sealed class ExtraAnnualAssemblyDetailItemSpec : Specification<CanonicalExtraAnnualAssembly, GetExtraAnnualAssemblyDetailItem>
{
    /// <summary>
    /// Configures the specification to project a CanonicalExtraAnnualAssembly and its related data into a GetExtraAnnualAssemblyDetailItem DTO.
    /// </summary>
    /// <remarks>
    /// The constructor sets up a no-tracking, split-query projection that maps the entity's scalar properties, related symbol and fiscal fields, optional parent-assembly and chief-member info, various related collections (session orders, shareholders, board members, inspectors, new board members, wages/gifts, newspapers, interims, and proportioned retained earnings), and optional attendee records (CEO, audit committee chairman, independent auditor representative, top financial position) into the corresponding DTO structure.
    /// </remarks>
    public ExtraAnnualAssemblyDetailItemSpec()
    {
        Query
            .AsNoTracking()
            .AsSplitQuery()
            .Select(x => new GetExtraAnnualAssemblyDetailItem(
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
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.Hour : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.Location : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.Day : null,
                x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.LetterTracingNo : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblyChief : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblySuperVisor1 : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblySuperVisor2 : null,
                x.AssemblyChiefMembersInfo != null ? x.AssemblyChiefMembersInfo.AssemblySecretary : null,
                x.BoardMemberPeriod == null ? null : x.BoardMemberPeriod.ToString(),
                x.PublishSecurityDescription,
                x.OtherDescription,
                x.NewHour,
                x.NewDay,
                x.NewDate,
                x.NewLocation,
                x.BreakDescription,
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
                x.Inspectors.Select(item => new InspectorQueryDto(
                    item.Serial,
                    item.Name,
                    item.Type
                )).ToList(),
                x.NewBoardMembers.Select(item => new NewBoardMemberQueryDto(
                    item.Name,
                    item.IsLegal,
                    item.NationalCode,
                    item.BoardMemberSerial,
                    item.LegalType,
                    item.MembershipType
                )).ToList(),
                x.BoardMemberWageAndGifts.Select(item => new BoardMemberWageAndGiftQueryDto(
                    item.Type,
                    item.FieldName,
                    item.CurrentYearValue,
                    item.PastYearValue,
                    item.Description
                )).ToList(),
                x.NewsPapers.Select(item => new NewsPaperQueryDto(
                    item.NewsPaperId,
                    item.Name
                )).ToList(),
                x.AssemblyInterims.Select(item => new AssemblyInterimQueryDto(
                    item.FieldName,
                    item.Description,
                    item.YearEndToDateValue,
                    item.Percent,
                    item.ChangesReason,
                    item.RowClass
                )).ToList(),
                x.ProportionedRetainedEarnings.Select(item => new ProportionedRetainedEarningQueryDto(
                    item.FieldName,
                    item.Description,
                    item.YearEndToDateValue,
                    item.RowClass
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
                    : null,
                x.IndependentAuditorRepresentative != null
                    ? new AssemblyAttendeeQueryDto(
                        x.IndependentAuditorRepresentative.FullName,
                        x.IndependentAuditorRepresentative.NationalCode,
                        x.IndependentAuditorRepresentative.AttendingMeeting,
                        x.IndependentAuditorRepresentative.Degree,
                        x.IndependentAuditorRepresentative.DegreeRef,
                        x.IndependentAuditorRepresentative.EducationField,
                        x.IndependentAuditorRepresentative.EducationFieldRef,
                        x.IndependentAuditorRepresentative.Verification
                    )
                    : null,
                x.TopFinancialPosition != null
                    ? new AssemblyAttendeeQueryDto(
                        x.TopFinancialPosition.FullName,
                        x.TopFinancialPosition.NationalCode,
                        x.TopFinancialPosition.AttendingMeeting,
                        x.TopFinancialPosition.Degree,
                        x.TopFinancialPosition.DegreeRef,
                        x.TopFinancialPosition.EducationField,
                        x.TopFinancialPosition.EducationFieldRef,
                        x.TopFinancialPosition.Verification
                    )
                    : null
            ));
    }

    /// <summary>
    /// Restricts the specification to the CanonicalExtraAnnualAssembly with the given identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to filter by.</param>
    /// <returns>The current specification instance with the applied filter.</returns>
    public ExtraAnnualAssemblyDetailItemSpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }
}