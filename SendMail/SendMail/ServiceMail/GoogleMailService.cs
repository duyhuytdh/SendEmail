using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


namespace SendMail.ServiceMail
{
    public class GoogleMailService
    {
        private static string[] Scopes = { GmailService.Scope.MailGoogleCom };
        private static string ApplicationName = "Send Mail Mobilink";
        private static GmailService service;
        public static Message message;

        public static void  initService()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret_orther.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-sendmail-mobilink.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,

            });
            Debugger.Log(1, "Google API", "Success!");
        }

        public static Message sendMail(String userId, Message email)
        {
            try
            { 
                return service.Users.Messages.Send(email, userId).Execute();
            }
            catch (Exception v_e)
            {

                Debugger.Log(1, "Send Mail", "Failed: "+v_e);
            }
            return null;
        }
    }
}