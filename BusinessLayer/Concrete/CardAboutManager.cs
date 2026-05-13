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
    public class CardAboutManager : ICardAboutService
    {
        ICardAboutDal _cardAboutDal;

        public CardAboutManager(ICardAboutDal cardAboutDal)
        {
            _cardAboutDal = cardAboutDal;
        }

        public void CardAboutAddBL(CardAbout p)
        {
            throw new NotImplementedException();
        }

        public void CardAboutDelete(CardAbout cardAbout)
        {
            throw new NotImplementedException();
        }

        public void CardAboutUpdate(CardAbout cardAbout)
        {
            throw new NotImplementedException();
        }

        public CardAbout GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<CardAbout> GetList()
        {
            return _cardAboutDal.List();
        }
    }
}
