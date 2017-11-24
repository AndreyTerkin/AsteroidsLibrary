using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AsteroidsLibrary.SpaceObjects;

namespace AsteroidsLibrary
{
    public class SpaceObjectSpawnEventArgs
    {
        public SpaceObjectTypes ObjectType { get; set; }

        public SpaceObjectSpawnEventArgs(SpaceObjectTypes objectType)
        {
            ObjectType = objectType;
        }
    }

    public class Game
    {
        public delegate void SpaceObjectSpawned(object sender, SpaceObjectSpawnEventArgs e);
        public event SpaceObjectSpawned SpaceObjectSpawnEvent;

        // for debug
        public delegate void MessageDelegate(string text);
        public event MessageDelegate MessageDelegateEvent;

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

        private Random random;

        private static Game instance;

        private Game()
        {
            ResetOptions();
            random = new Random();
        }

        public static Game GetInstance()
        {
            if (instance == null)
                instance = new Game();
            return instance;
        }

        public void StartSpawnObjects()
        {
            // TODO: Learn more about it
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
            SpaceObjectSpawnEvent?.Invoke(this, new SpaceObjectSpawnEventArgs(type));
        }

        public void StopGame()
        {
            ResetOptions();
            ufoTimer.Dispose();
            asteroidTimer.Dispose();
        }

        private void ResetOptions()
        {
            waveNum = 1;
            enemiesPerWave = 10;
            score = 0;

            ufoCount = 0;
            asteroidCount = 0;
        }
    }
}
