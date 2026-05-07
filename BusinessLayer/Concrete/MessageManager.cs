using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetByID(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

        public List<Message> GetListInbox(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p && x.IsDraft == false);
        }

        public List<Message> GetListSendbox(string p)
        {
            return _messageDal.List(x => x.SenderMail == p && x.IsDraft == false);
        }

        public List<Message> GetListDraft()
        {
            return _messageDal.List(x => x.IsDraft == true);
        }


        public void MessageAddBL(Message message)
        {
            //Admin için sonrasında geri al yoksa sıkıntı olur
            //message.SenderMail = "admin@gmail.com";
            message.MessageDate = DateTime.Now;
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }

        public void IsReadMessage(int id)
        {
            var messageValue = _messageDal.Get(x => x.MessageID == id);
            if (messageValue != null)
            {
                messageValue.IsRead = true;
                _messageDal.Update(messageValue);
            }
        }
    }
}
