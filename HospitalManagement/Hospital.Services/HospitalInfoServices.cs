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
    public class HospitalInfoServices : IHospitalInfo
    {

         private readonly IUnitOfWork _unitOfWork;

        public HospitalInfoServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteHospitalInfo(int id)
        {
          var model= _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
            _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
            _unitOfWork.Save();

        }

        public PageResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
        {
             var vm = new HospitalInfoViewModel();
            int totalCount;
            List<HospitalInfoViewModel>  vmList= new List<HospitalInfoViewModel>();
            try 
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modelList=_unitOfWork.GenericRepository<HospitalInfo>().GetAll().Skip(ExcludeRecords).Take(pageSize).ToList();

                totalCount=_unitOfWork.GenericRepository<HospitalInfo>().GetAll().ToList().Count;

                vmList = ConvertModelToView(modelList);
            
            }
            catch (Exception)
            {
                throw;
            }

            var result = new PageResult<HospitalInfoViewModel>
            {
            
                Data = vmList,
                TotalItem = totalCount,
                PageNumber= pageNumber,
                PageSize= pageSize

            };

            return result;


        }

        public HospitalInfoViewModel GetHospitalById(int HospitalId)
        {
            var model=_unitOfWork.GenericRepository<HospitalInfo>().GetById(HospitalId);
            var vm=new HospitalInfoViewModel();
            return vm;
        }

        public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
            _unitOfWork.Save();

        }

        public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
        {
            var model = new HospitalInfoViewModel().ConvertViewModel(hospitalInfo);
            var ModelById=_unitOfWork.GenericRepository<HospitalInfo>().GetById(model.Id);
            ModelById.Name = model.Name;
            ModelById.City = model.City;
            ModelById.Pincode = model.Pincode;
            ModelById.Country = model.Country;

            _unitOfWork.GenericRepository<HospitalInfo>().Update(ModelById);
            _unitOfWork.Save();

        }

        private List<HospitalInfoViewModel> ConvertModelToView(List<HospitalInfo> modelList)
        {
            return modelList.Select(x => new HospitalInfoViewModel(x)).ToList();
        }
    }
}
