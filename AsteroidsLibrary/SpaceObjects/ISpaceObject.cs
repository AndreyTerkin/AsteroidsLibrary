using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public interface ISpaceObject
    {
        SpaceObject SpaceObject { get; set; }
        Vector2 Size { get; set; }
        float Speed { get; set; }
        int ScoresForDestroy { get; set; }

        void Explode(SpaceObject killer);
    }
}
