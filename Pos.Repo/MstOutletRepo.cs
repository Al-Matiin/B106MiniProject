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
    public class MstOutletRepo:IDisposable
    {
        private PosContext db;
        private MapperMstOutlet mapper;

        //ctor
        public MstOutletRepo()
        {
            db     = new PosContext();
            mapper = new MapperMstOutlet();
        }

        //get list VM that Name start with name but only in active
        public List<MstOutletVM> GetByNameContain(string _name)
        {
            List<MstOutletVM> list;
            if (!string.IsNullOrEmpty(_name))
            {
                list =
                    (
                        from Outlet in db.PosMstOutlet
                        where Outlet.Active == true && Outlet.Name.ToLower().Contains(_name.ToLower())
                        select new MstOutletVM()
                        {
                            Id         = Outlet.Id,
                            Name       = Outlet.Name,
                            Address    = Outlet.Address,
                            Phone      = Outlet.Phone,
                            Email      = Outlet.Email,
                            ProvinceId = Outlet.ProvinceId,
                            DistrictId = Outlet.DistrictId,
                            RegionId   = Outlet.RegionId,
                            PostalCode = Outlet.PostalCode,
                            CreatedBy  = Outlet.CreatedBy,
                            CreatedOn  = Outlet.CreatedOn,
                            ModifiedBy = Outlet.ModifiedBy,
                            ModifiedOn = Outlet.ModifiedOn,
                            Active     = Outlet.Active
                        }
                    )
                    .ToList();
            }
            else
                list = GetVm();
            return list;
        }


        //get vm by Id but only in active
        public MstOutletVM GetById(long id)
        {
            var viewModel =
                 (
                     from Outlet in db.PosMstOutlet
                     where Outlet.Active == true && Outlet.Id == id
                     select new MstOutletVM()
                     {
                         Id         = Outlet.Id,
                         Name       = Outlet.Name,
                         Address    = Outlet.Address,
                         Phone      = Outlet.Phone,
                         Email      = Outlet.Email,
                         ProvinceId = Outlet.ProvinceId,
                         DistrictId = Outlet.DistrictId,
                         RegionId   = Outlet.RegionId,
                         PostalCode = Outlet.PostalCode,
                         CreatedBy  = Outlet.CreatedBy,
                         CreatedOn  = Outlet.CreatedOn,
                         ModifiedBy = Outlet.ModifiedBy,
                         ModifiedOn = Outlet.ModifiedOn,
                         Active     = Outlet.Active
                     }
                 )
                 .SingleOrDefault();

            return viewModel;
        }


        //get all vm with active
        public List<MstOutletVM> GetVm()
        {
            var list =
                (
                    from Outlet in db.PosMstOutlet
                    where Outlet.Active == true
                    select new MstOutletVM()
                    {
                        Id         = Outlet.Id,
                        Name       = Outlet.Name,
                        Address    = Outlet.Address,
                        Phone      = Outlet.Phone,
                        Email      = Outlet.Email,
                        ProvinceId = Outlet.ProvinceId,
                        DistrictId = Outlet.DistrictId,
                        RegionId   = Outlet.RegionId,
                        PostalCode = Outlet.PostalCode,
                        CreatedBy  = Outlet.CreatedBy,
                        CreatedOn  = Outlet.CreatedOn,
                        ModifiedBy = Outlet.ModifiedBy,
                        ModifiedOn = Outlet.ModifiedOn,
                        Active     = Outlet.Active
                    }
                )
                .ToList();

            return list;

        }



        //add outlet to database
        public void Add(MstOutletVM viewModel)
        {
            var model             = mapper.vmToModel(viewModel);
            model.Id              = GetNextId();
            model.Active          = true;
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
        }


        //update outlet
        public void Update(MstOutletVM viewModel)
        {
            var model             = mapper.vmToModel(viewModel);
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }


        //delete outlet-->this wiil not delete physically, only set active to false
        public void Delete(MstOutletVM viewModel)
        {
            var model             = mapper.vmToModel(viewModel);
            model.Active          = false;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
        }

        


        //get id with max value +1
        protected int GetNextId()
        {
            var _previous = db.PosMstOutlet.Max(item => (int?)item.Id);
            return (_previous ?? 0) + 1;
        }




        //responsible to mapping vm to model or vice versa
        protected class MapperMstOutlet
        {
            public PosMstOutlet vmToModel(MstOutletVM viewModel)
            {
                var model = new PosMstOutlet()
                {
                    Id         = viewModel.Id,
                    Name       = viewModel.Name,
                    Address    = viewModel.Address,
                    Phone      = viewModel.Phone,
                    Email      = viewModel.Email,
                    ProvinceId = viewModel.ProvinceId,
                    DistrictId = viewModel.DistrictId,
                    RegionId   = viewModel.RegionId,
                    PostalCode = viewModel.PostalCode,
                    CreatedBy  = viewModel.CreatedBy,
                    CreatedOn  = viewModel.CreatedOn,
                    ModifiedBy = viewModel.ModifiedBy,
                    ModifiedOn = viewModel.ModifiedOn,
                    Active     = viewModel.Active
                };
                return model;
            }




            public MstOutletVM modelToVm(PosMstOutlet model)
            {
                var viewModel = new MstOutletVM()
                {
                    Id         = model.Id,
                    Name       = model.Name,
                    Address    = model.Address,
                    Phone      = model.Phone,
                    Email      = model.Email,
                    ProvinceId = model.ProvinceId,
                    DistrictId = model.DistrictId,
                    RegionId   = model.RegionId,
                    PostalCode = model.PostalCode,
                    CreatedBy  = model.CreatedBy,
                    CreatedOn  = model.CreatedOn,
                    ModifiedBy = model.ModifiedBy,
                    ModifiedOn = model.ModifiedOn,
                    Active     = model.Active
                };
                return viewModel;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MstOutletRepo() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
