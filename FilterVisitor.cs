    public class FilterVisitor : DefaultExpressionVisitor
    {
        private readonly IdkWhatImDoingContext _context;

        public int FilterValue { get; set; }
        public CategoryVisitor(DbContext contextForFilter)
        {
            _context = (IdkWhatImDoingContext)contextForFilter;
            this.FilterValue = _context.CategoryID;
        }

        public override DbExpression Visit(DbScanExpression expression)
        {
            if (this.FilterValue > 0)
            {
                // Get the current expression
                var dbExpression = base.Visit(expression);
                var binding = expression.Bind();
                return binding.Filter(
                    binding.VariableType
                        .Variable(binding.VariableName)
                        .Property("FilterFieldName")
                        .Equal(DbExpression.FromInt32(this.FilterValue)));
            }

            return base.Visit(expression);
        }
    }
