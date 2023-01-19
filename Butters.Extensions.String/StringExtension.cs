using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Butters.Extensions.String
{
    /// <summary>
    /// String extension library
    /// </summary>
    public static partial class StringExtensions
    {
        private static readonly string ExceptionMessageFormat = "{0} is not valid.";

        [GeneratedRegex("([A-Z])([A-Z]+|[a-z0-9]+)($|[A-Z]\\w*)")]
        private static partial Regex RegexToCamelCaseUpper();

        #region Convert Type

        /// <summary>
        /// Convert string to byte array
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToByte(this string str) => Encoding.UTF8.GetBytes(str);

        /// <summary>
        /// Converts string to boolean which is case insensitive
        /// true: "true", "t", "yes", "y"
        /// false: "false", "f", "no", "n"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool ToBoolean(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));

            var val = str.ToLower().Trim();
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
                _ => throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str))
            };
        }

        /// <summary>
        /// Converts string to 16-bit signed integer
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static short ToShort(this string str) =>
            short.TryParse(str, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));

        /// <summary>
        /// Converts string to 16-bit signed integer, this is as same as ToShort()
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static short ToInt16(this string str) => str.ToShort();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int ToInt(this string str) =>
            int.TryParse(str, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt32(this string str) => str.ToInt();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static long ToLong(this string str) =>
            long.TryParse(str, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToInt64(this string str) => str.ToLong();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static float ToFloat(this string str) =>
            float.TryParse(str, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToSingle(this string str) => str.ToFloat();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static double ToDouble(this string str) =>
            double.TryParse(str, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static decimal ToDecimal(this string str) =>
            decimal.TryParse(str, out var number) ?
            number :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dateTimeFormat"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string str, string dateTimeFormat) =>
            DateTime.TryParseExact(str, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime) ?
            dateTime :
            DateTime.MinValue;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T ToEnum<T>(this string str, bool ignoreCase = true) where T : struct, Enum =>
            Enum.TryParse(str, ignoreCase, out T result) ?
            result :
            throw new ArgumentException(string.Format(ExceptionMessageFormat, str), nameof(str));
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T? ToObjectFromJson<T>(this string str) => JsonSerializer.Deserialize<T>(str);

        #endregion

        #region format

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSnakeCase(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }

        /// <summary>
        /// https://code-maze.com/csharp-convert-string-titlecase-camelcase/
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string str) => ToCamelCaseLower(str);

        /// <summary>
        /// https://code-maze.com/csharp-convert-string-titlecase-camelcase/
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCamelCaseUpper(this string str)
        {
            var words = str.Split(new[] { "_", " ", "-" }, StringSplitOptions.RemoveEmptyEntries);

            var tailWords = words
                .Select(word => char.ToUpper(word[0]) + word[1..]) // word[1..] == word.Substring(1)
                .ToArray();

            return $"{string.Join(string.Empty, tailWords)}";
        }

        /// <summary>
        /// https://stackoverflow.com/questions/3565015/bestpractice-transform-first-character-of-a-string-into-lower-case
        /// </summary>
        /// <param name="str"></param>        
        /// <returns></returns>        
        public static string? ToFirstCharLowerCase(this string? str)
        {
            if (!string.IsNullOrWhiteSpace(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

    }
}
