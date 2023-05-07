using NUnit.Framework;
namespace Battleships.Tests;

/// <summary>
/// those are readonly structs so must behave like proper structs
/// and be compared/identified by value - tests against messing with equals and hash and opearators
/// </summary>
[TestFixture]
public class ComparingCoordinates
{
    [TestCase(0, 0)]
    [TestCase(255, 255)]
    [TestCase(99, 0)]
    public void ShouldCompareValues(byte column, byte row) {
        var one = new Coordinates(column, row);
        var second = new Coordinates(column, row);
        Assert.AreEqual(one, second);
    }

    [TestCase(0, 0)]
    [TestCase(255, 255)]
    [TestCase(99, 0)]
    public void ShouldFindElement(byte column, byte row) {
        var coords = new Coordinates(column, row);
        var collection = new List<Coordinates>();
        collection.Add(new Coordinates(column, row));   
        Assert.That(collection.Contains(coords), $"wow");

    }
}
