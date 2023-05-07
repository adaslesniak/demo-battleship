using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships;

internal class Map
{
    public enum State { Unknwon, Empty, Sunk, Occupied }

    readonly State[,] map;

    static readonly Random luck = new Random();

    internal bool HitField(Coordinates target) {
        if(map[target.column, target.row] is not State.Occupied) {
            return false;
        }
        map[target.column, target.row] = State.Sunk;
        return true;
    }

    internal bool IsAlive() =>
        map.Cast<State>().Any(field => field is State.Occupied);

    private Map(byte mapSize) {
        map = new State[mapSize, mapSize];
    }

    internal static Map Prepare(byte mapSize, Ship[] ships) {
        var instance = new Map(mapSize);
        foreach(var deployed in ships) {
            foreach(var field in ShipArea(deployed)) {
                instance.map[field.column, field.row] = State.Occupied;
            }
        }
        return instance;
        //coulda/shoulda throw something in the face of invalid values, but there is no requirement for validation
    }

    static Coordinates[] ShipArea(Ship subject) {
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

    //TODO FIXME this should be in separate class, as it breaks single responsibility principle
    internal static Ship[] AutoDeployment(GameSettings rules) {

        var table = new Ship[rules.initialShips.Length];
        var usedFields = new HashSet<Coordinates>();
        for(int i = 0; i < rules.initialShips.Length; i++) {
            table[i] = Prepre(rules.initialShips[i]);
            UseFields(ShipArea(table[i]));
        }
        return table;


        Ship Prepre(Ship.Factory howToMakeIt) {
            int safetyCounter = 0;
            while(safetyCounter++ < 101000) {
                var ship = howToMakeIt(RandomDirection(), Map.RandomPlace(rules.mapSize));
                if(IsFreePlaceFor(ship)) {
                    return ship;
                }
            }
            throw new Exception("Blame programmers");
        }

        bool IsFreePlaceFor(Ship ship) =>
            ShipArea(ship).All(field =>
                field.column < rules.mapSize
                && field.row < rules.mapSize
                && false == usedFields.Contains(field));

        void UseFields(Coordinates[] used) {
            foreach(var field in used) {
                usedFields.Add(field);
            }
        }



        Ship.Direction RandomDirection() =>
            luck.Next() % 2 is 0
            ? Ship.Direction.Left
            : Ship.Direction.Down;
    }


    internal static Coordinates RandomPlace(int worldSize) =>
        new((byte)luck.Next(worldSize), (byte)luck.Next(worldSize));
}