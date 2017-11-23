using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class SpaceObjectPositionChangedEventArgs
    {
        public Vector3 Position { get; set; }

        public SpaceObjectPositionChangedEventArgs(Vector3 position)
        {
            Position = position;
        }
    }
}
