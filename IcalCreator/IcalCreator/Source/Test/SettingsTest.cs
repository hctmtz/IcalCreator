using System;
using IcalCreator.Settings;

namespace IcalCreator.Test
{
    public class SettingsTest
    {
        static void Main(string[] args)
        {
            AppSettingsModel settings = AppSettingsModel.Load();

            DateTime SettingsDatetime = DateTime.SpecifyKind(settings.LastEdit, DateTimeKind.Utc);
            var SettingsLocalDate = SettingsDatetime.ToLocalTime();

            settings.TimeInterval += 10;
            settings.Save();

        }
    }

}
