using System;
using Xunit;

namespace Rusty.Core.Tests
{
    public class OptionTest
    {
        [Fact]
        public void Except()
        {
            Assert.Equal(1, new Some<int>(1).Expect("error message."));
            Assert.Throws<InvalidOperationException>(() => None<int>.Instance.Expect("error message."));
        }

        [Fact]
        public void Unwrap()
        {
            Assert.Equal(1, new Some<int>(1).Unwrap());
            Assert.Throws<InvalidOperationException>(() => None<int>.Instance.Unwrap());
        }

        [Fact]
        public void UnwrapOr()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(1, opt1.UnwrapOr(2));

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(2, opt2.UnwrapOr(2));
        }

        [Fact]
        public void UnwrapOrElse()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(1, opt1.UnwrapOrElse(() => 2));

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(2, opt2.UnwrapOrElse(() => 2));
        }

        [Fact]
        public void IsSome()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.True(opt1.IsSome());

            Option<int> opt2 = None<int>.Instance;
            Assert.False(opt2.IsSome());
        }

        [Fact]
        public void IsNone()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.False(opt1.IsNone());

            Option<int> opt2 = None<int>.Instance;
            Assert.True(opt2.IsNone());
        }

        [Fact]
        public void Map()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal("2", opt1.Map(x => (x + 1).ToString()).Unwrap());

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(None<string>.Instance, opt2.Map(x => (x + 1).ToString()));
        }

        [Fact]
        public void MapOr()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal("1", opt1.MapOr("2", x => x.ToString()));

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal("2", opt2.MapOr("2", x => x.ToString()));
        }

        [Fact]
        public void MapOrElse()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal("1", opt1.MapOrElse(() => "2", x => x.ToString()));

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal("2", opt2.MapOrElse(() => "2", x => x.ToString()));
        }

        [Fact]
        public void OkOr()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(1, opt1.OkOr("error message.").Unwrap());

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(typeof(Err<int, string>), opt2.OkOr("error message.").GetType());
        }

        [Fact]
        public void OkOrElse()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(1, opt1.OkOrElse(() => "1").Unwrap());

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(typeof(Err<int, string>), opt2.OkOrElse(() => "error message.").GetType());
        }

        [Fact]
        public void And()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(2, opt1.And(new Some<int>(2)).Unwrap());
            Assert.Equal(None<int>.Instance, opt1.And(None<int>.Instance));

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(None<int>.Instance, opt2.And(new Some<int>(2)));
            Assert.Equal(None<int>.Instance, opt2.And(None<int>.Instance));
        }

        [Fact]
        public void AndThen()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(2, opt1.AndThen(x => new Some<int>(x + 1)).Unwrap());
            Assert.Equal(None<int>.Instance, opt1.AndThen(_ => None<int>.Instance));

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(None<int>.Instance, opt2.AndThen(x => new Some<int>(x + 1)));
            Assert.Equal(None<int>.Instance, opt2.AndThen(_ => None<int>.Instance));
        }

        [Fact]
        public void Filter()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(1, opt1.Filter(x => x == 1).Unwrap());
            Assert.Equal(None<int>.Instance, opt1.Filter(x => x != 1));

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(None<int>.Instance, opt2.Filter(x => x == 1));
        }

        [Fact]
        public void Or()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(1, opt1.Or(new Some<int>(2)).Unwrap());
            Assert.Equal(1, opt1.Or(None<int>.Instance).Unwrap());

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(2, opt2.Or(new Some<int>(2)).Unwrap());
            Assert.Equal(None<int>.Instance, opt2.Or(None<int>.Instance));
        }

        [Fact]
        public void OrElse()
        {
            Option<int> opt1 = new Some<int>(1);
            Assert.Equal(1, opt1.OrElse(() => new Some<int>(2)).Unwrap());
            Assert.Equal(1, opt1.OrElse(() => None<int>.Instance).Unwrap());

            Option<int> opt2 = None<int>.Instance;
            Assert.Equal(2, opt2.OrElse(() => new Some<int>(2)).Unwrap());
            Assert.Equal(None<int>.Instance, opt2.OrElse(() => None<int>.Instance));
        }
    }
}
