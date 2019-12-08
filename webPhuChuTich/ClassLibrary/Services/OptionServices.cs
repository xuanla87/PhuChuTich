using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public interface IOptionServices
    {
        Option Add(Option _model, string _userName);
        void Update(Option _model, string _userName);

        Option Delete(int _id, string _userName);

        IEnumerable<Option> All();

        IEnumerable<Option> GetByContentId(int _id);
        Option GetById(int _id);

        void Save();
    }
    public class OptionServices : IOptionServices
    {
        private IOptionRepository _Repository;
        private IUnitOfWork _unitOfWork;
        private ILogActionRepository _RepositoryLogAction;
        public OptionServices(IOptionRepository Repository, IUnitOfWork unitOfWork, ILogActionRepository repositoryLogAction)
        {
            this._Repository = Repository;
            this._unitOfWork = unitOfWork;
            this._RepositoryLogAction = repositoryLogAction;
        }

        public Option Add(Option _model, string _userName)
        {
            try
            {
                LogAction entity = new LogAction();
                entity.action = "Thêm option: <a href=\"" + _model.contentId + "\" target=\"_blank\">" + _model.contentId + "</a>";
                entity.browser = "";
                entity.ipAddress = "";
                entity.userAction = _userName;
                entity.createTime = DateTime.Now;
                _RepositoryLogAction.Add(entity);
            }
            catch
            {
            }
            return _Repository.Add(_model);
        }

        public void Update(Option _model, string _userName)
        {
            try
            {
                LogAction entity = new LogAction();
                entity.action = "Thêm option: <a href=\"" + _model.contentId + "\" target=\"_blank\">" + _model.contentId + "</a>";
                entity.browser = "";
                entity.ipAddress = "";
                entity.userAction = _userName;
                entity.createTime = DateTime.Now;
                _RepositoryLogAction.Add(entity);
            }
            catch
            {
            }
            _Repository.Update(_model);
        }

        public Option Delete(int _id, string _userName)
        {
            try
            {
                LogAction entity = new LogAction();
                entity.action = "Thêm option: <a href=\"" + _id + "\" target=\"_blank\">" + _id + "</a>";
                entity.browser = "";
                entity.ipAddress = "";
                entity.userAction = _userName;
                entity.createTime = DateTime.Now;
                _RepositoryLogAction.Add(entity);
            }
            catch
            {
            }
            return _Repository.Delete(_id);
        }

        public IEnumerable<Option> All()
        {
            return _Repository.GetAll();
        }

        public IEnumerable<Option> GetByContentId(int _id)
        {
            return _Repository.GetByContenId(_id);
        }

        public Option GetById(int _id)
        {
            return _Repository.GetSingleById(_id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
