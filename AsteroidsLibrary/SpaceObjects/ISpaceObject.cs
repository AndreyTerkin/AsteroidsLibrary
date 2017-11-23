using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsLibrary.SpaceObjects
{
    public interface ISpaceObject
    {
        SpaceObject SpaceObject { get; }

        void Explode();
    }
}
