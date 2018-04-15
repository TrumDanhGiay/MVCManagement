using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.DAL
{
    public class CardReaderRepository : BaseRepository<DATNEntities, CardReader>, ICardReaderRepository
    {
        public object GetReaderInfo(Guid CardReaderID)
        {
            var ReaderInfo = Context.CardReaders
                .Join(Context.Readers, a => a.ReaderID, b => b.ReaderID, (a, b) => new { CardReader = a, Reader = b })
                .Where(x => x.CardReader.IsDelete == false && x.CardReader.CardReaderID == CardReaderID)
                .Select(u => new
                {
                    u.Reader.AspNetUser.Avartar,
                    u.Reader.AspNetUser.FullName,
                    u.Reader.Job.JobName,
                    u.Reader.TrainingSystem.TrainingSystem1,
                    u.Reader.Identifier,
                    u.CardReader.CardReaderCode,
                    u.CardReader.DateStart,
                    u.CardReader.DateExpried
                });
            return ReaderInfo;
        }
    }
}
