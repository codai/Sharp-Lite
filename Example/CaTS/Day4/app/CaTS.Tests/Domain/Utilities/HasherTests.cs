using CaTS.Domain.Utilities;
using NUnit.Framework;

namespace CaTS.Tests.Domain.Utilities
{
    [TestFixture]
    public class HasherTests
    {
        [Test]
        public void CanCompareHashedStrings() {
            var hash1 = Hasher.Hash("dog");
            var hash2 = Hasher.Hash("cat");
            var hash3 = Hasher.Hash("dog");

            Assert.That(! hash1.Equals(hash2));
            Assert.That(hash1.Equals(hash3));
        }
    }
}
