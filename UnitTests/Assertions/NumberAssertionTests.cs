﻿using NSubstitute;
using NUnit.Framework;

namespace EasyAssertions.UnitTests
{
    [TestFixture]
    public class NumberAssertionTests : AssertionTests
    {
        [Test]
        public void ShouldBe_FloatsWithinDelta_ReturnsActualValue()
        {
            const float actual = 1f;
            Actual<float> result = actual.ShouldBe(1f, 1f);

            Assert.AreEqual(actual, result.And);
        }

        [Test]
        public void ShouldBe_FloatsOutsideDelta_FailsWithObjectsNotEqualMessage()
        {
            const float expected = 10f;
            const float actual = 1f;
            MockFormatter.NotEqual(expected, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldBe(expected, 0, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldBe_DoublesWithinDelta_ReturnsActualValue()
        {
            const double actual = 1d;

            Actual<double> result = actual.ShouldBe(1d, 0);

            Assert.AreEqual(actual, result.And);
        }

        [Test]
        public void ShouldBe_DoublesOutsideDelta_FailsWithObjectsNotEqualMessage()
        {
            const double expected = 10d;
            const double actual = 1d;
            MockFormatter.NotEqual(expected, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldBe(expected, 1d, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldNotBe_FloatsOutsideDelta_ReturnsActualValue()
        {
            const float actual = 1f;
            Actual<float> result = actual.ShouldNotBe(2f, 0);

            Assert.AreEqual(actual, result.And);
        }

        [Test]
        public void ShouldNotBe_FloatsWithinDelta_FailsWithObjectsEqualMessage()
        {
            const float actual = 1f;
            const float notExpected = 2f;
            MockFormatter.AreEqual(notExpected, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldNotBe(notExpected, 1f, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldNotBe_DoublesOutsideDelta_ReturnsActualValue()
        {
            const double actual = 1d;

            Actual<double> result = actual.ShouldNotBe(2d, 0);

            Assert.AreEqual(actual, result.And);
        }

        [Test]
        public void ShouldNotBe_DoublesWithinDelta_FailsWithObjectsEqualMessage()
        {
            const double actual = 1d;
            const double notExpected = 2d;
            MockFormatter.AreEqual(notExpected, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldNotBe(notExpected, 1d, "foo"));

            Assert.AreEqual("bar", result.Message);
        }

        [Test]
        public void ShouldBeGreaterThan_IsGreaterThan_ReturnsActualValue()
        {
            const int actual = 2;

            Actual<int> result = actual.ShouldBeGreaterThan(1);

            Assert.AreEqual(actual, result.And);
        }

        [Test]
        public void ShouldBeGreaterThan_NotGreaterThan_FailsWithNotGreaterThanMessage()
        {
            const int actual = 1;
            const int expected = 2;
            MockFormatter.NotGreaterThan(expected, actual, "foo").Returns("bar");

            EasyAssertionException result = Assert.Throws<EasyAssertionException>(() => actual.ShouldBeGreaterThan(expected, "foo"));

            Assert.AreEqual("bar", result.Message);
        }
    }
}