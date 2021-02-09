using System;
using System.Collections.Generic;

namespace SimplyTrivia.Helpers
{
    /// <summary>
    /// This class contains a few helpful methods to assist with getting
    /// the responses from the API and deserializing them into usable classes
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// This dictionary replaces the encoded value (key) with the equivalent normal character (value)
        /// </summary>
        public static readonly Dictionary<string, string> encodedCharacters = new Dictionary<string, string>()
        {
            { "&quot;", "'" },
            { "&#039;", "'" },
            { "&ldquo;", "'" },
            { "&rdquo;", "'" },
            { "&Eacute;", "E" },
			{ "&eacute", "e" },
			{ "&ouml;", "o" },
            { "&ocirc", "o" },
            { "&Uuml;", "U" },
            { "&uuml;", "u" },
            { "&lt;", "<" },
            { "&gt;", ">" },
            { "&amp;", "&"},
        };

        /// <summary>
        /// Replace encoded characters with a normal character equivalent 
        /// </summary>
        /// <param name="json"></param>
        public static void FixJSONCharacters(ref string json)
        {
            foreach (KeyValuePair<string, string> item in encodedCharacters)
            {
                if (json.Contains(item.Key))
                {
                    json = json.Replace(item.Key, item.Value);
                }
            }
        }
    }
}
