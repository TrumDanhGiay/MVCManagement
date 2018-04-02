using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.CustomModels
{
    public class BookViewModels
    {
        public string BookCode { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string CompanyPublishName { get; set; }
        public string YearPublish { get; set; }
        public string Kind { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }
        public string BookFormat { get; set; }
        public string Price { get; set; }
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public string CreateBy { get; set; }
        public string Image { get; set; }
    }
}
