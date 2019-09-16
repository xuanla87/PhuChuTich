namespace ClassLibrary.Data.Infrastructure
{
    using System;
    public interface IDbFactory : IDisposable
    {
        NewDbContext Init();
    }
    public class DbFactory : Disposable, IDbFactory
    {
        private NewDbContext dbContext;

        public NewDbContext Init()
        {
            return dbContext ?? (dbContext = new NewDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
