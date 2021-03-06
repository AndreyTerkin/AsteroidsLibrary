﻿using System;
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
        public Vector3 Position { get; set; }

        public IMovable Mover { get; set; }

        public SpaceObject(SpaceObjectTypes type, SpaceObjectAttributes attributes)
        {
            Type = type;
            Attributes = attributes;
        }

        public virtual void OnSpaceObjectDestroyed(object sender, Vector3 position, SpaceObject killer)
        {
            if (killer != null)
            {
                SpaceObjectDestroyedEvent?.Invoke(sender != null ? sender : this,
                    new SpaceObjectDestroyedEventArgs(position, Attributes.ScoreForDestroy));
            }
            if (SpaceObjectDestroyedEvent != null)
                SpaceObjectDestroyedEvent -= Game.GetInstance().UpdateScore;

            if (Game.GetInstance().ObjectMap.ContainsKey(Type)
                && Game.GetInstance().ObjectMap[Type]?.Count > 0)
            {
                Game.GetInstance().ObjectMap[Type].Remove(this);
            }
        }

        public virtual void OnPositionChanged(object sender, Vector3 position)
        {
            Position = position;
            PositionChangedEvent?.Invoke(sender != null ? sender : this,
                new SpaceObjectPositionChangedEventArgs(position));
        }
    }

    public enum SpaceObjectTypes
    {
        Player,
        Asteroid,
        AsteroidFragment,
        Ufo,
        Bullet,
        Laser
    }

    public struct SpaceObjectAttributes
    {
        public SpaceObjectAttributes(Vector2 size, float speed, int scoreForDestroy)
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
