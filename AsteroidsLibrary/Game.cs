using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using AsteroidsLibrary.SpaceObjects;

namespace AsteroidsLibrary
{
    public class Game
    {
        // for debug
        public delegate void MessageDelegate(string text);
        public event MessageDelegate MessageDelegateEvent;

        public Dictionary<SpaceObjectTypes, SpaceObjectAttributes> SpaceObjectMap { get; set; }

        private Border border;
        private int waveNum;
        private int enemiesPerWave;
        private int score;

        private float minAsteroidSpawnTime = 1000.0f;
        private float maxAsteroidSpawnTime = 5000.0f;
        private float minUfoSpawnTime = 4000.0f;
        private float maxUfoSpawnTime = 12000.0f;

        private int ufoCount;
        private int asteroidCount;

        private Timer ufoTimer;
        private Timer asteroidTimer;

        private System.Random random;

        private static Game instance;

        private Game()
        {
            ResetOptions();
            random = new System.Random();
        }

        public static Game GetInstance()
        {
            if (instance == null)
                instance = new Game();
            return instance;
        }

        public void SetBorders(Border border)
        {
            this.border = border;
        }

        public void AddUnit(SpaceObjectTypes type, Vector2 size, float speed, int scoreForDestroy)
        {
            SpaceObjectMap.Add(type,
                new SpaceObjectAttributes(size, Vector3.zero, speed, scoreForDestroy));
        }

        public SpaceObjectAttributes GetUnitOfType(SpaceObjectTypes type)
        {
            return SpaceObjectMap[type];
        }

        public void StopGame()
        {
            ResetOptions();
            ufoTimer.Dispose();
            asteroidTimer.Dispose();
        }

        public void StartSpawnObjects()
        {
            SynchronizationContext mainContext = SynchronizationContext.Current;
            asteroidTimer = new Timer(SpawnAsteroid, mainContext, 10, Timeout.Infinite);
            ufoTimer = new Timer(SpawnUfo, mainContext, 10, Timeout.Infinite);
        }

        private void SpawnUfo(object state)
        {
            if (ufoCount == enemiesPerWave * waveNum)
            {
                waveNum++;
                ufoCount = 0;
            }

            SynchronizationContext mainContext = state as SynchronizationContext;
            mainContext.Send(SpawnSignal, SpaceObjectTypes.Ufo);
            ufoCount++;
            int time = random.Next((int)(minUfoSpawnTime / waveNum),
                                   (int)(maxUfoSpawnTime / waveNum));
            ufoTimer.Change(time, time);
        }

        private void SpawnAsteroid(object state)
        {
            if (asteroidCount == enemiesPerWave * waveNum)
            {
                waveNum++;
                asteroidCount = 0;
            }

            SynchronizationContext mainContext = state as SynchronizationContext;
            mainContext.Send(SpawnSignal, SpaceObjectTypes.Asteroid);
            asteroidCount++;

            int time = random.Next((int)(minAsteroidSpawnTime / waveNum),
                                   (int)(maxAsteroidSpawnTime / waveNum));
            asteroidTimer.Change(time, time);
        }

        private void SpawnSignal(object state)
        {
            SpaceObjectTypes type = (SpaceObjectTypes)state;
            ObjectSpawner.SpawnBehindBorder(type, border);
        }

        private void ResetOptions()
        {
            SpaceObjectMap = new Dictionary<SpaceObjectTypes, SpaceObjectAttributes>();

            waveNum = 1;
            enemiesPerWave = 10;
            score = 0;

            ufoCount = 0;
            asteroidCount = 0;
        }
    }

    [System.Serializable]
    public struct Border
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }
}
