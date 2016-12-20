using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BlockChain 
{

    public static class Helper {
        public static DateTime ReadUnixTimestamp(uint value) {
            // DateTimeOffset.FromUnixTimeSeconds() 4.6+ only
            return DateTime.SpecifyKind(new DateTime(1970, 1, 1) + TimeSpan.FromSeconds(value), DateTimeKind.Utc);
        }


        private static Regex _scriptPattern = new Regex(@"\b[0-9a-fA-F]+\b", RegexOptions.Compiled);
        public static string DetectScriptPattern(string script) {
            return _scriptPattern.Replace(script, m => $"[hex size={m.Length.ToString(CultureInfo.InvariantCulture)}]").Trim();
        }
    }
}
