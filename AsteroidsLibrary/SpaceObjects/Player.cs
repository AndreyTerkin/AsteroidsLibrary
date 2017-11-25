using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsLibrary.SpaceObjects
{
    class Player : SpaceObject
    {
        public Player(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        { }
    }
}
