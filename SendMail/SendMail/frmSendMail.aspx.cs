using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;

using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System.Data;
using DevExpress.Web;

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
            createEmail = new CreateMail();
            GoogleMailService.initService();
            radio_service_stpm.Checked = true;
        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                EmailSend email = new EmailSend();
                email.fromEmail = ip_txt_from_email.Value;
                email.toEmail = ip_txt_to_email.Value;
                email.passWordSendMail = ip_txt_pass_email.Value;
                email.subject = ip_txt_subject.Value;
                email.body = txt_content_mail.Value;

                if (radio_service_google.Checked)
                {
                    GoogleMailService.sendMail("duyhuytdh@gmail.com", createEmail.createMessage(email.subject
                                                                                                , email.body
                                                                                                , email.fromEmail
                                                                                                , email.toEmail));
                }
                else if (radio_service_stpm.Checked)
                {
                    STPMService.SendMail(email.fromEmail
                                        , email.passWordSendMail
                                        , email.toEmail
                                        , email.subject
                                        , email.body);
                }
                string message = "Gửi email thành công";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
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
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('"+ message+"');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        protected void btnImportList_Click(object sender, EventArgs e)
        {
            try
            {
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
                //checkBoxSelectAll.Checked = true;
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        protected void btnSendListMail_Click(object sender, EventArgs e)
        {
            try
            {
                EmailSend email = new EmailSend();
                email.fromEmail = ip_txt_from_email.Value;
                email.toEmail = ip_txt_to_email.Value;
                email.passWordSendMail = ip_txt_pass_email.Value;
                email.subject = ip_txt_subject.Value;
                email.body = txt_content_mail.Value;
                SelectedValueCollection listEmail = checkBoxListEmail.SelectedValues;
                progressBar.Maximum = listEmail.Count;
                progressBar.Minimum = 0;
                if (radio_service_google.Checked)
                {
                    foreach (string item in listEmail)
                    {
                        email.toEmail = item;
                        GoogleMailService.sendMail("duyhuytdh@gmail.com", createEmail.createMessage(email.subject
                                                                                                , email.body
                                                                                                , email.fromEmail
                                                                                                , email.toEmail));
                    }
                }
                else if (radio_service_stpm.Checked)
                {
                    foreach (String item in listEmail)
                    {
                        email.toEmail = item;
                        STPMService.SendMail(email.fromEmail
                                        , email.passWordSendMail
                                        , email.toEmail
                                        , email.subject
                                        , email.body);
                        progressBar.Position = progressBar.Position + 1;
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Gửi thành công!" + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
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