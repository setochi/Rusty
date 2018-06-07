using System;
using Xunit;

namespace Rusty.Core.Tests
{
    public class ResultTest
    {
        [Fact]
        public void IsOk()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.True(res1.IsOk());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.False(res2.IsOk());
        }

        [Fact]
        public void IsErr()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.False(res1.IsErr());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.True(res2.IsErr());
        }

        [Fact]
        public void Some()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(typeof(Some<int>),  res1.Some().GetType());
            Assert.Equal(1, res1.Some().Unwrap());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(typeof(None<int>), res2.Some().GetType());
            Assert.Throws<InvalidOperationException>(() => res2.Some().Unwrap());
        }

        [Fact]
        public void None()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(typeof(None<Exception>), res1.None().GetType());
            Assert.Throws<InvalidOperationException>(() => res1.None().Unwrap());

            Result<int, string> res2 = new Err<int, string>("error message.");
            Assert.Equal(typeof(Some<string>), res2.None().GetType());
            Assert.Equal("error message.", res2.None().Unwrap());
        }

        [Fact]
        public void Map()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(2, res1.Map(x => x + 1).Unwrap());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(typeof(Err<string, Exception>), res2.Map(x => $"{x + 1}").GetType());
        }

        [Fact]
        public void MapErr()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(1, res1.MapErr(e => e).Unwrap());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(typeof(Err<int, string>), res2.MapErr(e => "mapped error.").GetType());
        }

        [Fact]
        public void And()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(2, res1.And(new Ok<int, Exception>(2)).Unwrap());
            Assert.Equal(typeof(Err<string, Exception>),  res1.And(new Err<string, Exception>(new Exception("this is test."))).GetType());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(typeof(Err<string, Exception>), res2.And(new Ok<string, Exception>("string value.")).GetType());
            Assert.Equal(typeof(Err<string, Exception>), res2.And(new Err<string, Exception>(new Exception("this is test."))).GetType());
        }

        [Fact]
        public void AndThen()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal("2", res1.AndThen(x => new Ok<string, Exception>($"{x + 1}")).Unwrap());
            Assert.Equal(typeof(Err<string, Exception>), res1.AndThen(x => new Err<string, Exception>(new Exception("this is test."))).GetType());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(typeof(Err<string, Exception>), res2.AndThen(x => new Ok<string, Exception>($"{x + 1}")).GetType());
        }

        [Fact]
        public void Or()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(1, res1.Or(new Ok<int, Exception>(2)).Unwrap());
            Assert.Equal(1, res1.Or(new Err<int, Exception>(new Exception("this is test."))).Unwrap());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(2, res2.Or(new Ok<int, Exception>(2)).Unwrap());
            Assert.Equal(typeof(Err<int, Exception>), res2.Or(new Err<int, Exception>(new Exception("this is test."))).GetType());
        }

        [Fact]
        public void OrElse()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(1, res1.OrElse(x => new Ok<int, Exception>(2)).Unwrap());
            Assert.Equal(1, res1.OrElse(e => new Err<int, Exception>(new Exception("this is test."))).Unwrap());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(2, res2.OrElse(x => new Ok<int, Exception>(2)).Unwrap());
            Assert.Equal(typeof(Err<int, ArgumentException>), res2.OrElse(e => new Err<int, ArgumentException>(new ArgumentException())).GetType());
        }

        [Fact]
        public void Unwrap()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(1, res1.Unwrap());

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Throws<Exception>(() => res2.Unwrap());
        }

        [Fact]
        public void UnwrapOr()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(1, res1.UnwrapOr(2));

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(2, res2.UnwrapOr(2));
        }

        [Fact]
        public void UnwrapOrElse()
        {
            Result<int, Exception> res1 = new Ok<int, Exception>(1);
            Assert.Equal(1, res1.UnwrapOrElse(e => 2));

            Result<int, Exception> res2 = new Err<int, Exception>(new Exception("this is test."));
            Assert.Equal(2, res2.UnwrapOrElse(e => 2));
        }
    }
}
