using NUnit.Framework;
namespace Battleships.Tests;

[NUnit.Framework.TestFixture]
public class ThereShallBeTests
{
    [Test]
    public void NoTestsNoGo() {
        NUnit.Framework.Assert.Fail("no passaran without tests!");
    }
    
}