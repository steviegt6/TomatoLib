using System.Collections.Generic;
using Terraria.ModLoader;
using TomatoLib.Core.Utilities.Localization;
using Tomlet;
using Tomlet.Models;

namespace TomatoLib.Core.Localization.Parsers
{
    public class TomlFileParser : ILocalizationFileParser
    {
        public IDictionary<string, ModTranslation> ParseText(Mod mod, string culture, string text,
            Dictionary<string, ModTranslation> translations)
        {
            TomlDocument document = new TomlParser().Parse(text);

            foreach ((string s, TomlValue tomlValue) in document.Entries)
            {
                Dictionary<string, string> values = new();
                GetValues(s, tomlValue, values);

                foreach (KeyValuePair<string, string> tr in values)
                {
                    string key = tr.Key;
                    string value = tr.Value;
                    value = value.Replace("\\n", "\n");

                    if (!translations.TryGetValue(key, out ModTranslation translation))
                        translation = translations[key] = DefaultLocalizationLoader.GetOrCreateTranslation(mod, key);

                    translation.AddTranslation(culture, value);
                }
            }

            return translations;
        }

        public void GetValues(string key, TomlValue toml, Dictionary<string, string> values)
        {
            if (toml is TomlTable table)
            {
                foreach ((string s, TomlValue value) in table.Entries)
                    // key + "." + kvp.Key is needed to the name of the table is added to the translations key
                    GetValues(key + "." + s, value, values);

                return;
            }

            values.Add(key, toml.StringValue);
        }
    }
}