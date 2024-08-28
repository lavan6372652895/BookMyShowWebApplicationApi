using BingMapsRESTToolkit;
using BookMyShowWebApplicationDataAccess.InterFaces.Email;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.Imail;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using SmtpClient = System.Net.Mail.SmtpClient;
namespace BookMyShowWebApplicationServices.Services.Mail
{
    public class Mail : Imail
    {
        private readonly IConfiguration _configuration;
        public maildto maildto;
        public Iemail _email;
        public Mail(IConfiguration config,Iemail email)
        {
            _configuration = config;
            var MailSettings = _configuration.GetSection("MailSettings");
            maildto =new maildto()
            {
                SenderEmail= MailSettings.GetValue<string>("SenderEmail") ?? string.Empty,
                password= MailSettings.GetValue<string>("password") ?? string.Empty,
            };
            
        }

        public async Task<bool> sendOtp(string email)
        {
            bool result = false;
            try
            {
                var mailMessagedata = CreateHtmlBoby(email);
                var client = new SmtpClient("smtp-mail.outlook.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(maildto.SenderEmail,maildto.password)
                };

                var mailMessage = new MailMessage(mailMessagedata.FromEmail??string.Empty, mailMessagedata.ToEmail??string.Empty )
                {
                    Subject = mailMessagedata.subject,
                    Body = mailMessagedata.body,
                    IsBodyHtml = true
                };
                await client.SendMailAsync(mailMessage);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
              
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
            return result;
        }
        private  Maildata CreateHtmlBoby(string Email)
        {
            Maildata mailMessagedata = new Maildata();
            try
            {
                var otpdto = CreateOtp();
                mailMessagedata.FromEmail = maildto.SenderEmail;
                mailMessagedata.ToEmail = Email;
                mailMessagedata.subject = "One Time Password(OTP)";
                mailMessagedata.body = $"<html><body><p>Your OTP code is: <strong>{otpdto.otps}</strong> Otp is valid only 10min</p><p>Do not share your OTP with anyone including your   Depository Participant (DP).For any OTP related query please email us at subbu6372@outlook.com</p></body></html>";
                return mailMessagedata;
            }
            catch {
                
                return mailMessagedata;
            }
        }

        private Otpdto CreateOtp()
        {
            Otpdto otpdto = new Otpdto();
            try
            {
                otpdto.num = "01223456789";
                otpdto.len = otpdto.num.Length;
                otpdto.otps = String.Empty;
                otpdto.otpDigit = 2;
                for (int i = 0; i < otpdto.otpDigit; i++)
                {
                    do
                    {
                        otpdto.getIndex = new Random().Next(0, otpdto.len);
                        otpdto.finalDigit = otpdto.num.ToCharArray()[otpdto.getIndex].ToString();
                    } while (otpdto.otps.IndexOf(otpdto.finalDigit) != -1);
                    otpdto.otps += otpdto.finalDigit;
                }
                return otpdto;
            }
            catch {
                return otpdto;
            }
            

        }




    }
}
