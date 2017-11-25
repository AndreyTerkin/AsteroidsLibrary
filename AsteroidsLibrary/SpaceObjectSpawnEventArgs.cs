using UnityEngine;

using AsteroidsLibrary.SpaceObjects;

namespace AsteroidsLibrary
{
    public class SpaceObjectSpawnEventArgs
    {
        public SpaceObject Object { get; protected set; }
        public Vector3 Position { get; protected set; }
        public Vector2 Direction { get; protected set; }

        public SpaceObjectSpawnEventArgs(SpaceObject spaceObject)
        {
            Object = spaceObject;
        }

        public SpaceObjectSpawnEventArgs(
            SpaceObject spaceObject,
            Vector3 position,
            Vector2 direction)
        {
            Object = spaceObject;
            Position = position;
            Direction = direction;
        }
    }
}
