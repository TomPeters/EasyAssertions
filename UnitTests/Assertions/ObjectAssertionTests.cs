﻿using NSubstitute;
using NUnit.Framework;

namespace EasyAssertions.UnitTests
{
    [TestFixture]
    public class ObjectAssertionTests : AssertionTests
    {
        [Test]
        public void ShouldBe_SameValueReturnsActualValue()
        {
            Equatable actual = new Equatable(1);
            Equatable expected = new Equatable(1);
            Actual<Equatable> result = actual.ShouldBe(expected);

            Assert.AreSame(actual, result.And);
        }

        [Test]
        public void ShouldBe_DifferentObjects_FailsWithObjectsNotEqualMessage()
        {
            object obj1 = new object();
            object obj2 = new object();
            MockFormatter.NotEqual(obj2, obj1, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => obj1.ShouldBe(obj2, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldBe_DifferentStrings_FailsWithStringsNotEqualMessage()
        {
            MockFormatter.NotEqual("foo", "bar", "baz").Returns("qux");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => "bar".ShouldBe("foo", "baz"));

            Assert.AreEqual("qux", result.Message);
        }

        [Test]
        public void ShouldNotBe_DifferentValue_ReturnsActualValue()
        {
            Equatable actual = new Equatable(1);

            Actual<Equatable> result = actual.ShouldNotBe(new Equatable(2));

            Assert.AreSame(actual, result.Value);
        }

        [Test]
        public void ShouldNotBe_EqualValue_FailsWithObjectsEqualMessage()
        {
            Equatable actual = new Equatable(1);
            Equatable notExpected = new Equatable(1);
            MockFormatter.AreEqual(notExpected, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldNotBe(notExpected, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldBeNull_IsNull_Passes()
        {
            ((object)null).ShouldBeNull();
        }

        [Test]
        public void ShouldBeNull_NotNull_FailsWithNotEqualToNullMessage()
        {
            object actual = new object();
            MockFormatter.NotEqual(null, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldBeNull("foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldNotBeNull_NotNull_ReturnsActualValue()
        {
            object actual = new object();

            Actual<object> result = actual.ShouldNotBeNull();

            Assert.AreSame(actual, result.And);
        }

        [Test]
        public void ShouldNotBeNull_IsNull_FailsWithIsNullMessage()
        {
            MockFormatter.IsNull("foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => ((object)null).ShouldNotBeNull("foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldBeThis_SameObject_ReturnsActualValue()
        {
            object obj = new object();
            Actual<object> result = obj.ShouldBeThis(obj);

            Assert.AreSame(obj, result.And);
        }

        [Test]
        public void ShouldBeThis_DifferentObject_FailsWithObjectsNotSameMessage()
        {
            Equatable actual = new Equatable(1);
            Equatable expected = new Equatable(1);
            MockFormatter.NotSame(expected, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldBeThis(expected, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldNotBeThis_DifferentObject_ReturnsActualValue()
        {
            object actual = new object();

            Actual<object> result = actual.ShouldNotBeThis(new object());

            Assert.AreSame(actual, result.And);
        }

        [Test]
        public void ShouldNotBeThis_SameObject_FailsWithObjectsAreSameMessage()
        {
            object actual = new object();
            MockFormatter.AreSame(actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldNotBeThis(actual, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldBeA_SubType_ReturnsTypedActual()
        {
            object actual = new SubEquatable(1);
            Actual<Equatable> result = actual.ShouldBeA<Equatable>();

            Assert.AreSame(actual, result.And);
            Assert.AreEqual(1, result.And.Value);
        }

        [Test]
        public void ShouldBeA_SuperType_FailsWithTypesNotEqualMessage()
        {
            object actual = new Equatable(1);
            MockFormatter.NotEqual(typeof(SubEquatable), typeof(Equatable), "foo").Returns("bar");
            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldBeA<SubEquatable>("foo"));

            Assert.AreEqual("bar", result.Message);
        }

        protected class Equatable
        {
            public readonly int Value;

            public Equatable(int value)
            {
                Value = value;
            }

            public override bool Equals(object obj)
            {
                Equatable otherEquatable = obj as Equatable;
                return otherEquatable != null
                    && otherEquatable.Value == Value;
            }
        }

        protected class SubEquatable : Equatable
        {
            public SubEquatable(int value) : base(value) { }
        }
    }
}