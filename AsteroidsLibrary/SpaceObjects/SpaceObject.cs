using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class SpaceObject
    {
        public delegate void SpaceObjectDestroyed(object sender, SpaceObjectDestroyedEventArgs e);
        public event SpaceObjectDestroyed SpaceObjectDestroyedEvent;

        public delegate void PositionChanged(object sender, SpaceObjectPositionChangedEventArgs e);
        public event PositionChanged PositionChangedEvent;

        public void OnSpaceObjectDestroyed(object sender, int score)
        {
            SpaceObjectDestroyedEvent?.Invoke(sender != null ? sender : this,
                new SpaceObjectDestroyedEventArgs(score));
        }

        public virtual void OnPositionChanged(object sender, Vector3 position)
        {
            PositionChangedEvent?.Invoke(sender != null ? sender : this,
                new SpaceObjectPositionChangedEventArgs(position));
        }
    }
}
