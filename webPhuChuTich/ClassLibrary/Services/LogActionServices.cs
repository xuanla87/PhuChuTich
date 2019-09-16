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
    public interface ILogActionServices
    {
        LogAction Add(LogAction _model);

        IEnumerable<LogAction> All();

        LogActionView GetAll(string _keyWords, string _userName, DateTime? _fromDate, DateTime? _toDate, int? _pageIndex, int? _pageSize);

        IEnumerable<LogAction> GetByUserName(string _userName);

        LogAction GetById(int _id);

        void Save();
    }
    public class LogActionServices : ILogActionServices
    {
        private ILogActionRepository _Repository;
        private IUnitOfWork _unitOfWork;
        public LogActionServices(ILogActionRepository Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = Repository;
            this._unitOfWork = unitOfWork;
        }

        public LogAction Add(LogAction _model)
        {
            return _Repository.Add(_model);
        }

        public LogActionView GetAll(string _keyWords, string _userName, DateTime? _fromDate, DateTime? _toDate, int? _pageIndex, int? _pageSize)
        {
            var entitys = _Repository.GetAll();
            if (!string.IsNullOrEmpty(_keyWords))
            {
                entitys = entitys.Where(x => x.userAction.ToLower().Contains(_keyWords.ToLower().Trim()) || x.action.Contains(_keyWords.ToLower().Trim()));
            }
            if (!string.IsNullOrEmpty(_userName))
            {
                entitys = entitys.Where(x => x.userAction == _userName);
            }
            if (_fromDate.HasValue)
            {
                entitys = entitys.Where(x => x.createTime >= _fromDate.Value.Date);
            }
            if (_toDate.HasValue)
            {
                entitys = entitys.Where(x => x.createTime <= _toDate.Value.Date);
            }
            entitys = entitys.OrderByDescending(x => x.createTime);
            int totalRecord = entitys.Count();
            if (_pageIndex != null && _pageSize != null)
            {
                entitys = entitys.Skip((_pageIndex.Value - 1) * _pageSize.Value);
            }
            var totalPage = 0;
            if (_pageSize != null)
            {
                totalPage = (int)Math.Ceiling(1.0 * totalRecord / _pageSize.Value);
                entitys = entitys.Take(_pageSize.Value);
            }
            return new LogActionView { ViewLogActions = entitys, Total = totalPage };
        }

        public IEnumerable<LogAction> All()
        {
            return _Repository.GetAll();
        }

        public IEnumerable<LogAction> GetByUserName(string _userName)
        {
            return _Repository.GetByUserName(_userName);
        }

        public LogAction GetById(int _id)
        {
            return _Repository.GetSingleById(_id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
