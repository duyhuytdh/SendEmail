using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SendMail.Util;
using System.Web.Security;
using System.Diagnostics;

namespace SendMail
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = 0;
                string constr = ConfigurationManager.ConnectionStrings["SendMailConnectionString"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("Validate_User"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccountName", txt_account.Value);
                        cmd.Parameters.AddWithValue("@Password", Cryption.Encrypt(txt_password.Value));
                        cmd.Connection = con;
                        con.Open();
                        userId = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                    }
                    switch (userId)
                    {
                        case -1:
                            lbl_failure.Text = "Username and/or password is incorrect.";
                            break;
                        default:
                            FormsAuthentication.RedirectFromLoginPage(txt_account.Value, true);
                            FormsAuthentication.SetAuthCookie(txt_account.Value, true);
                            Debugger.Log(1,"Login"," "+this.User.Identity.IsAuthenticated);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}