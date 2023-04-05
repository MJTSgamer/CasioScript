using System.Globalization;

namespace CasioScript.Utilities
{
    public static class Utilities
    {
        public static float TryParseFloat(string input)
        {
            if (TryParseFloat(input, out float result))
            {
                return result;
            }
            else
            {
                return 0f;
            }
        }
        
        public static bool TryParseFloat(string input, out float result)
        {
            // Check if the input is null or empty
            if (string.IsNullOrEmpty(input))
            {
                result = 0f;
                return false;
            }

            // Replace commas with dots, and remove any additional dots
            input = input.Replace(",", ".");
            int dotIndex = input.IndexOf('.');
            if (dotIndex != -1)
            {
                input = input.Remove(dotIndex, 1);
                input = input.Insert(dotIndex, ".");
            }

            // Try to parse the input as a float
            if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }
            else
            {
                result = 0f;
                return false;
            }
        }
    }
}