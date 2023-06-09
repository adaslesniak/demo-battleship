﻿using NUnit.Framework;
namespace Battleships.Tests;

[TestFixture]
public class ParsingCoordinates
{
    const int worldSize = 10;

    //stupid human encoding of rows from 1, but keeping them as indexes from 0
    [TestCase("a1", 0, 0)]
    [TestCase("B09", 1, 8)]
    [TestCase("c 7", 2, 6)]
    public void DecodesCoordinates(string encoded, byte expectedColumn, byte expectedRow) {
        Assert.That(Coordinates.TryParse(encoded, out var decoded, worldSize), $"couldn't parse: {encoded}");
        Assert.AreEqual(decoded.column, expectedColumn, $"wrong column({decoded.column}) - should be {expectedColumn}");  
        Assert.AreEqual(decoded.row, expectedRow, $"wrong row({decoded.row}) - should be {expectedRow}");
    }

    [TestCase(1, 1, "b2")]
    [TestCase(9, 9, "j10")]
    [TestCase(0, 4, "a5")]
    public void EncodesCoordinates(byte column, byte row, string expectedEncoding) {
        var coords = new Coordinates(column, row);
        var encoded = coords.ToString().ToLower();
        Assert.AreEqual(encoded, expectedEncoding, $"Wrong parsing ({encoded}) -> should be {expectedEncoding}"); 
    }

    [TestCase("ab1")]
    [TestCase(".1b")]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("3")]
    [TestCase("bb")]
    [TestCase("d3.0")]
    public void ShouldFailGracefully(string trashData) {
        Assert.False(Coordinates.TryParse(trashData, out _, worldSize), $"This trash data should fail: {trashData}");
    }

    [TestCase("a0")] //yeah - 0 is invalid as humans are counting instead of indexing
    [TestCase("z2")]
    [TestCase("c99")]
    [TestCase("b-1")]
    public void ShouldFailOutOfRange(string outOfBounds) {
        Assert.False(Coordinates.TryParse(outOfBounds, out _, worldSize), $"Those values beyond world range should fail: {outOfBounds}");
    }
}