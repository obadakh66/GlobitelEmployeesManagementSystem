using Microsoft.Extensions.Configuration;
using System.IO;

namespace Globitel.Domain.Common
{
    public class AppConfig
    {
        private readonly string _connectionString = string.Empty;
        private readonly string _issuer = string.Empty;
        private readonly string _audience = string.Empty;
        private readonly string _jWTKey = string.Empty;
        private readonly string _accountSid = string.Empty;
        private readonly string _authToken = string.Empty;
        private readonly string _mobileSMSNumber = string.Empty;

        public AppConfig()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);

            IConfigurationRoot root = configBuilder.Build();
            _issuer = root.GetSection("Jwt").GetSection("Issuer").Value;
            _audience = root.GetSection("Jwt").GetSection("Audience").Value;
            _jWTKey = root.GetSection("Jwt").GetSection("Key").Value;
            _connectionString = root.GetConnectionString("SqlConnection");
            _accountSid = root.GetSection("OTP").GetSection("AccountSid").Value;
            _authToken = root.GetSection("OTP").GetSection("AuthToken").Value;
            _mobileSMSNumber = root.GetSection("OTP").GetSection("MobileNUmber").Value;

        }
        public string ConnectionString
        {
            get => _connectionString;
        }

        public string AccountSid
        {
            get => _accountSid;
        }
        public string AuthToken
        {
            get => _authToken;
        }
        public string MobileNumber
        {
            get => _mobileSMSNumber;
        }
        public string Issuer
        {
            get => _issuer;
        }

        public string Audience
        {
            get => _audience;
        }
        public string JWTKey
        {
            get => _jWTKey;
        }

    }
}
