using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    class Player
    {
        public int x, y, w, h;
        public int hunger, health, thirst;
        public List<Type> inventory;

        public Player(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;

            this.hunger = 100;
            this.thirst = 25; // Thirst is more important than hunger soooo ¯\_(ツ)_/¯
            this.health = 100;

            this.inventory = new List<Type>();
        }
    }
}
