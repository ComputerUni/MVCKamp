using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
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

        public List<Message> GetListInbox(string username, string searchString = null)
        {
            var values = _messageDal.List(x => x.ReceiverMail == username
                                    && x.IsDraft == false);

            if (!string.IsNullOrEmpty(searchString))
            {
                values = values.Where(x => x.MessageContent.Contains(searchString) || x.Subject.Contains(searchString)).ToList();
            }
            return values;
        }

        public List<Message> GetListSendbox(string mail, string searchString = null)
        {
            var values = _messageDal.List(x => x.SenderMail == mail && x.IsDraft == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                values = values.Where(x => x.MessageContent.Contains(searchString) || x.Subject.Contains(searchString)).ToList();
            }
            return values;
        }

        public List<Message> GetListDraft()
        {
            return _messageDal.List(x => x.IsDraft == true);
        }


        public void MessageAddBL(Message message)
        {
            message.SenderMail = "admin@gmail.com";
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
