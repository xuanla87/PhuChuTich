
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
    public interface IContactServices
    {
        Contact Add(Contact _model);

        void Update(Contact _model);

        Contact Delete(int _id);

        ContactView GetAll(string _keyWords, DateTime? _fromDate, DateTime? _toDate, bool? _isTrash, int? _pageIndex, int? _pageSize);

        IEnumerable<Contact> All();

        IEnumerable<Contact> GetAllByEmail(string _email);

        Contact GetById(int _id);

        void Save();
    }
    public class ContactServices : IContactServices
    {
        private IContactRepository _Repository;
        private IUnitOfWork _unitOfWork;
        public ContactServices(IContactRepository Repository, IUnitOfWork unitOfWork)
        {
            this._Repository = Repository;
            this._unitOfWork = unitOfWork;
        }

        public Contact Add(Contact _model)
        {
            return _Repository.Add(_model);
        }

        public Contact Delete(int _id)
        {
            return _Repository.Delete(_id);
        }

        public IEnumerable<Contact> All()
        {
            return _Repository.GetAll();
        }

        public ContactView GetAll(string _keyWords, DateTime? _fromDate, DateTime? _toDate, bool? _isTrash, int? _pageIndex, int? _pageSize)
        {
            var entitys = _Repository.GetAll();

            if (!string.IsNullOrEmpty(_keyWords))
            {
                entitys = entitys.Where(x => x.contactFullName.ToLower().Contains(_keyWords.ToLower().Trim()) || x.contactEmail.ToLower().Contains(_keyWords.ToLower().Trim()) || x.contactTitle.ToLower().Contains(_keyWords.ToLower().Trim()));
            }
            if (_fromDate.HasValue)
            {
                entitys = entitys.Where(x => x.createTime.Date >= _fromDate.Value.Date);
            }
            if (_toDate.HasValue)
            {
                entitys = entitys.Where(x => x.createTime.Date <= _toDate.Value.Date);
            }
            if (_isTrash.HasValue)
            {
                entitys = entitys.Where(x => x.isTrash == _isTrash);
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
            return new ContactView { ViewContacts = entitys, Total = totalPage };
        }

        public IEnumerable<Contact> GetAllByEmail(string _email)
        {
            return _Repository.GetByEmails(_email);
        }

        public Contact GetById(int _id)
        {
            return _Repository.GetSingleById(_id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Contact _model)
        {
            _Repository.Update(_model);
        }
    }
}
