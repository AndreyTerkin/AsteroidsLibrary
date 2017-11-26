using System;
using System.Collections.Generic;
using UnityEngine;

using AsteroidsLibrary.SpaceObjects;

namespace AsteroidsLibrary.Movers
{
    public class MoverRelativeNearestObject : IMovable
    {
        private SpaceObjectTypes aimType;
        private float speed;
        private Vector3 aimPosition;

        /// <summary>
        /// Create instance of MoverRelativeNearestObject class
        /// </summary>
        /// <param name="aimType">Object type for relative movement of mover. For correct work must be SpaceObject or subclass type</param>
        /// <param name="speed">Movement speed. Positive means movement towards aim, negative - away from aim</param>
        public MoverRelativeNearestObject(SpaceObjectTypes aimType, float speed)
        {
            this.aimType = aimType;
            this.speed = speed;
        }

        public Vector3 UpdatePosition(Vector3 currentPosition)
        {
            List<SpaceObject> objects = Game.GetInstance().ObjectMap[aimType];
            if (objects.Count == 0)
                return currentPosition;

            
            float minDistance = float.MaxValue;
            SpaceObject nearestObject = null;
            foreach (var spaceObject in objects)
            {
                float distance = Vector3.Distance(currentPosition, spaceObject.Position);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    nearestObject = spaceObject;
                }
            }

            if (nearestObject != null)
            {
                nearestObject.PositionChangedEvent += ChangeDirection;
            }

            return Vector3.MoveTowards(currentPosition,
                                       aimPosition,
                                       speed * Time.deltaTime);
        }

        private void ChangeDirection(object sender, SpaceObjectPositionChangedEventArgs e)
        {
            aimPosition = e.Position;
        }
    }
}
