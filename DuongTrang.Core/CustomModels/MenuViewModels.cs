﻿using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.CustomModels
{
    public class MenuViewModels
    {
        public string MenuName { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        public List<ChildMenuViewModels> menuItems { get; set; }
    }
}
