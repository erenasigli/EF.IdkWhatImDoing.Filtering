        public class BlaBlaEntitiesConfiguration : DbConfiguration
        {
            public BlaBlaEntitiesConfiguration()
            {
                AddInterceptor(new FilterInterceptor());
            }
        }
