using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using SendMail.Models;
using SendMail.Util;

namespace SendMail
{
    public partial class frmChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                SendMailEntities db = new SendMailEntities();
                User user = db.Users.FirstOrDefault(x => x.AccountName == Context.User.Identity.Name);
                if (Cryption.Decrypt(user.Password) == txt_pass_old.Value)
                {
                    if (txt_pass_new.Value == txt_pass_new_repeat.Value)
                    {
                        user.Password = Cryption.Encrypt(txt_pass_new.Value);
                        db.SaveChanges();
                        lbl_failure.Text = "";
                        string message = "Bạn đã thay đổi mật khẩu thành công";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                    }
                    else
                    {
                        lbl_failure.Text = "Mật khẩu nhập lại không đúng!";
                    }
                }
                else
                {
                    lbl_failure.Text = "Mật khẩu cũ của bạn không đúng!";
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
            }
        }
    }
}