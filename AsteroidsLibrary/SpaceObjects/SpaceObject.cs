using System;
using UnityEngine;

using AsteroidsLibrary.Movers;

namespace AsteroidsLibrary.SpaceObjects
{
    public class SpaceObject
    {
        public delegate void SpaceObjectDestroyed(object sender, SpaceObjectDestroyedEventArgs e);
        public event SpaceObjectDestroyed SpaceObjectDestroyedEvent;

        public delegate void PositionChanged(object sender, SpaceObjectPositionChangedEventArgs e);
        public event PositionChanged PositionChangedEvent;

        public SpaceObjectTypes Type { get; set; }
        public SpaceObjectAttributes Attributes { get; set; }

        public IMovable Mover { get; set; }

        // TODO: temporary hack!!!
        public SpaceObject()
        { }

        public SpaceObject(SpaceObjectTypes type, SpaceObjectAttributes attributes)
        {
            Type = type;
            Attributes = attributes;
        }

        public virtual void OnSpaceObjectDestroyed(object sender, Vector3 position, int score)
        {
            SpaceObjectDestroyedEvent?.Invoke(sender != null ? sender : this,
                new SpaceObjectDestroyedEventArgs(position, score));
        }

        public virtual void OnPositionChanged(object sender, Vector3 position)
        {
            PositionChangedEvent?.Invoke(sender != null ? sender : this,
                new SpaceObjectPositionChangedEventArgs(position));
        }
    }

    public enum SpaceObjectTypes
    {
        Player,
        Asteroid,
        AsteroidFragment,
        Ufo
    }

    public struct SpaceObjectAttributes
    {
        public SpaceObjectAttributes(Vector2 size, Vector3 position, float speed, int scoreForDestroy)
        {
            Size = size;
            Speed = speed;
            ScoreForDestroy = scoreForDestroy;
        }

        public Vector2 Size { get; set; }
        public float Speed { get; set; }
        public int ScoreForDestroy { get; set; }
    }
}
