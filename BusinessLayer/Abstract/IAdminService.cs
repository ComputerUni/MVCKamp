using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService
    {
        void AdminAdd(Admin admin);
        Admin Login(Admin admin);

        string[] GetAdminRole(string username);
    }
}
