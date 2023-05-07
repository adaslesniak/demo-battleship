using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    internal struct Coordinates
    {
        internal readonly byte column;
        internal readonly byte row;

        internal Coordinates(int column, int row) {
            this.column = (byte)column;
            this.row = (byte)row;
        }

        internal Coordinates(byte column, byte row) {
            this.column = column;
            this.row = row;
        }

        public override string ToString() =>
            $"{(char)(column + 'a')}{row}";

        public static bool TryParse(string? text, out Coordinates result, byte worldSize) {
            byte column = 0;
            byte row = 0;
            var isValid =
                text?.Length >= 2
                && TryParseColumn(text[0], out column)
                && column < worldSize
                && byte.TryParse(text.Substring(1), out row)
                && row < worldSize + 1;
            if(isValid is false) {
                result = new Coordinates(0, 0);
                return false;
            }
            result = new Coordinates(column, row);  
            return true;

            static bool TryParseColumn(char key, out byte result) {
                result = (byte)0;
                if(char.IsAsciiLetterUpper(key)) {
                    result = (byte)(key - 'A');
                } else if(char.IsAsciiLetterLower(key)) {
                    result = (byte)(key - 'a');
                } else {
                    return false;
                }
                return true;
            }
        }
    }
}
