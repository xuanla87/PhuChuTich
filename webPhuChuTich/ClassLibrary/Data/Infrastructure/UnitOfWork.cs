namespace ClassLibrary.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IUnitOfWork
    {
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private NewDbContext dbContext;
        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public NewDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
