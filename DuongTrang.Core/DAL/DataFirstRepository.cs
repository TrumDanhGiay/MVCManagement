using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.DAL
{
    public class DataFirstRepository : IDataFirstRepository
    {
        DATNEntities dbContext = new DATNEntities();
        public object getDataFirst()
        {
            ArrayList listDataFirst = new ArrayList();
            var kind = dbContext.Kinds.Where(x => x.IsDelete ==false).ToList().Select(u => u.Kind1);
            var language = dbContext.Languages.Where(x => x.IsDelete == false).ToList().Select(u => u.Language1);
            var company = dbContext.Companies.Where(x => x.IsDelete ==false).ToList().Select(u => u.CompanyName);
            var category = dbContext.Categories.Where(x => x.IsDelete == false).ToList().Select(u => u.CategoryName);
            listDataFirst.Add(kind);
            listDataFirst.Add(language);
            listDataFirst.Add(company);
            listDataFirst.Add(category);
            return listDataFirst;
        }
    }
}
