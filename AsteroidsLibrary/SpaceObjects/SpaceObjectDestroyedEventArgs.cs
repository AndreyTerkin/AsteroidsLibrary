using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class SpaceObjectDestroyedEventArgs
    {
        public int ScoresForDestroy { get; set; }
        public Vector3 Position { get; set; }

        public SpaceObjectDestroyedEventArgs(Vector3 position, int score)
        {
            ScoresForDestroy = score;
            Position = position;
        }
    }
}
