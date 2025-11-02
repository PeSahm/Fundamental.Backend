using Fundamental.Domain.Codals.Manufacturing.Enums;
using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Dto.RegisterCapitalIncrease.V1;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.RegisterCapitalIncrease;

public class CapitalIncreaseRegistrationNoticeV1Processor(IServiceScopeFactory serviceScopeFactory) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.UnKnown;
    public static LetterType LetterType => LetterType.CapitalIncreaseRegistrationNotice;
    public static CodalVersion CodalVersion => CodalVersion.V1;
    public static LetterPart LetterPart => LetterPart.NotSpecified;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JsonSerializerSettings jsonSettings = new() { NullValueHandling = NullValueHandling.Ignore };
        CodalCapitalIncreaseRegistrationNotice? capitalIncrease =
            JsonConvert.DeserializeObject<CodalCapitalIncreaseRegistrationNotice>(model.Json, jsonSettings);

        if (capitalIncrease is null)
        {
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        DateTime? startDate = capitalIncrease.StartDate.ToGregorianDateTime();

        if (startDate is null)
        {
            return;
        }

        Domain.Codals.Manufacturing.Entities.CapitalIncreaseRegistrationNotice? existingStatement =
            await dbContext.CapitalIncreaseRegistrationNotices
                .FirstOrDefaultAsync(
                    x => x.TraceNo == statement.TracingNo,
                    cancellationToken);

        Symbol symbol = await dbContext.Symbols
            .FirstAsync(x => x.Isin == statement.Isin, cancellationToken);

        if (existingStatement is null)
        {
            Domain.Codals.Manufacturing.Entities.CapitalIncreaseRegistrationNotice capitalIncreaseRegistrationNotice = new(
                Guid.NewGuid(),
                symbol,
                statement.TracingNo,
                statement.HtmlUrl,
                startDate.ToDateOnly() ?? DateTime.Now.ToDateOnly(),
                capitalIncrease.LastExtraAssembly.ToGregorianDateOnly() ?? DateTime.Now.ToDateOnly(),
                new CodalMoney(capitalIncrease.NewCapital),
                new CodalMoney(capitalIncrease.PreviousCapital),
                new CodalMoney(capitalIncrease.Reserves),
                new CodalMoney(capitalIncrease.RetaindedEarning),
                new CodalMoney(capitalIncrease.RevaluationSurplus),
                new CodalMoney(capitalIncrease.CashIncoming),
                new CodalMoney(capitalIncrease.SarfSaham),
                capitalIncrease.CashForceclosurePriority,
                capitalIncrease.PrimaryMarketTracingNo,
                DateTime.UtcNow
            );

            dbContext.Add(capitalIncreaseRegistrationNotice);
        }
        else
        {
            existingStatement.Update(capitalIncrease.NewCapital, capitalIncrease.PreviousCapital);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}