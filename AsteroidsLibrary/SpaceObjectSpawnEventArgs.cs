using UnityEngine;

using AsteroidsLibrary.SpaceObjects;

namespace AsteroidsLibrary
{
    public class SpaceObjectSpawnEventArgs
    {
        public SpaceObjectTypes ObjectType { get; protected set; }
        public SpaceObjectAttributes Attributes { get; protected set; }
        public Vector3 Position { get; protected set; }
        public Vector2 Direction { get; protected set; }

        public SpaceObjectSpawnEventArgs(
            SpaceObjectTypes objectType,
            SpaceObjectAttributes attributes)
        {
            ObjectType = objectType;
            Attributes = attributes;
        }

        public SpaceObjectSpawnEventArgs(
            SpaceObjectTypes objectType,
            SpaceObjectAttributes attributes,
            Vector3 position,
            Vector2 direction)
        {
            ObjectType = objectType;
            Attributes = attributes;
            Position = position;
            Direction = direction;
        }
    }
}
