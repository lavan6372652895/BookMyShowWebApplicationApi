using BookMyShowWebApplicationModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationDataAccess.InterFaces.Email
{
    public interface Iemail
    {
        Task<List<OtpVerification>> addOtp(OtpVerification otp);
    }
}
