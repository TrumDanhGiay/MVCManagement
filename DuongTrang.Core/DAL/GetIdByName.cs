using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.DAL
{
    public class GetIdByName : IGetIdByName
    {
        DATNEntities dbContext = new DATNEntities();
        public Guid GetCategoryID(string name)
        {
            return dbContext.Categories.Where(x => x.CategoryName.Trim().Equals(name.Trim())).ToList().Select(u => u.CategoryID).FirstOrDefault();
        }

        public Guid GetCompanyID(string name)
        {
            return dbContext.Companies.Where(x => x.CompanyName.Trim().Equals(name.Trim())).ToList().Select(u => u.CompanyID).FirstOrDefault();
        }

        public Guid GetKindID(string name)
        {
            return dbContext.Kinds.Where(x => x.Kind1.Trim().Equals(name.Trim())).ToList().Select(u => u.KindID).FirstOrDefault();
        }

        public Guid GetLanguageID(string name)
        {
            return dbContext.Languages.Where(x => x.Language1.Trim().Equals(name.Trim())).ToList().Select(u => u.LanguageID).FirstOrDefault();
        }
    }
}
