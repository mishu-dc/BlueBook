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
    public class MarketHierarchyModel
    {
        UnitOfWork _unitOfWork = null;

        public MarketHierarchyModel(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MarketHierarchyTreeViewModel>> GetMarketHierarchyAsync()
        {
            List<MarketHierarchy> mhs = await _unitOfWork.MarketHierarchies.FindAsync(x => x.Id > 0);

            var records = mhs.Where(x => x.ParentId == null)
                    .Select(m => new MarketHierarchyTreeViewModel()
                    {
                        id = m.Id,
                        text = m.Code + "-" + m.Name,
                        code = m.Code,
                        name = m.Name,
                        type = m.Type.ToString(),
                        parentId = m.ParentId!=null?m.ParentId.Value:-1,
                        children = GetChildren(mhs, m.Id)
                    }).ToList();

            return records;
        }

        private List<MarketHierarchyTreeViewModel> GetChildren(List<MarketHierarchy> mhs, int? parentId)
        {
            var records = mhs.Where(x => x.ParentId == parentId.Value)
                    .Select(m => new MarketHierarchyTreeViewModel()
                    {
                        id = m.Id,
                        text = m.Code + "-" + m.Name,
                        code = m.Code,
                        name = m.Name,
                        type = m.Type.ToString(),
                        parentId = m.ParentId != null ? m.ParentId.Value : -1,
                        children = GetChildren(mhs, m.Id)
                    }).ToList();

            return records;
        }

        public async Task<MarketHierarchy> SaveAsync(MarketHierarchyViewModel record)
        {
            MarketHierarchy mh = null;
            MarketHierarchy parent = null;

            if (record.Id != null)
            {
                mh = _unitOfWork.MarketHierarchies.Get(record.Id.Value);
                if (mh == null)
                {
                    throw new Exception("Invalid Market Hierarchy Id");
                }

                mh.UpdatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                mh.UpdatedDate = DateTime.Now;
            }
            else
            {
                mh = new MarketHierarchy();
                _unitOfWork.MarketHierarchies.Add(mh);

                mh.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
            }

            if (record.ParentId != null)
            {
                parent = _unitOfWork.MarketHierarchies.Get(record.ParentId.Value);
            }
            
            mh.Code = record.Code;
            mh.Name = record.Name;
            mh.Parent = parent;
            mh.Type = parent != null ? parent.Type + 1 : MarketHierarchyType.Nation;

            await _unitOfWork.CompleteAsync();

            return mh;
        }

        public async Task<int> DeleteAsync(int Id)
        {
            MarketHierarchy mh = await _unitOfWork.MarketHierarchies.GetAsync(Id);

            if (mh == null)
            {
                throw new Exception("Invalid Market Hierarchy Id");
            }

            _unitOfWork.MarketHierarchies.Remove(mh);
            return await _unitOfWork.CompleteAsync();
        }
    }
}