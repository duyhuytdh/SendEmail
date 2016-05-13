using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SendMail
{
    public partial class frmEmailOwn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gridView_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            e.NewValues["Password"] = Util.Cryption.Encrypt(e.NewValues["Password"].ToString());
        }

        protected void gridView_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            e.NewValues["Password"] = Util.Cryption.Encrypt(e.NewValues["Password"].ToString());
        }
    }
}