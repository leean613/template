﻿namespace Common.Configurations
{
    public class SmtpConfig
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool UseSSL { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
