using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.Imail;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using BookMyShowWebApplicationDataAccess.InterFaces.Users;
using NReco.PdfGenerator;
using BookMyShowWebApplicationModal.Users;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
namespace BookMyShowWebApplicationServices.Services.Mail
{
    public class Mail : Imail
    {
        private readonly IConfiguration _configuration;
        private readonly maildto _maildto;
        private static readonly Random _random = new Random();
        private readonly IuserRepo _userrepo;

        public Mail(IConfiguration config,IuserRepo userrepo)
        {
            _configuration = config;

            var mailSettings = _configuration.GetSection("MailSettings");
            _maildto = new maildto
            {
                SenderEmail = mailSettings.GetValue<string>("SenderEmail") ?? string.Empty,
                password = mailSettings.GetValue<string>("Password") ?? string.Empty
            };
            _userrepo = userrepo;
        }

        public async Task<bool> sendEmail(string email, int bookingId = 0)
        {
            bool result = false;
            try
            {
                var mailMessageData = CreateHtml(email, bookingId);
                if (string.IsNullOrEmpty(mailMessageData.FromEmail) || string.IsNullOrEmpty(mailMessageData.ToEmail))
                {
                    throw new ArgumentNullException("Sender or recipient email is missing.");
                }
                using var client = new SmtpClient("smtp-mail.outlook.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_maildto.SenderEmail, _maildto.password)
                };

                using var mailMessage = new MailMessage(mailMessageData.FromEmail, mailMessageData.ToEmail)
                {
                    Subject = mailMessageData.subject,
                    Body = mailMessageData.body,
                    IsBodyHtml = true,
                };
                if (mailMessageData.Pdf != null)
                {
                    mailMessage.Attachments.Add(mailMessageData.Pdf);
                }
                await client.SendMailAsync(mailMessage);
                result = true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                // e.g., Log.Error("Failed to send email", ex);
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }

            return result;
        }

        private Maildata CreateHtml(string email, int bookingId = 0)
        {
            var mailMessageData = new Maildata();

            try
            {
                if (bookingId > 0)
                {
                    var ticket = SendGeneratePdf(bookingId);
                    mailMessageData.Pdf = ticket.Result;
                    mailMessageData.subject = "Your Booking is completed";

                }
                else
                {
                    var otpDto = CreateOtp();
                    mailMessageData.body = $"<html><body><p>Your OTP code is: <strong>{otpDto.otps}</strong>. OTP is valid for only 10 minutes.</p><p>Do not share your OTP with anyone, including your Depository Participant (DP). For any OTP-related queries, please email us at subbu6372@outlook.com</p></body></html>";
                    mailMessageData.subject = "One Time Password (OTP)";
                }

                mailMessageData.FromEmail = _maildto.SenderEmail;
                mailMessageData.ToEmail = email;
               
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                // e.g., Log.Error("Failed to create email body", ex);
                Console.WriteLine($"Failed to create email body: {ex.Message}");
            }

            return mailMessageData;
        }

        private Otpdto CreateOtp()
        {
            var otpDto = new Otpdto();

            try
            {
                otpDto.num = "0123456789";
                otpDto.len = otpDto.num.Length;
                otpDto.otps = string.Empty;
                otpDto.otpDigit = 6;

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
                // Log the exception or handle it accordingly
                // e.g., Log.Error("Failed to generate OTP", ex);
                Console.WriteLine($"Failed to generate OTP: {ex.Message}");
            }

            return otpDto;
        }

        //public  Attachment SendGeneratePdf(int bookingId)
        //{
        //    int userid = 7;
        //    var document = new PdfDocument();
        //    var TicketDetails =  _userrepo.GetTicket(bookingId,userid);

        //    var page = document.Pages.Add();
        //    var graphics = page.Graphics;
        //    var brush = new PdfSolidBrush(Syncfusion.Drawing.Color.AntiqueWhite);
        //    var font = new PdfStandardFont(PdfFontFamily.Helvetica, 20f);
        //    graphics.DrawString("Hello world!", font, brush, new PointF(20, 20));

        //    var memoryStream = new MemoryStream();
        //    document.Save(memoryStream);
        //    document.Close(true);


        //    memoryStream.Position = 0; // Return the attachment
        //    return new Attachment(memoryStream, "PdfAttachment.pdf", "application/pdf");
        //}
        public async Task<Attachment> SendGeneratePdf(int bookingId)
        {
            int userid = 7;
            var TicketDetails =await _userrepo.GetTicket(bookingId, userid).ConfigureAwait(false);
            //var TicketDetails = new TicketDto()
            //{
            //    posterpic = "img_girl.jpg",
            //    Moviename = "RRR",
            //    name = "PVR",
            //    Code = "scree1"
            //};

            // Create a new instance of HtmlToPdfConverter
            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();

            // Generate PDF from HTML content
            var htmlContent = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 0;
            }}
            .ticket {{
                width: 60%; /* Adjust as needed */
                margin: 20px auto;
                padding: 20px;
                background-color: #fff;
                border: 1px solid #ddd;
                box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            }}
            .header {{
                background-color: #ff9900; /* Orange */
                color: #fff;
                padding: 10px;
                text-align: center;
                border-radius: 4px;
            }}
            .content {{
                display: flex;
                justify-content: space-between;
                padding: 20px;
                flex-wrap: wrap;
            }}
            .left-column {{
                width: 30%;
                display: flex;
                align-items: center;
                justify-content: center;
            }}
            .right-column {{
                width: 65%;
                padding-left: 10px;
            }}
            .logo {{
                width: 150px; /* Adjust as needed */
                height: 50px; /* Adjust as needed */
                border-radius: 50%;
                overflow: hidden;
                display: flex;
                align-items: center;
                justify-content: center;
                background-color: #ff9900; /* Orange */
                margin: 10px;
            }}
            .logo img {{
                max-width: 100%;
                height: auto;
            }}
            .film-title {{
                font-size: 18px;
                font-weight: bold;
                margin-bottom: 10px;
            }}
            .details {{
                font-size: 14px;
                color: #666;
            }}
            .footer {{
                background-color: #f7f7f7;
                padding: 10px;
                text-align: center;
                border-top: 1px solid #ddd;
                margin-top: 20px;
                border-radius: 4px;
            }}
        </style>
    </head>
    <body>
        <div class='ticket'>
            <div class='header'>
                <h2>Your Movie Ticket</h2>
            </div>
            <div class='content'>
                <div class='left-column'>
                    <div class='logo'>
                        <img src='{TicketDetails[0].posterpic}' alt='Filmstrip Icon' width='150' height='50'>
                    </div>
                    <p class='film-title'>{TicketDetails[0].Moviename}</p>
                </div>
                <div class='right-column'>
                    <p class='details'>Ticket Number: ABC-{TicketDetails[0].BookingId}</p>
                    <p class='details'>Seat: {TicketDetails[0].SeatNumbers} | Time: {TicketDetails[0].ShowTime} | Date: {TicketDetails[0].showdate}</p>
                </div>
            </div>
            <div class='footer'>
                <h4 class='details'>Please visit again</h4>
                <p class='details'>Website: localhost:4200/.com</p>
            </div>
        </div>
    </body>
    </html>";


            // Generate the PDF document from HTML
            byte[] pdfBytes = htmlToPdfConverter.GeneratePdf(htmlContent);

            // Save the PDF document to a MemoryStream
            var memoryStream = new MemoryStream();
            memoryStream.Write(pdfBytes, 0, pdfBytes.Length);
            memoryStream.Position = 0;

            // Ensure the Stream supports reading
            if (!memoryStream.CanRead)
            {
                throw new InvalidOperationException("Generated PDF stream does not support reading.");
            }

            // Return the attachment
            return new Attachment(memoryStream, $"{TicketDetails[0].Moviename}.pdf", "application/pdf");
        }



    }
}
