using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

using SettingsType = System.Collections.Generic.Dictionary<string, int>;

namespace VMC2VMT
{
    public sealed class Settings : MonoBehaviour
    {
        [SerializeField] string fileName;

        [SerializeField] Logger logger;

        SettingsType settings;

        public void ReadSettings(bool force = false)
        {
            if (!force && settings != null) return;
            try
            {
                using var file = File.OpenText(Path.Combine(Application.streamingAssetsPath, fileName));
                using var reader = new JsonTextReader(file);
                settings = JsonSerializer.CreateDefault().Deserialize<SettingsType>(reader);
            }
            catch (Exception e)
            {
                logger.AddLogError(e.Message);
                throw;
            }
        }

        public bool TryGet(string name, out int value)
        {
            if (settings == null)
            {
                value = default;
                return false;
            }

            return settings.TryGetValue(name, out value);
        }
    }
}