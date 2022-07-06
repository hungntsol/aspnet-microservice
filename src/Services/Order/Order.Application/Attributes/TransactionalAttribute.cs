using System.Transactions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Order.Application.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
public class TransactionalAttribute : Attribute, IAsyncActionFilter
{
	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		using var transactionScope = new TransactionScope();
		var actionExecutedContext = await next();

		if (actionExecutedContext.Exception is null)
		{
			transactionScope.Complete();
		}
	}
}