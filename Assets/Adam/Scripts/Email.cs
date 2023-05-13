using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Email : MonoBehaviour
{
    private string senderEmail = "augmentedunityemail@gmail.com";
    private string senderPassword = "hwhetecvhvcooeig";
    private string recipientEmail = "augmentedcornell@gmail.com";
    private string subject = "AugmentED Web App Quiz Score";
    private string body = "Hello, world!";

    public void SendEmail(string content)
    {
        MailMessage mail = new MailMessage(senderEmail, recipientEmail);
        mail.Subject = subject;
        
        // device
        string deviceModel = " - Device Model: " + SystemInfo.deviceModel;
        string deviceName = " - Device Name: " + SystemInfo.deviceName;
        string deviceType = " - Device Type: " + SystemInfo.deviceType.ToString();
        string deviceOS = " - Device OS: " + SystemInfo.operatingSystem;
        string deviceId = " - Device ID: " + SystemInfo.deviceUniqueIdentifier;
        
        
        mail.Body = content + "\n" + deviceId + "\n" + deviceName + "\n" + 
                    deviceModel + "\n" + deviceOS + "\n" + deviceType;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new NetworkCredential(senderEmail, senderPassword) as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = 
            (sender, certificate, chain, sslPolicyErrors) => true;

        smtpServer.Send(mail);
        Debug.Log("Email sent!");
    }

}
