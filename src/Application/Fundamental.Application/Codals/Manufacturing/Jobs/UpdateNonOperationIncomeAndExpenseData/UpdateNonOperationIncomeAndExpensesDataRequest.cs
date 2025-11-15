using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateNonOperationIncomeAndExpenseData;

[HandlerCode(HandlerCode.UpdateNonOperationIncomeAndExpenses)]
public record UpdateNonOperationIncomeAndExpensesDataRequest(int Days) : IRequest<Response>;