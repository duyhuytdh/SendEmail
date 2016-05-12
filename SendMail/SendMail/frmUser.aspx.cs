using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SendMail.Util;
using System.Web.Security;

namespace SendMail
{
    public partial class frmUser : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.Page.User.Identity.IsAuthenticated)
            //{
            //    FormsAuthentication.RedirectToLoginPage();
            //}
        }

        protected void gridView_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["Password"] = Util.Cryption.Encrypt(e.NewValues["Password"].ToString());
            e.NewValues["TimeCreated"] = DateTime.Now;
        }

        protected void gridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["Password"] = Util.Cryption.Encrypt(e.NewValues["Password"].ToString());
        }

        protected void gridView_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            
        }
    }
}