using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pos.Model;
using Pos.ViewModel;
using System.Data.Entity;

namespace Pos.Repo
{
    public class MstResidence
    {
        private PosContext db;
        private MapperResidence mapper;
        public MstResidence()
        {
            db = new PosContext();
            mapper = new MapperResidence();
        }

        public List<ProvinceVm> GetVmProvinces()
        {
            return db.PosMstProvince
                .Where(x => x.Active == true)
                .Select(model => new ProvinceVm()
                {
                    Id = model.Id,
                    Name = model.Name
                })
                .ToList();
        }

        public List<RegionVM> GetVmRegionByProvinceId(long id)
        {
            return db.PosMstRegion
                .Where(x => x.Active == true && x.ProvinceId==id)
                .OrderBy(x=> x.Name)
                .Select(model => new RegionVM()
                {
                    Id = model.Id,
                    Name = model.Name
                })
                .ToList();
        }


        public List<DistrictVm> GetVmDistrictByRegionId(long id)
        {
            return db.PosMstDistrict
                .Where(x => x.Active == true && x.RegionId == id)
                .OrderBy(x => x.Name)
                .Select(model => new DistrictVm()
                {
                    Id = model.Id,
                    Name = model.Name
                })
                .ToList();
        }

        //responsible to mapping vm to model or vice versa
        protected class MapperResidence
        {
            
            public ProvinceVm modelToVm(PosMstProvince model)
            {
                var viewModel = new ProvinceVm()
                {
                    Id = model.Id,
                    Name = model.Name
                };
                return viewModel;
            }


            //public ProvinceVm modelToVm(PosMstProvince model)
            //{
            //    var viewModel = new ProvinceVm()
            //    {
            //        Id = model.Id,
            //        Name = model.Name
            //    };
            //    return viewModel;
            //}


            //public ProvinceVm modelToVm(PosMstProvince model)
            //{
            //    var viewModel = new ProvinceVm()
            //    {
            //        Id = model.Id,
            //        Name = model.Name
            //    };
            //    return viewModel;
            //}
        }
    }
}
