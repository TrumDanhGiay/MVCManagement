using DuongTrang.Core.CustomModels;
using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.IServices
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
        List<MenuViewModels> GetListMenuByRoles(string RolesID);
    }
}
