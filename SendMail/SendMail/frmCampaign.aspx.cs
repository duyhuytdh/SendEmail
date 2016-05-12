using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SendMail.Models;
using System.Web.Security;

namespace SendMail
{
    public partial class frmCampaign : System.Web.UI.Page
    {
        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        private void loadDatatoGrid()
        {

        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                SendMailEntities db = new SendMailEntities();
                Campaign campaign = new Campaign();
                campaign.CampaignName = txt_campaign_name.Value.ToString();
                campaign.Description = txt_desciption.Value.ToString();
                campaign.isActive = true;
                db.Campaigns.Add(campaign);
                db.SaveChanges();
                gridCampaign.DataBind();
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
            }
        }

        #endregion
    }
}