using BCrypt.Net;
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
    public class AdminManager : IAdminService
    {
        IAdminDal _adminDal;

        public AdminManager(IAdminDal adminDal)
        {
            _adminDal = adminDal;
        }

        public void AdminAdd(Admin admin)
        {
            admin.AdminPassword = BCrypt.Net.BCrypt.HashPassword(admin.AdminPassword);
            _adminDal.Insert(admin);
        }

        public Admin Login(Admin admin) 
        {
            var adminUser = _adminDal.Get(x => x.AdminUserName == admin.AdminUserName && x.AdminStatus == true && x.AdminRole == "A");

            if(adminUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(admin.AdminPassword, adminUser.AdminPassword))
                {
                    return adminUser;
                }
            }
            return null;
        }

        public string[] GetAdminRole(string username)
        {
            var adminUser = _adminDal.Get(x => x.AdminUserName == username);
            return new string[] { adminUser.AdminRole };
        }

        public List<Admin> GetList()
        {
            return _adminDal.List();
        }

        public Admin GetByID(int id)
        {
            return _adminDal.Get(x => x.AdminID == id);
        }

        public void AdminDelete(Admin admin)
        {
            _adminDal.Update(admin);
        }

        public void AdminUpdate(Admin admin)
        {
            var values = _adminDal.Get(x => x.AdminID == admin.AdminID);

            values.AdminUserName = admin.AdminUserName;
            values.AdminRole = admin.AdminRole;

            if (!string.IsNullOrWhiteSpace(admin.AdminPassword))
            {
                values.AdminPassword =
                    BCrypt.Net.BCrypt.HashPassword(admin.AdminPassword);
            }

            _adminDal.Update(values);
        }
    }
}
