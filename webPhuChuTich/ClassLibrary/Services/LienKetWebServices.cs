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

    public interface ILienKetWebServices
    {
        LienKetWeb Add(LienKetWeb _model, string _userName);
        void Update(LienKetWeb _model, string _userName);
        void Delete(int _id, string _userName);

        IEnumerable<LienKetWeb> All();

        LienKetWeb GetById(int _id);

        void Save();
    }
    public class LienKetWebServices : ILienKetWebServices
    {
        private ILienKetWebRepository _Repository;
        private IUnitOfWork _unitOfWork;
        private ILogActionRepository _RepositoryLogAction;
        public LienKetWebServices(ILienKetWebRepository Repository, IUnitOfWork unitOfWork, ILogActionRepository repositoryLogAction)
        {
            this._Repository = Repository;
            this._unitOfWork = unitOfWork;
            this._RepositoryLogAction = repositoryLogAction;
        }

        public LienKetWeb Add(LienKetWeb _model, string _userName)
        {
            try
            {
                LogAction entity = new LogAction();
                entity.action = "Thêm LienKetWeb: <a href=\"" + _model.lienKetWebId + "\" target=\"_blank\">" + _model.lienKetWebName + "</a>";
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
        public void Update(LienKetWeb _model, string _userName)
        {
            _Repository.Update(_model);
            try
            {
                LogAction entity = new LogAction();
                entity.action = "Sửa LienKetWeb: <a href=\"" + _model.lienKetWebId + "\" target=\"_blank\">" + _model.lienKetWebName + "</a>";
                entity.browser = "";
                entity.ipAddress = "";
                entity.userAction = _userName;
                entity.createTime = DateTime.Now;
                _RepositoryLogAction.Add(entity);
            }
            catch
            {
            }
        }

        public void Delete(int _id, string _userName)
        {
            try
            {
                LogAction entity = new LogAction();
                entity.action = "Thêm LienKetWeb: <a href=\"" + _id + "\" target=\"_blank\">" + _id + "</a>";
                entity.browser = "";
                entity.ipAddress = "";
                entity.userAction = _userName;
                entity.createTime = DateTime.Now;
                _RepositoryLogAction.Add(entity);
                _Repository.Delete(_id);
            }
            catch
            {
            }
        }

        public IEnumerable<LienKetWeb> All()
        {
            return _Repository.GetAll().OrderBy(x => x.isSort);
        }

        public LienKetWeb GetById(int _id)
        {
            return _Repository.GetSingleById(_id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }

}
