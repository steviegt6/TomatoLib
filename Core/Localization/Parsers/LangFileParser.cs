using System.Collections.Generic;
using System.IO;
using Terraria.ModLoader;
using TomatoLib.Core.Utilities.Localization;

namespace TomatoLib.Core.Localization.Parsers
{
    public class LangFileParser : ILocalizationFileParser
    {
        public IDictionary<string, ModTranslation> ParseText(Mod mod, string culture, string text,
            Dictionary<string, ModTranslation> translations)
        {
            using StringReader reader = new(text);

            while ((text = reader.ReadLine()) != null)
            {
                int index = text.IndexOf('=');

                if (index < 0)
                    continue;

                string key = text[..index].Trim().Replace(' ', '_');
                string value = text[(index + 1)..];

                if (value.Length == 0)
                    continue;

                value = value.Replace("\\n", "\n");

                if (!translations.TryGetValue(key, out ModTranslation translation))
                    translation = translations[key] = DefaultLocalizationLoader.GetOrCreateTranslation(mod, key);

                translation.AddTranslation(culture, value);
            }

            return translations;
        }
    }
}