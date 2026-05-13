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

    public class SkillsManager : ISkillsService
    {
        ISkillsDal _skillsDal;
        public SkillsManager(ISkillsDal skillDal)
        {
            _skillsDal = skillDal;
        }

        public Skills GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Skills> GetList()
        {
            return _skillsDal.List();
        }

        public void SkillsAddBL(Skills p)
        {
            throw new NotImplementedException();
        }

        public void SkillsDelete(Skills skills)
        {
            throw new NotImplementedException();
        }

        public void SkillsUpdate(Skills skills)
        {
            throw new NotImplementedException();
        }
    }
}
