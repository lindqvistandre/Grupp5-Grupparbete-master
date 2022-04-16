using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealtyFirmAPI.Data;
using RealtyFirmAPI.Helpers;
using RealtyFirmAPI.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
namespace RealtyFirmAPI.Services
{
    public class BidderAuthorizeAttribute : TypeFilterAttribute
    {
        public BidderAuthorizeAttribute() : base(typeof(BidderAuthFilter)) { }
    }
    public class BidderAuthFilter : IAuthorizationFilter
    {
        private readonly RealtyDbContext _context;
        public BidderAuthFilter(RealtyDbContext context)
        {
            _context = context;
        }
        public async void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var action = filterContext.ActionDescriptor;

            bool hasAllowAnonymous = filterContext.ActionDescriptor.EndpointMetadata
                                 .Any(em => em.GetType() == typeof(AllowAnonymousAttribute)); //< -- Here it is

            if (hasAllowAnonymous)
                return;

            try
            {
                var headers = filterContext.HttpContext.Request.Headers;
                if (!headers.ContainsKey("Authorization"))
                {
                    filterContext.Result = new StatusCodeResult(403);
                }
                var authHeader = headers["Authorization"].ToString();

                if (!authHeader.StartsWith("Bearer ") && authHeader.Length > 7)
                {
                    filterContext.Result = new StatusCodeResult(403);
                }
                var handler = new JwtSecurityTokenHandler();

                var token = handler.ReadJwtToken(authHeader.Remove(0, 7));
                int? now = new JwtSecurityToken(expires: DateTime.Now).Payload.Exp; //Enklaste sättet jag fann. jwt lagrar tiden som ett för mig okänt format
                if (token.Payload.Exp < now) //Kollar att token inte har gått ut...
                {
                    filterContext.Result = new StatusCodeResult(403);
                }

                string id = Security.Decrypt(AppSettings.appSettings.JwtEmailEncryption, token.Payload.Sub);
                //Ovan kan den dekryptera guid som finns i token. Samma nyckel som när den skulle krypteras.
                Bidder b = _context.Bidders.Find(Guid.Parse(id));
                if (b == null)
                {
                    filterContext.Result = new StatusCodeResult(403);
                }
                filterContext.HttpContext.Items["extractId"] = id;

            }
            catch { filterContext.Result = new StatusCodeResult(403); }
        }
    }
}
