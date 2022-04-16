using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using RealtyFirmAPI.Data;
using RealtyFirmAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyFirmAPI.Services
{
    public interface IGoogleAuthenticationService
    {
        Task<Broker> AuthenticateBrokerAsync(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
        Task<Bidder> AuthenticateBidderAsync(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload);
    }
    public class GoogleAuthenticationService : IGoogleAuthenticationService
    {
        private readonly RealtyDbContext _context;
        private static IList<Broker> _brokers = new List<Broker>();
        private static IList<Bidder> _bidders = new List<Bidder>();
        public GoogleAuthenticationService(RealtyDbContext context)
        {
            _context = context;
            this.Refresh();
        }
        public async Task<Broker> AuthenticateBrokerAsync(GoogleJsonWebSignature.Payload payload)
        {
            await Task.Delay(1);
            return await FindBrokerAsync(payload);
        }
        public async Task<Bidder> AuthenticateBidderAsync(GoogleJsonWebSignature.Payload payload)
        {
            await Task.Delay(1);
            return await FindBidderAsync(payload);
        }

        private async Task<Broker> FindBrokerAsync(Google.Apis.Auth.GoogleJsonWebSignature.Payload payload)
        {
            Broker b = await _context.Brokers.FirstOrDefaultAsync(x => x.Email == payload.Email); //Kanske kolla google-id också?

            if (b == null) //Fixa med denna, så att man inte bara lägger till en broker...
            {

                //b = new Broker()
                //{
                //    Broker_Id = Guid.NewGuid(),
                //    First_Name = payload.Name.Split(' ')[0],
                //    Last_Name = payload.Name.Split(' ')[1],
                //    Email = payload.Email,
                //    Google_Id = payload.Subject

                //};
                //_brokers.Add(b);
            }
            this.PrintBrokers();

            return b;
        }

        private async Task<Bidder> FindBidderAsync(GoogleJsonWebSignature.Payload payload)
        {
            Bidder b = await _context.Bidders.FirstOrDefaultAsync(x => x.Email == payload.Email);

            return b;
        }
        private void PrintBrokers()
        {
            string s = "";
            foreach (Broker b in _brokers)
            {
                s += $"\n[Google-mail: {b.Email}, Google-id: {b.Google_Id}]";
            }
            Helpers.SimpleLogger.Log(s);
        }
        private void Refresh()
        {
            if (_brokers.Count == 0)
            {
                _brokers = _context.Brokers.ToList();
            }
        }


    }
}
