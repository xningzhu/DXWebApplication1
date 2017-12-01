using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Net.Configuration;

namespace ezapp
{
    /// <summary>
    /// 郵件處理函式庫
    /// </summary>
    public class ezMail
    {
        /// <summary>
        /// 寄件者郵件位置
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 寄件者名稱
        /// </summary>
        public string FromName { get; set; }
        /// <summary>
        /// 收件者
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 寄件副本
        /// </summary>
        public string CC { get; set; }
        /// <summary>
        /// 密件副本
        /// </summary>
        public string BCC { get; set; }
        /// <summary>
        /// 郵件主旨
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 郵件內文
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment { get; set; }
        /// <summary>
        /// 郵件內文是否為 HTML 的格式
        /// </summary>
        public bool IsBodyHtml { get; set; }
        /// <summary>
        /// 是否啟用 SSL 機制
        /// </summary>
        public bool EnabledSsl { get; set; }

        /// <summary>
        /// ezMail建構子
        /// </summary>
        public ezMail()
        {
            this.SetInit("", "", "", false, true);
        }
        /// <summary>
        /// ezMail建構子
        /// </summary>
        /// <param name="sTo"></param>
        public ezMail(string sTo)
        {
            this.SetInit(sTo, "", "", false, true);
        }
        /// <summary>
        /// ezMail建構子
        /// </summary>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        public ezMail(string sTo, string sSubject)
        {
            this.SetInit(sTo, sSubject, "", false, true);
        }
        /// <summary>
        /// ezMail建構子
        /// </summary>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sBody"></param>
        public ezMail(string sTo, string sSubject, string sBody)
        {
            this.SetInit(sTo, sSubject, sBody, false, true);
        }
        /// <summary>
        /// ezMail建構子
        /// </summary>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sBody"></param>
        /// <param name="bIsBodyHtml"></param>
        public ezMail(string sTo, string sSubject, string sBody, bool bIsBodyHtml)
        {
            this.SetInit(sTo, sSubject, sBody, bIsBodyHtml, true);
        }
        /// <summary>
        /// ezMail建構子
        /// </summary>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sBody"></param>
        /// <param name="bIsBodyHtml"></param>
        /// <param name="bEnabledSsl"></param>
        public ezMail(string sTo, string sSubject, string sBody, bool bIsBodyHtml, bool bEnabledSsl)
        {
            this.SetInit(sTo, sSubject, sBody, bIsBodyHtml, bEnabledSsl);
        }
        /// <summary>
        /// 設定預設值
        /// </summary>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sBody"></param>
        /// <param name="bIsBodyHtml"></param>
        /// <param name="bEnabledSsl"></param>
        void SetInit(string sTo, string sSubject, string sBody, bool bIsBodyHtml, bool bEnabledSsl)
        {
            FromName = "";
            CC = "";
            BCC = "";
            To = sTo;
            Subject = sSubject;
            Body = sBody;
            IsBodyHtml = bIsBodyHtml;
            EnabledSsl = bEnabledSsl;
            Attachment = "";
        }

        /// <summary>
        /// 發送郵件函數
        /// </summary>
        /// <returns></returns>
        public string SetMail()
        {
            string RetMessage = "";
            try
            {
                System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
                MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
                MailMessage msg = new MailMessage();
                string strFrom = settings.Smtp.From;
                string strFromName = "";
                string strFromNo = settings.Smtp.From;
                char[] FromChars = { '/' };
                char[] ToChars = { ';' };
                char[] AttachChars = { ',', ';', '\t' };
                if (strFrom.IndexOf("/") > 0)
                {
                    string[] strFromNames = strFrom.Split(FromChars);
                    if (FromName == "")
                        strFromName = strFromNames[0];
                    else
                        strFromName = FromName;
                    strFromNo = strFromNames[1];
                }
                else
                {
                    if (FromName != "") strFromName = FromName;
                }
                msg.From = new MailAddress(strFromNo, strFromName, Encoding.UTF8);

                if (To != "")
                {
                    if (To.Substring(To.Length - 1 , 1) != ";") To += ";";
                    string[] Tos = To.Split(ToChars);
                    string strTo = "";
                    string strToName = "";
                    foreach (string strTos in Tos)
                    {
                        strToName = "";
                        if (strTos != "")
                        {
                            strTo = strTos;
                            if (strTo.IndexOf("/") > 0)
                            {
                                string[] strToNames = strTo.Split(FromChars);
                                {
                                    strToName = strToNames[0];
                                    strTo = strToNames[1];
                                }
                            }
                        }
                        msg.To.Add(new MailAddress(strTo, strToName, Encoding.UTF8));
                    }
                }

                if (CC != "")
                {
                    if (CC.Substring(CC.Length - 1, 1) != ";") To += ";";
                    string[] CCs = CC.Split(ToChars);
                    string strCC = "";
                    string strCCName = "";
                    foreach (string strCCs in CCs)
                    {
                        strCCName = "";
                        if (strCCs != "")
                        {
                            strCC = strCCs;
                            if (strCC.IndexOf("/") > 0)
                            {
                                string[] strCCNames = strCC.Split(FromChars);
                                {
                                    strCCName = strCCNames[0];
                                    strCC = strCCNames[1];
                                }
                            }
                        }
                        msg.CC.Add(new MailAddress(strCC, strCCName, Encoding.UTF8));
                    }
                }

                if (BCC != "")
                {
                    if (BCC.Substring(BCC.Length - 1, 1) != ";") To += ";";
                    string[] BCCs = BCC.Split(ToChars);
                    string strBCC = "";
                    string strBCCName = "";
                    foreach (string strBCCs in BCCs)
                    {
                        strBCCName = "";
                        if (strBCCs != "")
                        {
                            strBCC = strBCCs;
                            if (strBCC.IndexOf("/") > 0)
                            {
                                string[] strBCCNames = strBCC.Split(FromChars);
                                {
                                    strBCCName = strBCCNames[0];
                                    strBCC = strBCCNames[1];
                                }
                            }
                        }
                        msg.Bcc.Add(new MailAddress(strBCC, strBCCName, Encoding.UTF8));
                    }
                }

                if (Attachment != "")
                {
                    if (Attachment.Substring(Attachment.Length - 1, 1) != ";") Attachment += ";";

                    string[] AttFile = Attachment.Split(AttachChars);
                    foreach (string strFile in AttFile)
                    {
                        if (strFile != "")
                        {
                            string strFileName = strFile;
                            if (strFile.Substring(0,1) == "~")
                                strFileName = HttpContext.Current.Server.MapPath(strFileName);
                            msg.Attachments.Add(new Attachment(strFileName));
                        }
                    }
                }

                msg.Subject = Subject;
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = Body;
                msg.IsBodyHtml = IsBodyHtml;
                msg.BodyEncoding = Encoding.UTF8;
                msg.Priority = MailPriority.High;

                SmtpClient smtp = new SmtpClient(settings.Smtp.Network.Host, settings.Smtp.Network.Port);
                smtp.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
                //smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = EnabledSsl;
                smtp.Send(msg);
                msg.Dispose();
                smtp.Dispose();
            }
            catch (Exception ex)
            {
                RetMessage = ex.Message;
            }
            return RetMessage;
        }

        //使用方法:
        //ezMail myMail = new ezMail();
        //myMail.From = "johnson.ezmail@gmail.com";
        //myMail.To = "許志強/johnson.ezmail@gmail.com";
        //myMail.Subject = "寄件測試";
        //myMail.Body = "從 ezCloudApp 寄出";
        //myMail.IsBodyHtml = false;
        //myMail.EnabledSsl = true;
        //myMail.Attachment = @"~\files\SendMail.txt";
        //Label1.Text = myMail.SetMail();
    }
}