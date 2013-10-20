using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_of_Cards.Resources
{
    class PersistentStorage
    {
        public static void addValueToProperites(string key, string value)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(key))
                settings[key] = value;
            else
                settings.Add(key, value);
        }
        public static string getValueForKey(string key)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(key))
                return settings[key] as string;
            return "";
        }
        public static void removeValueForKey(string key)
        {
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(key))
                settings.Remove(key);
        }
    }
}
