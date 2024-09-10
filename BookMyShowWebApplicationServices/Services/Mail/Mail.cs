using BingMapsRESTToolkit;
using BookMyShowWebApplicationDataAccess.InterFaces.Email;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.Imail;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationServices.Services.Mail
{
    public class Mail : Imail
    {
        private readonly IConfiguration _configuration;
        private readonly maildto _maildto;
        private static readonly Random _random = new Random();

        public Mail(IConfiguration config)
        {
            _configuration = config;
           
            var mailSettings = _configuration.GetSection("MailSettings");
            _maildto = new maildto()
            {
                SenderEmail = mailSettings.GetValue<string>("SenderEmail") ?? string.Empty,
                password = mailSettings.GetValue<string>("Password") ?? string.Empty
            };
        }

        public async Task<bool> sendOtp(string email)
        {
            bool result = false;
            try
            {
                var mailMessageData = CreateHtmlBody(email);
                if (string.IsNullOrEmpty(mailMessageData.FromEmail) || string.IsNullOrEmpty(mailMessageData.ToEmail))
                {
                    throw new ArgumentNullException("Sender or recipient email is missing.");
                }

                using (var client = new SmtpClient("smtp-mail.outlook.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_maildto.SenderEmail, _maildto.password)
                })
                {
                    var mailMessage = new MailMessage(mailMessageData.FromEmail, mailMessageData.ToEmail)
                    {
                        Subject = mailMessageData.subject,
                        Body = mailMessageData.body,
                        IsBodyHtml = true
                    };
                    await client.SendMailAsync(mailMessage);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
            return result;
        }

        private Maildata CreateHtmlBody(string email)
        {
            Maildata mailMessageData = new Maildata();
            try
            {
                var otpDto = CreateOtp();
                mailMessageData.FromEmail = _maildto.SenderEmail;
                mailMessageData.ToEmail = email;
                mailMessageData.subject = "One Time Password (OTP)";
                mailMessageData.body = $"<html><body><p>Your OTP code is: <strong>{otpDto.otps}</strong>. OTP is valid for only 10 minutes.</p><p>Do not share your OTP with anyone, including your Depository Participant (DP). For any OTP-related queries, please email us at subbu6372@outlook.com</p></body></html>";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create email body: {ex.Message}");
            }
            return mailMessageData;
        }

        private Otpdto CreateOtp()
        {
            Otpdto otpDto = new Otpdto();
            try
            {
                otpDto.num = "0123456789";
                otpDto.len = otpDto.num.Length;
                otpDto.otps = string.Empty;
                otpDto.otpDigit = 2;

                for (int i = 0; i < otpDto.otpDigit; i++)
                {
                    string finalDigit;
                    do
                    {
                        int index = _random.Next(0, otpDto.len);
                        finalDigit = otpDto.num[index].ToString();
                    } while (otpDto.otps.Contains(finalDigit));

                    otpDto.otps += finalDigit;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to generate OTP: {ex.Message}");
            }
            return otpDto;
        }

    }
}
