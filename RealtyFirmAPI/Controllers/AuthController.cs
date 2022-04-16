using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealtyFirmAPI.Data;
using RealtyFirmAPI.Helpers;
using RealtyFirmAPI.Models;
using RealtyFirmAPI.Models.DTO;
using RealtyFirmAPI.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealtyFirmAPI.Controllers
{
    [BrokerAuthorize]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {


        private readonly IGoogleAuthenticationService _authService;
        private readonly RealtyDbContext _context;

        public AuthController(IGoogleAuthenticationService authService, RealtyDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("brokergoogle")]
        public async Task<IActionResult> BrokerGoogle([FromBody] GoogleTokenDTO gtDTO)
        {
            try
            {
                //Nedan verifieras att token är korrekt från google.
                var payload = GoogleJsonWebSignature.ValidateAsync(gtDTO.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
                var broker = await _authService.AuthenticateBrokerAsync(payload);

                SimpleLogger.Log(payload.ExpirationTimeSeconds.ToString());

                if (broker == null)
                {
                    return Forbid(); //Om man inte är broker ska man få förbjuden status.   
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, Security.Encrypt(AppSettings.appSettings.JwtEmailEncryption, broker.Broker_Id.ToString())),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(AppSettings.appSettings.JwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Standardalgoritm för att kryptera...

                var token = new JwtSecurityToken(String.Empty, //Här kan vi undersöka lite...
                                                 String.Empty,
                                                 claims,
                                                 expires: DateTime.Now.AddSeconds(55 * 60),
                                                 signingCredentials: creds);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception epicFail)
            {
                SimpleLogger.Log(epicFail.Message);
                BadRequest(epicFail.Message);
            }


            return BadRequest();
        }

        //[AllowAnonymous]
        //[HttpPost("biddergoogle")]
        //public async Task<IActionResult> BidderGoogle([FromBody] GoogleTokenDTO gtDTO)
        //{
        //    try
        //    {
        //        //Nedan verifieras att token är korrekt från google.
        //        var payload = GoogleJsonWebSignature.ValidateAsync(gtDTO.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
        //        var bidder = await _authService.AuthenticateBidderAsync(payload);

        //        SimpleLogger.Log(payload.ExpirationTimeSeconds.ToString());

        //        if (bidder == null)
        //        {
        //            return Forbid(); //Om man inte är broker ska man få förbjuden status.   
        //        }

        //        var claims = new[]
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, Security.Encrypt(AppSettings.appSettings.JwtEmailEncryption, bidder.Bidder_Id.ToString())),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        };

        //        var key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(AppSettings.appSettings.JwtSecret));
        //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Standardalgoritm för att kryptera...

        //        var token = new JwtSecurityToken(String.Empty, //Här kan vi undersöka lite...
        //                                         String.Empty,
        //                                         claims,
        //                                         expires: DateTime.Now.AddSeconds(55 * 60),
        //                                         signingCredentials: creds);
        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token)
        //        });
        //    }
        //    catch (Exception epicFail)
        //    {
        //        SimpleLogger.Log(epicFail.Message);
        //        BadRequest(epicFail.Message);
        //    }


        //    return BadRequest();
        //}
        [AllowAnonymous]
        [HttpPost("brokerpass")]
        public async Task<IActionResult> BrokerPass([FromBody] UserDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            try
            {

                string password = Security.Encrypt(AppSettings.appSettings.JwtSecret, user.Password);
                Broker b = await _context.Brokers.FirstOrDefaultAsync(b => b.Email == user.Email && b.Password == password);

                if (b != null)
                {
                    var claims = new[]
                    {
                        new Claim( JwtRegisteredClaimNames.Sub, Security.Encrypt(AppSettings.appSettings.JwtEmailEncryption, b.Broker_Id.ToString())),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.Unicode.GetBytes(AppSettings.appSettings.JwtSecret));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var Token = new JwtSecurityToken(
                        String.Empty,
                        String.Empty,
                        claims,
                        expires: DateTime.Now.AddSeconds(55 * 60),
                        signingCredentials: creds);
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(Token) });
                }
            }
            catch (Exception epicFail)
            {
                return BadRequest(epicFail.Message);
            }
            return Forbid();
        }

        //[AllowAnonymous]
        //[HttpPost("testavkrypto")]
        //public async Task<IActionResult> TestAvKrypto([FromBody]Message message)
        //{

        //    try
        //    {

        //        string s = Security.Encrypt(AppSettings.appSettings.JwtSecret, message.s);
        //        return Ok(s);

        //    }
        //    catch (Exception epicFail)
        //    {
        //        return BadRequest(epicFail.Message);
        //    }
        //    return Forbid();
        //}


    }

    //public class Message
    //{
    //    public string s { get; set; }
    //}


}
