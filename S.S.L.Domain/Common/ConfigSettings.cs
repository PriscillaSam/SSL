using System;
using System.Configuration;

namespace S.S.L.Domain.Common
{

    public class ConfigSettings
    {
        public SmtpSettings Smtp = new SmtpSettings();
        public UserSettings User = new UserSettings();
        public DomainSettings Domain = new DomainSettings();

        private static readonly Lazy<ConfigSettings> Settings = new Lazy<ConfigSettings>(() => new ConfigSettings(), true);

        public static ConfigSettings Instance => Settings.Value;

        public sealed class UserSettings
        {
            public string Username => GetValue(nameof(Username));
            public string Password => GetValue(nameof(Password));
        }

        public sealed class SmtpSettings
        {
            public int Port => int.Parse(GetValue($"Smtp:{nameof(Port)}"));
            public string Host => GetValue($"Smtp:{nameof(Host)}");
            public bool EnableSsl => bool.Parse(GetValue($"Smtp:{nameof(EnableSsl)}"));
            public string Sender => GetValue($"Smtp:{nameof(Sender)}");

        }

        public sealed class DomainSettings
        {
            public string EmailPath => GetValue(nameof(EmailPath));
            public string EmailConfirmUrl => GetValue(nameof(EmailConfirmUrl));
        }
        private static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }

}
