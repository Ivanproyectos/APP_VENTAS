using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Mime;
using Capa_Entidad; 

namespace App_Ventas.Recursos.Clases
{
    public class Css_Mail
    {
        public class MailHelper
        {
     

             public static void SendMailMessage(ref Cls_Ent_Auditoria auditoria, string recepient, string bcc, string cc, string subject, string body,
                                                            string strNomArchivo, string strRutaAdjunto = "", byte[] ArchivoByte=null,string RutaLogo ="", string sfrom = "")
            {
                string[] arrRecepient = null;

                if (recepient.IndexOf(",") > -1)
                {
                    arrRecepient = recepient.Split(',');
                }
                else if (recepient.IndexOf(";") > -1)
                {
                    arrRecepient = recepient.Split(';');
                }
                else
                {
                    arrRecepient = new string[1];
                    arrRecepient[0] = recepient;
                }
                System.IO.Stream streamArchivo = null;
                SendMailMessage_Private(ref auditoria, arrRecepient, bcc, cc, subject, body, ref streamArchivo, strNomArchivo, strRutaAdjunto, ArchivoByte,RutaLogo, sfrom);
            }

             public static void SendMailMessage(ref Cls_Ent_Auditoria auditoria, string[] recepient, string bcc, string cc, string subject, string body,string strNomArchivo, string strRutaAdjunto = "", byte[] ArchivoByte = null,string RutaLogo ="", string sfrom = "")
            {
                System.IO.Stream streamArchivo = null;
                SendMailMessage_Private(ref auditoria, recepient, bcc, cc, subject, body, ref streamArchivo, strNomArchivo, strRutaAdjunto, ArchivoByte,RutaLogo, sfrom);
            }


        private static void SendMailMessage_Private(ref Cls_Ent_Auditoria auditoria, string[] recepient, string bcc, string cc, string subject,
                                                        string body, ref System.IO.Stream streamArchivo, string strNomArchivo, string strRutaAdjunto,byte[] ArchivoByte,string RutaLogo,
                                                        string sfrom)
            {
                try
                {
                    MailMessage mMailMessage = new MailMessage();
                    SmtpClient mSmtpClient = new SmtpClient();

                    if ((sfrom != null) & sfrom != string.Empty)
                    {
                        if (!string.IsNullOrWhiteSpace(sfrom))
                            mMailMessage.From = new MailAddress(sfrom);
                    }

                    foreach (string unmail in recepient)
                    {
                        if (!string.IsNullOrWhiteSpace(unmail))
                            mMailMessage.To.Add(new MailAddress(unmail));
                    }

                    if ((bcc != null) & bcc != string.Empty)
                    {
                        //if (!string.IsNullOrWhiteSpace(bcc))
                        //    mMailMessage.Bcc.Add(new MailAddress(bcc));
                        string[] correos = bcc.Split(';');
                        foreach (string correo in correos)
                        {
                            mMailMessage.Bcc.Add(new MailAddress(correo));
                        }
                    }

                    if ((cc != null) & cc != string.Empty)
                    {
                        if (!string.IsNullOrWhiteSpace(cc))
                            mMailMessage.CC.Add(new MailAddress(cc));
                    }

                    if (mMailMessage.To.Count == 0 & mMailMessage.CC.Count == 0 & mMailMessage.Bcc.Count == 0)
                    {
                        var CodigoLog = Recursos.Clases.Css_Log.Guardar("Parametros vacios en correo.");
                        auditoria.Rechazar(CodigoLog);
                    }

                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s
                       , X509Certificate certificate
                       , X509Chain chain
                       , SslPolicyErrors sslPolicyErrors)

                    { return true; };

                    mMailMessage.Subject = subject;
                    //mMailMessage.Body = body;
                    //mMailMessage.Body = body.Replace(System.Environment.NewLine, "<br/>");
                    mMailMessage.IsBodyHtml = true;
                    mMailMessage.Priority = MailPriority.Normal;

                    if (!string.IsNullOrWhiteSpace(strRutaAdjunto))
                    {
                        System.Net.Mail.Attachment adjunto = new System.Net.Mail.Attachment(strRutaAdjunto);
                        mMailMessage.Attachments.Add(adjunto);
                    }

                    if ((streamArchivo != null) & !(string.IsNullOrWhiteSpace(strNomArchivo)))
                    {
                        System.Net.Mail.Attachment adjunto2 = new System.Net.Mail.Attachment(streamArchivo, strNomArchivo);
                        mMailMessage.Attachments.Add(adjunto2);
                    }

                    if ((ArchivoByte != null) )
                    {
                        mMailMessage.Attachments.Add(new Attachment(new MemoryStream(ArchivoByte), strNomArchivo));
                        //System.Net.Mail.Attachment adjunto2 = new System.Net.Mail.Attachment(streamArchivo, strNomArchivo);
                        //mMailMessage.Attachments.Add(adjunto2);
                    }
                    
                   
                    // logo 
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
                    try
                    {
                        LinkedResource imgLogo = new LinkedResource(RutaLogo, MediaTypeNames.Image.Jpeg);
                        imgLogo.ContentId = "imgenlogo";
                        htmlView.LinkedResources.Add(imgLogo);
                        mMailMessage.AlternateViews.Add(htmlView);

                    }
                    catch (Exception err)
                    {
                        var CodigoLog = Recursos.Clases.Css_Log.Guardar("ERROR: Al exportar el archivo escaneado... " + err.ToString());
                        auditoria.Rechazar(CodigoLog);
                    }

                    //mSmtpClient.Add(body);

                    mMailMessage.Priority = MailPriority.Normal;
                    mSmtpClient.Host = ConfigurationManager.AppSettings.Get("IpMail");
                    mSmtpClient.Port = int.Parse(ConfigurationManager.AppSettings.Get("PortMail").ToString());
                    string UserMail, ClaveMail;
                    UserMail = ConfigurationManager.AppSettings.Get("UserMail");
                    ClaveMail = ConfigurationManager.AppSettings.Get("ClaveMail");
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.Credentials = new NetworkCredential(UserMail, ClaveMail);
                    mSmtpClient.EnableSsl = true;
                    mSmtpClient.Send(mMailMessage);
                }
                catch (Exception ex)
                {
                    var CodigoLog = Recursos.Clases.Css_Log.Guardar(ex.Message.ToString());
                    auditoria.Rechazar(CodigoLog);
                }
            }
        }

        public static string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);
            sbText.Replace("\r\n", string.Empty); sbText.Replace(" ", string.Empty);
            return sbText.ToString();
     }
   }
}