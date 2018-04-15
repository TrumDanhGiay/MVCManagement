using DuongTrang.Core.IServices;
using Management.APICustomAuthorize;
using Management.Const;
using Management.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Management.Controllers.AdminController
{
    [Authorize]
    public class BorrowController : ApiController
    {
        private readonly IBorrowRepository borrowRepository;
        public BorrowController(IBorrowRepository _borrowRepository)
        {
            borrowRepository = _borrowRepository;
        }
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            return borrowRepository.GetAllBorrow();
        }

        // GET api/<controller>/5
        public object Get(string id)
        {
            return borrowRepository.GetBorrowInfomation(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        /// Cập nhật trạng thái yêu cầu mượn
        /// </summary>
        /// <param name="id">Mã yêu cầu</param>
        /// <param name="PendingStatus">Mã trạng thái yêu cầu mượn</param>
        /// <returns>Result</returns>
        [AuthorizeRole(ListRoles.Administrator)]
        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(string id, [FromBody]int PendingStatus)
        {
            if(id != null && PendingStatus != 0)
            {
                if(borrowRepository.UpdatePendingStatus(id, PendingStatus) == false)
                {
                    return BadRequest(MConst.ErrorAPI);
                }
                try
                {
                    if (PendingStatus == 3)
                    {
                        var borrow = borrowRepository.GetBorrow(id);
                        SmtpClient client = new SmtpClient();
                        client.Port = 587;
                        client.Host = "smtp.gmail.com";
                        client.EnableSsl = true;
                        client.Timeout = 10000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential("toi19962017@gmail.com", "25021996Aa");
                        MailMessage mm = new MailMessage("donotreply@utc.edu.com", "toi19962011@gmail.com", "Yêu cầu mượn sách số " + borrow.BorrowCode + " đã được xác nhận",
                            "Thời gian hẹn lấy sách của bạn là  : " + borrow.TimeToGet.DayOfWeek + ", " + borrow.TimeToGet.ToShortDateString() + " " +  borrow.TimeToGet.ToShortTimeString());
                        mm.BodyEncoding = UTF8Encoding.UTF8;
                        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        client.Send(mm);
                    }
                    await borrowRepository.SaveChangesAsync();
                    return Ok(MConst.SuccessEditAPI);
                }
                catch (Exception)
                {
                    return BadRequest(MConst.ErrorAPI);
                }
                
            }
            else
            {
                return BadRequest(MConst.NullID);
            }

        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}