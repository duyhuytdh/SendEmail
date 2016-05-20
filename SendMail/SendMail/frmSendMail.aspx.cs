using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;

using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System.Data;
using DevExpress.Web;
using System.Web.Security;
using SendMail.Util;
using System.Collections.Generic;
using SendMail.Business;
using System.Linq;

namespace SendMail
{
    public partial class frmSendMail : System.Web.UI.Page
    {

        #region Data Member
        CreateMail createEmail;
        #endregion

        #region Private Method
        #endregion

        #region Publuec Method
        #endregion

        #region Event
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                createEmail = new CreateMail();
                GoogleMailService.initService();
                radio_service_stpm.Checked = true;
                
                if (!IsPostBack && !IsCallback)
                {
                    cmbCampaign.SelectedIndex = 0;
                    cmbEmailOwn.SelectedIndex = 1;
                }
            }
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                using (SendMailEntities db = new SendMailEntities())
                {
                    ListEditItem cmbEmailOwnselectedItem = cmbEmailOwn.SelectedItem;
                    ListEditItem cmbcmbContactselectedItem = cmbContact.SelectedItem;
                    ListEditItem cmbcmbCampaignselectedItem = cmbCampaign.SelectedItem;
                    string toEmail = cmbcmbContactselectedItem.GetValue("Email").ToString();
                    string fromEmail = cmbEmailOwnselectedItem.GetValue("Email").ToString();
                    string password = Cryption.Decrypt(cmbEmailOwnselectedItem.GetValue("Password").ToString());
                    Int64 IdEmailOwn = Int64.Parse(cmbEmailOwnselectedItem.GetValue("ID").ToString());
                    //EmailOwn emailOwn = db.EmailOwns.FirstOrDefault(x => x.ID == IdEmailOwn);
                    LogSendEmail email = new LogSendEmail();
                    email.Subject = ip_txt_subject.Value;
                    email.Body = txt_content_mail.Value;
                    email.ContactID = Int64.Parse(cmbcmbContactselectedItem.GetValue("ContactID").ToString());
                    email.UserID = mGlobal.UserID;
                    email.IDEmailOwn = IdEmailOwn;
                    email.CampaignID = Int64.Parse(cmbcmbCampaignselectedItem.GetValue("CampaignID").ToString());

                    if (radio_service_google.Checked)
                    {
                        email.TypeServiceUsed = mGlobal.GOOGLE;
                        try
                        {
                            GoogleMailService.sendMail("duyhuytdh@gmail.com", createEmail.createMessage(email.Subject
                                                                                                    , email.Body
                                                                                                    , fromEmail
                                                                                                    , toEmail));
                            email.StatusSend = true;
                        }
                        catch (Exception)
                        {
                            email.StatusSend = false;
                            
                        }
                        
                        
                    }
                    else if (radio_service_stpm.Checked)
                    {
                        email.TypeServiceUsed = mGlobal.STPM;
                        try
                        {
                            STPMService.SendMail(fromEmail
                                           , password
                                           , toEmail
                                           , email.Subject
                                           , email.Body);
                            email.StatusSend = true;
                        }
                        catch (Exception)
                        {

                            email.StatusSend = false;
                        }
                       
                    }
                    email.TimeSend = DateTime.Now;
                    db.LogSendEmails.Add(email);
                    db.SaveChanges();
                    string message = "Gửi email thành công";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    txtNameFileUpload.Text = FileName;
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);
                }
                else
                {
                    String message = "Bạn chưa chọn file hoặc file này đang được mở!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
            }
        }

        protected void btnImportList_Click(object sender, EventArgs e)
        {
            try
            {
                List<Contact> lst_contact = new List<Contact>();
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FileName = txtNameFileUpload.Text;
                string Extension = Path.GetExtension(FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);

                DataTable dt = ImportExcel.ImportExcel2DataTable(FilePath, Extension);

                checkBoxListEmail.DataSource = dt;
                checkBoxListEmail.DataMember = "Email";
                checkBoxListEmail.TextField = "Email";
                checkBoxListEmail.DataBind();
                checkBoxListEmail.SelectAll();

                //check save contact new
                SelectedValueCollection listEmail = checkBoxListEmail.SelectedValues;
                foreach (String email in listEmail)
                {
                    if (!ContactBusiness.checkContactIsExist(email) && email!="")
                    {
                        Contact contact = new Contact();
                        contact.Email = email;
                        lst_contact.Add(contact);
                    }
                }

                using (SendMailEntities db = new SendMailEntities())
                {
                    db.Contacts.AddRange(lst_contact);
                    db.SaveChanges();
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
            }
        }

        protected void btnSendListMail_Click(object sender, EventArgs e)
        {
            try
            {
                using (SendMailEntities db = new SendMailEntities())
                {
                    ListEditItem cmbEmailOwnselectedItem = cmbEmailOwn.SelectedItem;
                    ListEditItem cmbcmbCampaignselectedItem = cmbCampaign.SelectedItem;
                    Int64 IdEmailOwn = Int64.Parse(cmbEmailOwnselectedItem.GetValue("ID").ToString());
                    string fromEmail = cmbEmailOwnselectedItem.GetValue("Email").ToString();
                    string password = Cryption.Decrypt(cmbEmailOwnselectedItem.GetValue("Password").ToString());
                    
                    List<LogSendEmail> lst_logEmail = new List<LogSendEmail>();

                    SelectedValueCollection listEmail = checkBoxListEmail.SelectedValues;
                    foreach (String item in listEmail)
                    {
                        //get contact
                        Contact contact = db.Contacts.FirstOrDefault(x=>x.Email== item);
                        LogSendEmail email = new LogSendEmail();
                        email.Subject = ip_txt_subject.Value;
                        email.Body = txt_content_mail.Value;
                        email.ContactID = contact.ContactID;
                        email.UserID = mGlobal.UserID;
                        email.TimeSend = DateTime.Now;
                        email.IDEmailOwn = IdEmailOwn;
                        email.CampaignID = Int64.Parse(cmbcmbCampaignselectedItem.GetValue("CampaignID").ToString());

                        if (radio_service_google.Checked)
                        {
                            email.TypeServiceUsed = mGlobal.GOOGLE;
                            try
                            {
                                GoogleMailService.sendMail("duyhuytdh@gmail.com", createEmail.createMessage(email.Subject
                                                                                                            , email.Body
                                                                                                            , fromEmail
                                                                                                            , item));
                                email.StatusSend = true;
                            }
                            catch (Exception)
                            {

                                email.StatusSend = false;
                            }
                          
                        
                            
                        }
                        else if (radio_service_stpm.Checked)
                        {
                            email.TypeServiceUsed = mGlobal.STPM;
                            try
                            {
                                STPMService.SendMail(fromEmail
                                                    , password
                                                    , item
                                                    , email.Subject
                                                    , email.Body);
                                email.StatusSend = true;
                            }
                            catch (Exception)
                            {
                                email.StatusSend = false;
                            }
                              
                        }

                        lst_logEmail.Add(email);
                    }
                    db.LogSendEmails.AddRange(lst_logEmail);
                    db.SaveChanges();
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Gửi thành công!" + "');", true);
                }
                
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
            }
        }

        protected void CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (checkBoxSelectAll.Checked == true)
                //{
                //    checkBoxListEmail.SelectAll();
                //}
                //else
                //{
                //    checkBoxListEmail.UnselectAll();
                //}

            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        #endregion

    }
}