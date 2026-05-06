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
    public class WriterManager : IWriterService
    {
        IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public Writer GetByID(int id)
        {
            return _writerDal.Get(x => x.WriterID == id);
        }

        public Writer GetByMail(string mail)
        {
            return _writerDal.Get(x => x.WriterMail == mail);
        }

        public List<Writer> GetList()
        {
            return _writerDal.List();
        }

        public Writer Login(Writer writer)
        {
            var writerUser = _writerDal.Get(x => x.WriterMail == writer.WriterMail);
            if (writerUser != null)
            {
                if (writer.WriterPassword == writerUser.WriterPassword)
                {
                    return writerUser;
                }
            }
            return null;
        }

        public void WriterAdd(Writer writer)
        {
            _writerDal.Insert(writer);
        }

        public void WriterDelete(Writer writer)
        {
            _writerDal.Delete(writer);
        }

        public void WriterUpdate(Writer writer)
        {
            _writerDal.Update(writer);
        }
    }
}
