using System.Text;

namespace SuperScale
{
    public static class StringExtentions
    {
        public static string InsertSpaces(this string input, int interval)
        {
            if (string.IsNullOrEmpty(input) || interval <= 0 || interval >= input.Length)
                return input;

            StringBuilder sb = new StringBuilder(input);

            for (int i = sb.Length - interval; i > 0; i -= interval)
            {
                sb.Insert(i, ' ');
            }

            return sb.ToString();
        }
    }
}
