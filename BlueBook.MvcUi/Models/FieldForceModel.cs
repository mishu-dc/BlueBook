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
    public class FieldForceModel
    {
        UnitOfWork _unitOfWork = null;

        public FieldForceModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<FieldForce>> GetFieldForceByPageAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? distributorId)
        {
            return await _unitOfWork.FieldForces.GetFieldForceByPageAsync(page, limit, sortBy, direction, code, name, distributorId);
        }

        public async Task<int> GetTotalFieldForceAsync(int? page, int? limit, string sortBy, string direction, string code, string name, int? distributorId)
        {
            return await _unitOfWork.FieldForces.GetTotalFieldForceAsync(page, limit, sortBy, direction, code, name, distributorId);
        }

        public async Task<int> SaveAsync(FieldForceViewModel record)
        {
            FieldForce fieldForce = null;
            FieldForceAddress address = null;
            MarketHierarchy mh = null;

            

            if (record.Id != null)
            {
                fieldForce = _unitOfWork.FieldForces.Get(record.Id.Value);
                if (fieldForce == null)
                {
                    throw new Exception("Invalid Fieldforce Id");
                }

                fieldForce.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                fieldForce.UpdatedDate = DateTime.Now;
                address = fieldForce.Address;
            }
            else
            {
                fieldForce = new FieldForce();
                _unitOfWork.FieldForces.Add(fieldForce);

                fieldForce.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            }

            if (address == null)
            {
                address = new FieldForceAddress();
                fieldForce.Address = address;
            }

            fieldForce.Code = record.Code;
            fieldForce.Name = record.Name;
            fieldForce.Email = record.Email;
            fieldForce.Phone = record.Phone;

            address.AddressLine1 = record.AddressLine1;
            address.AddressLine2 = record.AddressLine2;
            address.City = record.City;
            address.State = record.State;
            address.Zip = record.Zip;

            if(record.MarketHierarchyId!=null)
                mh = _unitOfWork.MarketHierarchies.Get(record.MarketHierarchyId.Value);

            fieldForce.MarketHierarchy = mh;
            

            if (fieldForce.Distributors != null)
            {
                fieldForce.Distributors.Clear();
            }
            else
            {
                fieldForce.Distributors = new List<Distributor>();
            }

            if (record.DistributorIds!=null && record.DistributorIds.Count > 0)
            {
                foreach(int id in record.DistributorIds)
                {
                    Distributor distributor = _unitOfWork.Distributors.Get(id);
                    if (distributor != null)
                    {
                        fieldForce.Distributors.Add(distributor);
                    }
                }
            }
            
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            FieldForce fieldforce = await _unitOfWork.FieldForces.GetAsync(id);

            if (fieldforce == null)
            {
                throw new Exception("Invalid Fieldforce Id");
            }

            _unitOfWork.FieldForces.Remove(fieldforce);
            return await _unitOfWork.CompleteAsync();
        }


    }
}