namespace RealtyFirmAPI.Models
{
    public class AppSettings
    {
        public static AppSettings appSettings { get; set; } //Skulle helst skriva det här på ett annat sätt. Men intressant sätt att 
                                                            //instatiera en statisk klass som inte är statisk?? Dialekter osv.
        public string JwtSecret { get; set; }
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string JwtEmailEncryption { get; set; }
    }
}
