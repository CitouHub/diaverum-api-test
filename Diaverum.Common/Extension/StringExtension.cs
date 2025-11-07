using System.Globalization;

namespace Diaverum.Common.Extension
{
    public static class StringExtension
    {
        public static double? ToDouble(this string str)
        {
            var success = double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out double value);
            return success ? value : null;
        }
    }
}
