using MedicApp.Database;

namespace MedicApp.Controllers
{
    public class AbstractController
    {
        //public readonly RequestDelegate _next;
        //public async Task Invoke(HttpContext context, AppDbContext appDbContext)
        //{
        //    string HttpVerb = context.Request.Method.ToUpper();
        //    var strategy = appDbContext.Database.CreateExecutionStrategy();
        //    await strategy.ExecuteAsync<object, object>(null!, operation: async (dbcxt, state, cancel) =>
        //    {
        //        await using var transaction = await appDbContext.Database.BeginTransactionAsync();

        //        await _next(context);

        //        await transaction.CommitAsync();

        //        return null;
        //    }, null);
        //}
    }
}