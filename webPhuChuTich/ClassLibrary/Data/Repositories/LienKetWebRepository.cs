namespace ClassLibrary.Data.Repositories
{
    using ClassLibrary.Data.Infrastructure;
    using ClassLibrary.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface ILienKetWebRepository : IRepository<LienKetWeb>
    {
        IEnumerable<LienKetWeb> GetById(int _id);
    }
    public class LienKetWebRepository : Repository<LienKetWeb>, ILienKetWebRepository
    {
        public LienKetWebRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<LienKetWeb> GetById(int _id)
        {
            return this.DbContext.LienKetWebs.Where(x => x.lienKetWebId == _id);
        }
    }
}
