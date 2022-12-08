using MedicApp.Database;

namespace MedicApp.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, AppDbContext context)
        {          
            string httpVerb = httpContext.Request.Method.ToUpper();
          
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync<object, object>(null!, operation: async (dbctx, state, cancel) =>
            {
                    // start the transaction
                await using var transaction = await context.Database.BeginTransactionAsync();

                    // invoke next middleware 
                await _next(httpContext);

                    // commit the transaction
                await transaction.CommitAsync();

                return null!;
            }, null);
            
        }
    }
}
