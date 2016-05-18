using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendMail.Models;

namespace SendMail.Business
{
    public static class ContactBusiness
    {
        public static bool checkContactIsExist(string email)
        {
            using (SendMailEntities db = new SendMailEntities())
            {
                Contact contact = db.Contacts.FirstOrDefault(x=>x.Email == email);
                if (contact == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}