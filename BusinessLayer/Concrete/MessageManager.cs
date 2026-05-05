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

        public List<Message> GetListInbox()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com" && x.IsDraft == false);
        }

        public List<Message> GetListSendbox()
        {
            return _messageDal.List(x => x.SenderMail == "admin@gmail.com" && x.IsDraft == false);
        }

        public List<Message> GetListDraft()
        {
            return _messageDal.List(x => x.IsDraft == true);
        }

        public void SaveDraft(Message message)
        {
            message.SenderMail = "admin@gmail.com";
            message.MessageDate = DateTime.Now;
            message.IsDraft = true;

            _messageDal.Insert(message);
        }

        public void SaveMessage(Message message)
        {
            message.SenderMail = "admin@gmail.com";
            message.MessageDate = DateTime.Now;
            message.IsDraft = false;

            _messageDal.Insert(message);
        }

        public void MessageAddBL(Message message)
        {
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

    }
}
