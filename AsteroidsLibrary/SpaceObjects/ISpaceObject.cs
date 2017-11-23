using System;

namespace AsteroidsLibrary.SpaceObjects
{
    public interface ISpaceObject
    {
        SpaceObject SpaceObject { get; }
        int ScoresForDestroy { get; set; }

        void Explode();
    }
}
