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
    public partial class frmSendMailwithContents : System.Web.UI.Page
    {
        #region Data Member
        CreateMail createEmail;
        #endregion

        #region Data structure
        private static class DataField {
            public const string Email = "Email";
            public const string Subject = "Subject";
            public const string Content = "Content";
        }
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

                gridView.DataSource = dt;
                gridView.DataBind();

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
                email.passWordSendMail = ip_txt_pass_email.Value;
         
                if (radio_service_google.Checked)
                {
          
                }
                else if (radio_service_stpm.Checked)
                {
                    for (int i = 0; i < gridView.VisibleRowCount; i++)
                    {
                        if (gridView.GetRowLevel(i) == gridView.GroupCount)
                        {
                            object keyValue = gridView.GetRowValues(i, new string[] {DataField.Email,DataField.Subject, DataField.Content });
                            if (keyValue != null)
                            {
                                Array arr = (Array)keyValue;
                                email.toEmail = arr.GetValue(0).ToString();
                                email.subject = arr.GetValue(1).ToString();
                                email.body = arr.GetValue(2).ToString();

                                STPMService.SendMail(email.fromEmail
                                       , email.passWordSendMail
                                       , email.toEmail
                                       , email.subject
                                       , email.body);
                            }

                        }
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