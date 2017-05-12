﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Knapcode.TorSharp.Tools.Tor
{
    public class TorConfigurationDictionary : IConfigurationDictionary
    {
        private readonly string _torDirectoryPath;

        public TorConfigurationDictionary(string torDirectoryPath)
        {
            _torDirectoryPath = torDirectoryPath;
        }

        public IDictionary<string, string> GetDictionary(TorSharpSettings settings)
        {
            var dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "SocksPort", settings.TorSocksPort.ToString(CultureInfo.InvariantCulture) },
                { "ControlPort", settings.TorControlPort.ToString(CultureInfo.InvariantCulture) }
            };

            if (settings.HashedTorControlPassword != null)
            {
                dictionary["HashedControlPassword"] = settings.HashedTorControlPassword;
            }

            if (!string.IsNullOrWhiteSpace(settings.TorDataDirectory))
            {
                dictionary["DataDirectory"] = settings.TorDataDirectory;
            }

            if (settings.TorExitNodes != null)
            {
                dictionary["ExitNodes"] = settings.TorExitNodes;
                dictionary["GeoIPFile"] = Path.Combine(_torDirectoryPath, "Data\\Tor\\geoip");
                dictionary["GeoIPv6File"] = Path.Combine(_torDirectoryPath, "Data\\Tor\\geoip6");
            }

            if (settings.TorStrictNodes != null)
            {
                dictionary["StrictNodes"] = (bool)settings.TorStrictNodes ? "1" : "0";
            }

            if (settings.TorNewCircuitPeriod != null)
            {
                dictionary["NewCircuitPeriod"] = settings.TorNewCircuitPeriod?.ToString(CultureInfo.InvariantCulture);
            }

            if (settings.MaxCircuitDirtiness != null)
            {
                dictionary["MaxCircuitDirtiness"] = settings.TorNewCircuitPeriod?.ToString(CultureInfo.InvariantCulture);
            }
            
            return dictionary;
        }
    }
}