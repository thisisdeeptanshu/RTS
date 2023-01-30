using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTS
{
    class Player : Object
    {
        public int hunger, health, thirst;
        public List<Type> inventory;

        public Player(Object obj)
        {
            this.x = obj.x;
            this.y = obj.y;
            this.w = obj.w;
            this.h = obj.h;
            this.hunger = 100;
            this.thirst = 25; // Thirst is more important than hunger soooo ¯\_(ツ)_/¯
            this.health = 100;
            this.inventory = new List<Type>();
        }
    }
}
