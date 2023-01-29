using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace RTS
{
    public class Object
    {
        public int x { get; set; }
        public int y { get; set; }
        public string type { get; set; }
    }

    public class Root
    {
        public List<Square> Squares { get; set; }
        public List<Object> Objects { get; set; }
    }

    public class Square
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w = 100;
        public int h = 100;
        [JsonConverter(typeof(StringEnumConverter))]
        public Type type { get; set; }

        public enum Type
        {
            Grass,
            Sand,
            Water
        }

        public Color GetColour()
        {
            if (this.type == Type.Grass) return Color.LightSeaGreen;
            if (this.type == Type.Sand) return Color.LightGoldenrodYellow;
            if (this.type == Type.Water) return Color.BlueViolet;
            else return Color.White;
        }
    }
}
