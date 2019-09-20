namespace ClassLibrary.Data.Repositories
{

    using ClassLibrary.Data.Infrastructure;
    using ClassLibrary.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IOptionRepository : IRepository<Option>
    {
        IEnumerable<Option> GetByContenId(int _id);
    }
    public class OptionRepository : Repository<Option>, IOptionRepository
    {
        public OptionRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Option> GetByContenId(int _id)
        {
            return this.DbContext.Options.Where(x => x.contentId == _id);
        }
    }
}
