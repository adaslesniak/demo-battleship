using NUnit.Framework;
namespace Battleships.Tests;

//that's crucial - we should create some mockup player and create game that uses both those
//players and then compare step by step if steps are valid... are those still unit tests? 
//or is this more complex test
[TestFixture]
internal class RunningGameLoop
{
    [Test]
    public void NoTestsNoGo() {
        Assert.Fail("no passaran without tests!");
    }
}
