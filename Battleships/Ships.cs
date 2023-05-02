﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships;
internal class Ship
{
    public enum Direction { Left, Down }

    internal delegate Ship Factory(Direction direction, Coordinates topLeft);

    internal readonly byte size;
    
    internal readonly (Coordinates topLeft, Direction rotation) position;

    private Ship(byte ofSize, Coordinates topLeft, Direction rotation) {
        size = ofSize;
        position = (topLeft, rotation);
    }

    public static Ship Battleship(Direction direction, Coordinates topLeft) =>
        new( 5, topLeft, direction );

    public static Ship Destroyer(Direction direction,Coordinates topLeft) =>
        new(4, topLeft, direction);
}
