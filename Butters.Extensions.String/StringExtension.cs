using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Butters.Extensions.String
{
    public static partial class StringExtensions
    {
        private static readonly string ExceptionMessageFormat = "{0} is not valid.";

        [GeneratedRegex("([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\\w*)")]
        private static partial Regex RegexToCamelCaseUpper();

        #region Convert Type

        public static byte[] ToByte(this string value) => Encoding.UTF8.GetBytes(value);

        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

            var val = value.ToLower().Trim();
            return val switch
            {
                "false" => false,
                "f" => false,
                "no" => false,
                "n" => false,
                "true" => true,
                "t" => true,
                "yes" => true,
                "y" => true,
                _ => throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value))
            };
        }

        public static short ToShort(this string value) =>
            short.TryParse(value, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

        public static short ToInt16(this string value) => value.ToShort();

        public static int ToInt(this string value) =>
            int.TryParse(value, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

        public static int ToInt32(this string value) => value.ToInt();

        public static long ToLong(this string value) =>
            long.TryParse(value, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

        public static long ToInt64(this string value) => value.ToLong();

        public static float ToFloat(this string value) =>
            float.TryParse(value, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

        public static float ToSingle(this string value) => value.ToFloat();

        public static double ToDouble(this string value) =>
            double.TryParse(value, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

        public static decimal ToDecimal(this string value) =>
            decimal.TryParse(value, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

        public static DateTime ToDateTime(this string value, string dateTimeFormat) =>
            DateTime.TryParseExact(value, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime) ?
            dateTime :
            DateTime.MinValue;

        public static T ToEnum<T>(this string value, bool ignoreCase = true) where T : struct, Enum =>
            Enum.TryParse(value, ignoreCase, out T result) ?
            result :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, value), nameof(value));

        public static T? ToObjectFromJson<T>(this string value) => JsonSerializer.Deserialize<T>(value);

        #endregion

        #region format

        //TODO performance
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        //TODO performance
        //https://code-maze.com/csharp-convert-string-titlecase-camelcase/
        public static string ToCamelCaseLower(this string str)
        {
            var words = str.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries);

            var leadWord = RegexToCamelCaseUpper().Replace(words[0], m =>
                {
                    return m.Groups[1].Value.ToLower() + m.Groups[2].Value.ToLower() + m.Groups[3].Value;
                });

            var tailWords = words.Skip(1)
                .Select(word => char.ToUpper(word[0]) + word[1..]) // word[1..] == word.Substring(1)
                .ToArray();

            return $"{leadWord}{string.Join(string.Empty, tailWords)}";
        }

        public static string ToCamelCase(this string str) => ToCamelCaseLower(str);

        //TODO performance
        //https://code-maze.com/csharp-convert-string-titlecase-camelcase/
        public static string ToCamelCaseUpper(this string str)
        {
            var words = str.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries);

            var tailWords = words
                .Select(word => char.ToUpper(word[0]) + word[1..]) // word[1..] == word.Substring(1)
                .ToArray();

            return $"{string.Join(string.Empty, tailWords)}";
        }

        //https://stackoverflow.com/questions/3565015/bestpractice-transform-first-character-of-a-string-into-lower-case
        public static string? ToFirstCharLowerCase(this string? str)
        {
            if (!string.IsNullOrWhiteSpace(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        #endregion
    }
}