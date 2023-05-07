using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships;

internal class Map
{
    public enum State { Unknwon, Empty, Sunk, Occupied }

    readonly State[,] map;
    readonly GameSettings setup;

    internal bool HitField(Coordinates target) {
        if(Math.Max(target.column, target.row) >= setup.mapSize) {
            return false;
        }
        if(map[target.column, target.row] is not State.Occupied) {
            return false;
        }
        map[target.column, target.row] = State.Sunk;
        return true;
    }

    public bool IsAfloat() =>
        map.Cast<State>().Any(IsOccupied);

    public int OccupiedFields() =>
        map.Cast<State>().Where(IsOccupied).Count();
    
    bool IsOccupied(State field) =>
        field is State.Occupied;

    private Map(GameSettings withSetup) {
        this.setup = withSetup;
        map = new State[setup.mapSize, setup.mapSize];
    }

    internal static Map Prepare(GameSettings rules, Ship[] ships) {
        var instance = new Map(rules);
        foreach(var deployed in ships) {
            foreach(var field in ShipArea(deployed)) {
                if(instance.map[field.column, field.row] is State.Occupied) {
                    throw new Exception("Cheater go away");
                }
                instance.map[field.column, field.row] = State.Occupied;
            }
        }
        return instance;
    }

    public static Coordinates[] ShipArea(Ship subject) {
        var fields = new Coordinates[subject.size];
        fields[0] = subject.position.topLeft;
        for(int i = 1; i < subject.size; i++) {
            var previousField = fields[i - 1];
            fields[i] =
                subject.position.rotation == Ship.Direction.Left
                ? new Coordinates(previousField.column + 1, previousField.row)
                : new Coordinates(previousField.column, previousField.row + 1);
        }
        return fields;
    }
}