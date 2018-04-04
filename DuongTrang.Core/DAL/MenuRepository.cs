using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuongTrang.Core.CustomModels;
using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;

namespace DuongTrang.Core.DAL
{
    public class MenuRepository : BaseRepository<DATNEntities, Menu>, IMenuRepository
    {
        /// <summary>
        /// Lấy danh sách menu theo quyền hạn
        /// </summary>
        /// <param name="RolesName">Tên quyền</param>
        /// <returns>Danh sách menu</returns>
        public List<MenuViewModels> GetListMenuByRoles(string RolesName)
        {
            List<MenuViewModels> listViewModels = new List<MenuViewModels>();
            //Lấy RolesID theo RolesName
            var RolesID = Context.AspNetRoles.Where(r => r.Name.Equals(RolesName)).Select(u => u.Id).FirstOrDefault();

            //Lấy MenuID theo RolesID
            var ListMenuID = Context.Role_Menu.Where(x => x.RoleId.Equals(RolesID)).Select(a => a.MenuID).ToList();

            //Lấy MenuName theo MenuID
            var ListMenu = Context.Menus.Where(t => ListMenuID.Contains(t.MenuID)).ToList();

            //Lấy tất cả menu con
            var listChildMenu = Context.ChildMenus.ToList();

            foreach (var item in ListMenu)
            {
                listViewModels.Add(new MenuViewModels
                {
                    MenuName = item.MenuName,
                    Icon = item.Icon,
                    Parrent = item.Parrent,
                    listchild = listChildMenu.Where(x => x.MenuID == item.MenuID).ToList()
                });
            }
            return listViewModels;
        }
    }
}
