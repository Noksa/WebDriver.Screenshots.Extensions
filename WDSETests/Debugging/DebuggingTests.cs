using NUnit.Framework;

namespace WDSETests.Debugging
{
#if DEBUG
    [TestFixture(TestName = "DEBUG SUITE")]
    [NonParallelizable]
    public class DebuggingTests : TestsInit
    {
        [Test]
        public void Debugging()
        {
            
        }
    }
#endif
}