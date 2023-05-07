namespace Battleships;

public static class RandomMapData
{
    internal static Ship[] RandomDeployment(this GameSettings rules) {

        var table = new Ship[rules.initialShips.Length];
        var usedFields = new HashSet<Coordinates>();
        for(int i = 0; i < rules.initialShips.Length; i++) {
            table[i] = Prepre(rules.initialShips[i]);
            UseFields(Map.ShipArea(table[i]));
        }
        return table;


        Ship Prepre(Ship.Factory howToMakeIt) {
            int safetyCounter = 0;
            while(safetyCounter++ < 101000) {
                var ship = howToMakeIt(RandomDirection(), rules.RandomPlace());
                if(IsFreePlaceFor(ship)) {
                    return ship;
                }
            }
            throw new Exception("Blame programmers");
        }

        bool IsFreePlaceFor(Ship ship) =>
            Map.ShipArea(ship).All(field =>
                field.column < rules.mapSize
                && field.row < rules.mapSize
                && false == usedFields.Contains(field));

        void UseFields(Coordinates[] used) {
            foreach(var field in used) {
                usedFields.Add(field);
            }
        }

        Ship.Direction RandomDirection() =>
            rules.luck.Next() % 2 is 0
            ? Ship.Direction.Left
            : Ship.Direction.Down;
    }

    internal static Coordinates RandomPlace(this GameSettings setup) =>
        new((byte)setup.luck.Next(setup.mapSize), (byte)setup.luck.Next(setup.mapSize));
}
