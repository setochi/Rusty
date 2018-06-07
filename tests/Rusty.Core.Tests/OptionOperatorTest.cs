using System;
using Xunit;

namespace Rusty.Core.Tests
{
    public class OptionOperatorTest
    {
        [Fact]
        public void WrapNullableStruct()
        {
            int? val1 = 1;
            Assert.Equal(1, Option.Wrap(val1).Unwrap());

            int? val2 = null;
            Assert.Equal(typeof(None<int>), Option.Wrap(val2).GetType());
        }

        [Fact]
        public void WrapClass()
        {
            string val1 = "tanaka";
            Assert.Equal(val1, Option.Wrap(val1).Unwrap());

            string val2 = null;
            Assert.Equal(typeof(None<string>), Option.Wrap(val2).GetType());
        }

        [Fact]
        public void WrapFunc()
        {
            Func<int> f1 = () => 1;
            Assert.Equal(1, Option.Wrap(f1).Unwrap());

            Func<int> f2 = () => throw new Exception("this is test.");
            Assert.Equal(typeof(None<int>), Option.Wrap(f2).GetType());
        }

        [Fact]
        public void WrapTryPattern()
        {
            var opt1 = Option.Wrap<string, int>(int.TryParse, "123456");
            Assert.Equal(123456, opt1.Unwrap());

            var opt2 = Option.Wrap<string, int>(int.TryParse, "tanaka");
            Assert.Equal(typeof(None<int>), opt2.GetType());
        }
    }
}
