using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class SpaceObject
    {
        public delegate void SpaceObjectDestroyed(int score);
        public event SpaceObjectDestroyed SpaceObjectDestroyedEvent;

        public delegate void PositionChanged(Vector3 position);
        public event PositionChanged PositionChangedEvent;

        public void OnSpaceObjectDestroyed(int score)
        {
            SpaceObjectDestroyedEvent?.Invoke(score);
        }

        public virtual void OnPositionChanged(Vector3 position)
        {
            PositionChangedEvent?.Invoke(position);
        }
    }
}
