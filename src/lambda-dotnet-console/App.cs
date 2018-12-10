using System;

namespace lambda_dotnet_console
{
    public class App
    {
        private AppSettings _settings;

        public App(
            AppSettings settings
        )
        {
            _settings = settings ??
                throw new ArgumentNullException(nameof(settings));
        }

        public string Run(string input)
        {
            return $"{_settings.Prefix}{input?.ToUpper()}";
        }
    }
}