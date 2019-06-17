using NUnit.Framework;

namespace UKPBS.Services.UnitTests.Helpers
{
    public static class UnitTestHelper
    {
        public static void AssertEqualSerialization<T>(T expected, T actual, string failureMessage)
        {
            if (expected == null || actual == null)
            {
                Assert.AreEqual(expected, actual);
                return;
            }

            var expectedString = SerializationHelper.SerializeToJson(expected);
            var actualString = SerializationHelper.SerializeToJson(actual);

            Assert.AreEqual(expectedString, actualString, failureMessage);
        }
    }
}
