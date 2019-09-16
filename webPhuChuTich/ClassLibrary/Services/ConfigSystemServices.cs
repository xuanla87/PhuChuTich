namespace ClassLibrary.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ClassLibrary.Data.Infrastructure;
    using ClassLibrary.Data.Repositories;
    using ClassLibrary.Models;

    public interface IConfigSystemServices
    {
        ConfigSystem Add(ConfigSystem _model);

        void Update(ConfigSystem _model);

        ConfigSystem Delete(int _id);

        IEnumerable<ConfigSystem> GetAll();

        ConfigSystem GetByKey(string _key);

        ConfigSystem GetById(int _id);

        string GetValueByKey(string _key);

        void SaveData(string _key, string _value);

        void Save();
    }
    public class ConfigSystemServices : IConfigSystemServices
    {
        private IConfigSystemRepository _Repository;
        private IUnitOfWork _unitOfWork;
        public ConfigSystemServices(IConfigSystemRepository Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = Repository;
            this._unitOfWork = unitOfWork;
        }

        public ConfigSystem Add(ConfigSystem _model)
        {
            return _Repository.Add(_model);
        }

        public ConfigSystem Delete(int _id)
        {
            return _Repository.Delete(_id);
        }

        public IEnumerable<ConfigSystem> GetAll()
        {
            return _Repository.GetAll();
        }

        public ConfigSystem GetByKey(string _key)
        {
            return _Repository.GetByKey(_key);
        }
        public string GetValueByKey(string _key)
        {
            var entity = _Repository.GetByKey(_key);
            if (entity != null)
                return entity.configValue;
            else
                return "";
        }
        public ConfigSystem GetById(int _id)
        {
            return _Repository.GetSingleById(_id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ConfigSystem _model)
        {
            _Repository.Update(_model);
        }

        public void SaveData(string _key, string _value)
        {
            var entity = _Repository.GetByKey(_key);
            if (entity != null)
            {
                entity.configValue = _value;
                _Repository.Update(entity);
                _unitOfWork.Commit();
            }
            else
            {
                entity = new ConfigSystem
                {
                    configKey = _key,
                    configValue = _value,
                    isTrash = false
                };
                _Repository.Add(entity);
                _unitOfWork.Commit();
            }
        }
    }
}
