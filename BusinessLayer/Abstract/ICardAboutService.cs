using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICardAboutService
    {
        List<CardAbout> GetList();
        void CardAboutAddBL(CardAbout p);
        CardAbout GetByID(int id);
        void CardAboutDelete(CardAbout cardAbout);
        void CardAboutUpdate(CardAbout cardAbout);
    }
}
