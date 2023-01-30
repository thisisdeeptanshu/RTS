using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace RTS
{
    public enum Type
    {
        Grass,
        Sand,
        Water,
        Tree,
        Chest,
        Player
    }

    public class Object
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w = 300;
        public int h = 300;
        [JsonConverter(typeof(StringEnumConverter))]
        public Type type { get; set; }
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
        public int w = 300;
        public int h = 300;
        [JsonConverter(typeof(StringEnumConverter))]
        public Type type { get; set; }
    }
}
