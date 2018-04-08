using BlueBook.DataAccess.Entities;
using BlueBook.Entity.Configurations;
using BlueBook.MvcUi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BlueBook.MvcUi.Models
{
    public class DistributorModel
    {
        UnitOfWork _unitOfWork = null;

        public DistributorModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Distributor>> GetDistributorsByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            return await _unitOfWork.Distributors.GetDistributorsByPageAsync(page, limit, sortBy, direction, code, name);
        }

        public async Task<int> GetTotalDistributorsAsync(int? page, int? limit, string sortBy, string direction, string code, string name)
        {
            return await _unitOfWork.Distributors.GetTotalDistributorsAsync(page, limit, sortBy, direction, code, name);
        }

        public async Task<int> SaveAsync(DistributorViewModel record)
        {
            Distributor distributor = null;

            if (record.Id != null)
            {
                distributor = _unitOfWork.Distributors.Get(record.Id.Value);
                if (distributor == null)
                {
                    throw new Exception("Invalid Distributor Id");
                }

                distributor.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                distributor.UpdatedDate = DateTime.Now;
            }
            else
            {
                distributor = new Distributor();
                _unitOfWork.Distributors.Add(distributor);

                distributor.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            }

            distributor.Code = record.Code;
            distributor.Name = record.Name;
            distributor.Address = record.Address;
            
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            Distributor distributor = await _unitOfWork.Distributors.GetAsync(id);

            if (distributor == null)
            {
                throw new Exception("Invalid Distributor Id");
            }

            _unitOfWork.Distributors.Remove(distributor);
            return await _unitOfWork.CompleteAsync();
        }

       
    }
}