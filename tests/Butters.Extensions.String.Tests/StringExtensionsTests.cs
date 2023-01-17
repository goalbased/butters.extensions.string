using Xunit;
using Butters.Extensions.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Butters.Extensions.String.Tests
{
    public class StringExtensionsTests
    {
        #region Convert Type

        [Theory]
        [InlineData(new byte[] { 97, 49 }, "a1")]
        public void ToByte(byte[] bytes, string str) => Assert.Equal(bytes, str.ToByte());

        [Theory]
        [InlineData(true, "TRUE")]
        [InlineData(true, "true")]
        [InlineData(true, "t")]
        [InlineData(true, "y")]
        [InlineData(true, "yes")]
        [InlineData(false, "False")]
        [InlineData(false, "false")]
        [InlineData(false, "f")]
        [InlineData(false, "n")]
        [InlineData(false, "no")]
        public void ToBoolean(bool expected, string str) => Assert.Equal(expected, str.ToBoolean());

        [Theory]
        [InlineData(1, "1")]
        [InlineData(short.MinValue, "-32768")]
        [InlineData(short.MaxValue, "32767")]
        public void ToShort(short expected, string str) => Assert.Equal(expected, str.ToShort());

        [Theory]
        [InlineData(1, "1")]
        [InlineData(short.MinValue, "-32768")]
        [InlineData(short.MaxValue, "32767")]
        public void ToInt16(short expected, string str) => Assert.Equal(expected, str.ToInt16());

        [Theory]
        [InlineData(1, "1")]
        [InlineData(int.MaxValue, "2147483647")]
        [InlineData(int.MinValue, "-2147483648")]
        public void ToInt(int expected, string str) => Assert.Equal(expected, str.ToInt());

        [Theory]
        [InlineData(1, "1")]
        public void ToInt32(int expected, string str) => Assert.Equal(expected, str.ToInt32());


        [Theory]
        [InlineData(1, "1")]
        [InlineData(long.MaxValue, "9223372036854775807")]
        [InlineData(long.MinValue, "-9223372036854775808")]
        public void ToLong(long expected, string str) => Assert.Equal(expected, str.ToLong());

        [Theory]
        [InlineData(1, "1")]
        public void ToInt64(int expected, string str) => Assert.Equal(expected, str.ToInt64());

        [Theory]
        [InlineData(1, "1")]
        [InlineData(float.MaxValue, "3.40282347E+38")]
        [InlineData(float.MinValue, "-3.40282347E+38")]
        public void ToFloat(float expected, string str) => Assert.Equal(expected, str.ToFloat());

        [Theory]
        [InlineData(1, "1")]
        public void ToSingle(float expected, string str) => Assert.Equal(expected, str.ToSingle());

        [Theory]
        [InlineData(1, "1")]
        [InlineData(double.MaxValue, "1.7976931348623157E+308")]
        [InlineData(double.MinValue, "-1.7976931348623157E+308")]
        public void ToDouble(double expected, string str) => Assert.Equal(expected, str.ToDouble());

        [Theory]
        [InlineData(1, "1")]
        // https://codeblog.jonskeet.uk/2014/08/22/when-is-a-constant-not-a-constant-when-its-a-decimal/        
        public void ToDecimal(decimal expected, string str) => Assert.Equal(expected, str.ToDecimal());

        [Fact]
        public void ToDateTime()
        {
            var expected = new DateTime(2022, 01, 02);
            var str = "2022/01/02";
            Assert.Equal(expected, str.ToDateTime("yyyy/MM/dd"));
        }


        [Theory]
        [InlineData(DayOfWeek.Monday, "Monday")]
        [InlineData(DayOfWeek.Monday, "monday")]
        public void ToEnum(DayOfWeek expected, string str) => Assert.Equal(expected, str.ToEnum<DayOfWeek>());

        [Theory]
        [InlineData("Xonday")]
        public void ToEnumException(string str) => Assert.Throws<ArgumentException>(() => str.ToEnum<DayOfWeek>());

        [Fact()]
        public void ToObjectFromJson()
        {
            var model = new TestModel()
            {
                A = "Astr",
                B = 3,
            };
            var str = JsonSerializer.Serialize(model);

            var result = str.ToObjectFromJson<TestModel>();

            Assert.Equivalent(model, result);
        }

        #endregion

        [Theory]
        [InlineData("name_of_property", "nameOfProperty")]
        [InlineData("name _of _property", "name Of Property")]
        public void ToSnakeCase(string expected, string str) => Assert.Equal(expected, str.ToSnakeCase());

        [Theory]
        [InlineData("welcomeToThe", "Welcome to the")]
        [InlineData("welcomeToThe", "Welcome To The")]
        [InlineData("welcomeToThe", "WelcomeToThe")]
        [InlineData("welcomeToThe", "Welcome_To_The")]
        [InlineData("welcomeToThe", "Welcome-To-The")]
        [InlineData("isoDate", "ISODate")]
        [InlineData("ioStream", "IOStream")]
        [InlineData("ioaaaStream", "IOAAAStream")]
        public void ToCamelCaseLower(string expected, string str) => Assert.Equal(expected, str.ToCamelCaseLower());

        [Theory]
        [InlineData("welcomeToThe", "Welcome to the")]
        public void ToCamelCase(string expected, string str) => Assert.Equal(expected, str.ToCamelCase());

        [Theory]
        [InlineData("WelcomeToThe", "Welcome to the")]
        [InlineData("WelcomeToThe", "Welcome To The")]
        [InlineData("WelcomeToThe", "WelcomeToThe")]
        [InlineData("WelcomeToThe", "Welcome_To_The")]
        [InlineData("ISODate", "ISODate")]
        [InlineData("IOStream", "IOStream")]
        [InlineData("IOAAAStream", "IOAAAStream")]
        public void ToCamelCaseUpper(string expected, string str) => Assert.Equal(expected, str.ToCamelCaseUpper());

        [Theory]
        [InlineData("abc", "Abc")]
        [InlineData("aBc", "aBc")]
        [InlineData("", "")]
        [InlineData("a", "A")]
        [InlineData("abc", "abc")]
        public void ToFirstCharLowerCase(string expected, string str) => Assert.Equal(expected, str.ToFirstCharLowerCase());
    }

    public class TestModel
    {
        public string? A { get; set; }
        public int B { get; set; }
        public int? C { get; set; }
    }
}


//[Fact()]
//public void IsDateTime()
//{
//    Assert.True("2020/01/01 10:10".IsDateTime("yyyy/MM/dd hh:mm"));
//};

//[Fact()]
//public void ToDateTime() => Assert.Equal(new DateTime(2000, 1, 1), "2000/01/01".ToDateTime("yyyy/MM/dd"));
