namespace ClassLibrary.Data.Repositories
{
    using ClassLibrary.Data.Infrastructure;
    using ClassLibrary.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IMenuRepository : IRepository<Menu>
    {
        IEnumerable<Menu> GetByParent(int? _id);
    }
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Menu> GetByParent(int? _id)
        {
            return this.DbContext.Menus.Where(x => x.menuParentId == _id);
        }
    }
}
