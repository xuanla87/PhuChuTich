namespace ClassLibrary.Data.Repositories
{
    using ClassLibrary.Data.Infrastructure;
    using ClassLibrary.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IConfigSystemRepository : IRepository<ConfigSystem>
    {
        ConfigSystem GetByKey(string _key);
    }
    public class ConfigSystemRepository : Repository<ConfigSystem>, IConfigSystemRepository
    {
        public ConfigSystemRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ConfigSystem GetByKey(string _key)
        {
            return this.DbContext.ConfigSystems.FirstOrDefault(x => x.configKey == _key);
        }
    }
}
