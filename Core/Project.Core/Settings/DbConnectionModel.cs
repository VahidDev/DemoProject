namespace Project.Core.Settings
{
    public class DbConnectionModel
    {
        public string ServerName { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool TrustedConnection { get; set; }

        public override string ToString()
        {
            return $"Server={ServerName};Database={Database};"
                + (TrustedConnection ? "TrustServerCertificate=True;Trusted_Connection=True;"
                : $"User Id={Username};Password={Password};Encrypt=False");
        }
    }
}
