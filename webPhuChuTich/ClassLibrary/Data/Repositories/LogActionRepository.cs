namespace ClassLibrary.Data.Repositories
{
    using ClassLibrary.Data.Infrastructure;
    using ClassLibrary.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface ILogActionRepository : IRepository<LogAction>
    {
        IEnumerable<LogAction> GetByUserName(string _userName);
    }
    public class LogActionRepository : Repository<LogAction>, ILogActionRepository
    {
        public LogActionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<LogAction> GetByUserName(string _userName)
        {
            return this.DbContext.LogActions.Where(x => x.userAction == _userName);
        }
    }
}
