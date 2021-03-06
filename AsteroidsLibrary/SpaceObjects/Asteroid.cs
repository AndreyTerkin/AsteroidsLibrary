﻿using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class Asteroid : SpaceObject
    {
        public static float minAsteroidSpawnTime = 1000.0f;
        public static float maxAsteroidSpawnTime = 5000.0f;

        private int fragmentCount = 2;

        public Asteroid(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        {}

        public override void OnSpaceObjectDestroyed(object sender, Vector3 position, SpaceObject killer)
        {
            base.OnSpaceObjectDestroyed(sender, position, killer);
            if (killer != null)
            {
                for (int i = 0; i < fragmentCount; i++)
                {
                    ObjectSpawner.SpawnOnPosition(SpaceObjectTypes.AsteroidFragment, position);
                }
            }
        }
    }
}
