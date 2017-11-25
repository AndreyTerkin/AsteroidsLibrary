using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    class Asteroid : SpaceObject
    {
        public Asteroid(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        {
        }

        public override void OnSpaceObjectDestroyed(object sender, Vector3 position, int score)
        {
            if (position != Vector3.zero)
            {
                for (int i = 0; i < 2; i++)
                {
                    ObjectSpawner.SpawnOnPosition(SpaceObjectTypes.AsteroidFragment, position);
                }
            }

            base.OnSpaceObjectDestroyed(sender, position, score);
        }
    }
}
