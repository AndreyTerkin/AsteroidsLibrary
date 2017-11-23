using System;

namespace AsteroidsLibrary.SpaceObjects
{
    public class SpaceObjectDestroyedEventArgs
    {
        public int ScoresForDestroy { get; set; }

        public SpaceObjectDestroyedEventArgs(int score)
        {
            ScoresForDestroy = score;
        }
    }
}
