using DataAccessLayer.Interface;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class CompanyProfileRepository : ICompanyProfile
    {
        public GiellyGreen_Team1Entities db = new GiellyGreen_Team1Entities();
        public dynamic GetProfile()
        {
            return db.GetCompanyProfile().ToList();
        }

        public dynamic PostProfile(dynamic model, int? id)
        {
            if(model != null)
                db.InsertUpdateCompanyProfile(id, model.CompanyName, model.AddressLine, model.City, model.ZipCode, model.Country, model.DefaultVat);
            return model;
        }
    }
}
