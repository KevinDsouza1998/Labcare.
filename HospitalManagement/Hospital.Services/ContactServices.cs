using Hospital.Models;
using Hospital.Repositories.Interface;
using Hospital.Utilities;
using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    internal class ContactServices : IContactService
    {

        private IUnitOfWork _unitOfWork;

        public ContactServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteContact(int id)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(id);
            _unitOfWork.GenericRepository<Contact>().Delete(model);
            _unitOfWork.Save();

        }

        public PageResult<ContactViewModel> GetAll(int pageNumber, int pageSize)
        {
            var vm = new ContactViewModel();
            int totalCount;

            List<ContactViewModel> vmList = new List<ContactViewModel>();
            try
            {

                int ExcludeRecords = (pageSize * pageNumber) - pageSize;

                var modelList = _unitOfWork.GenericRepository<Contact>().GetAll().
                    Skip(ExcludeRecords).
                Take(pageSize).ToList();

                totalCount = _unitOfWork.GenericRepository<Contact>().GetAll().ToList().Count;

                vmList = ConvertVModelToViewModelList(modelList);
            }
            catch (Exception)
            {

                throw;
            }

            var result = new PageResult<ContactViewModel>
            {
                Data = vmList,
                TotalItem = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize



            };

            return result;

        }

        public ContactViewModel GetContactById(int ContactId)
        {
            var model = _unitOfWork.GenericRepository<Contact>().GetById(ContactId);
            var vm = new ContactViewModel(model);
            return vm;

        }

        public void InsertContact(ContactViewModel Contact)
        {
            var model = new ContactViewModel().ConvertViewModel(Contact);
            _unitOfWork.GenericRepository<Contact>().Add(model);
            _unitOfWork.Save();

        }

        public void UpdateContact(ContactViewModel Contact)
        {
            var model = new ContactViewModel().ConvertViewModel(Contact);
            var ModelById = _unitOfWork.GenericRepository<Contact>().GetById(model.Id);

            ModelById.Email=Contact.Email;
            ModelById.Phone=Contact.Phone;
            ModelById.HospitalId = Contact.HosptalInfoId;

            _unitOfWork.GenericRepository<Contact>().Update(ModelById);
            _unitOfWork.Save();

        }

        private List<ContactViewModel> ConvertVModelToViewModelList(List<Contact> modelList)
        {
            return modelList.Select(x => new ContactViewModel(x)).ToList();
        }
    }
    {
    }
}
