    public class FilterInterceptor : IDbCommandTreeInterceptor
    {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
        {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.CSpace)
            {
                var queryCommand = interceptionContext.Result as DbQueryCommandTree;
                if (queryCommand != null)
                {
                    var context = interceptionContext.DbContexts.FirstOrDefault();
                    if (context != null)
                    {
                        var newQuery =
                            queryCommand.Query.Accept(new CategoryVisitor(context));
                        interceptionContext.Result = new DbQueryCommandTree(
                            queryCommand.MetadataWorkspace, queryCommand.DataSpace, newQuery);
                    }
                }
            }
        }
    }
