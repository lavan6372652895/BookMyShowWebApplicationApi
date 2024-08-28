using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShowWebApplicationModal
{
    public class Otpdto
    {
        
            public string finalDigit;

            public int getIndex { get; set; }

            public string num { get; set; }
            public int len { get; set; }
            public string otps { get; set; }
            public int otpDigit { get; set; }
    }

        public class OtpVerification
        {
           public int otpid { get; set; }
            public string otp { get; set; }
            public DateTime otptime { get; set; }
            public string Email { get; set; }
        }
}

