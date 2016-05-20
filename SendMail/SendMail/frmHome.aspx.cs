using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SendMail
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void gridView_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Number")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex +1);
            }
        }

        protected void date_tu_ngay_ButtonClick(object source, DevExpress.Web.ButtonEditClickEventArgs e)
        {
            gridView.DataBind();
        }

        protected void date_den_ngay_ButtonClick(object source, DevExpress.Web.ButtonEditClickEventArgs e)
        {
            gridView.DataBind();
        }

    }
}