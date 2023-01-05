using System.IO;
using UnityEngine;
using Newtonsoft.Json;

using SettingsType = System.Collections.Generic.Dictionary<string, int>;

namespace VMC2VMT
{
    public sealed class Settings : MonoBehaviour
    {
        [SerializeField] string fileName;
        SettingsType settings;

        public void ReadSettings(bool force = false)
        {
            if (!force && settings != null) return;
            using var file = File.OpenText(Path.Combine(Application.streamingAssetsPath, fileName));
            using var reader = new JsonTextReader(file);
            settings = JsonSerializer.CreateDefault().Deserialize<SettingsType>(reader);
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