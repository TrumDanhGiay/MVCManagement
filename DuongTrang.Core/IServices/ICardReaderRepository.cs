using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.IServices
{
    public interface ICardReaderRepository : IBaseRepository<CardReader>
    {
        object GetReaderInfo(Guid CardReaderID);
    }
}
