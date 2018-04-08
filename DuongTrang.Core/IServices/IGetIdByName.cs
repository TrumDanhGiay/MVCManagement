using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.IServices
{
    public interface IGetIdByName
    {
        Guid GetKindID(string name);
        Guid GetCategoryID(string name);
        Guid GetCompanyID(string name);
        Guid GetLanguageID(string name);
    }
}
