using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    class Asteroid : SpaceObject
    {
        public static float minAsteroidSpawnTime = 1000.0f;
        public static float maxAsteroidSpawnTime = 5000.0f;

        private int fragmentCount = 2;

        public Asteroid(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        {}

        public override void OnSpaceObjectDestroyed(object sender, Vector3 position, int score)
        {
            if (position != Vector3.zero)
            {
                for (int i = 0; i < fragmentCount; i++)
                {
                    ObjectSpawner.SpawnOnPosition(SpaceObjectTypes.AsteroidFragment, position);
                }
            }

            base.OnSpaceObjectDestroyed(sender, position, score);
        }
    }
}
