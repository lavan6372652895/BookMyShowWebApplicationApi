﻿using BookMyShowWebApplicationCommon.Helper;
using BookMyShowWebApplicationModal;
using BookMyShowWebApplicationServices.Interface.Imail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookMyShowWebApplication.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [AllowAnonymous]
    public class MailsController : ControllerBase
    {
        private readonly Imail _mail;
        public MailsController(Imail mail) { 
        _mail = mail;
        }

        [HttpPost]
        public async Task<IActionResult> SendOtp(string email)
        {
            var data = await _mail.sendEmail(email);
            if (data) {

                return Ok(data);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
