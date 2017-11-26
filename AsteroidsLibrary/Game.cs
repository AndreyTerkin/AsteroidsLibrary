using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using AsteroidsLibrary.SpaceObjects;

namespace AsteroidsLibrary
{
    public class Game
    {
        public delegate void ScoreUpdate(int score);
        public event ScoreUpdate ScoreUpdateEvent;

        public event Action GameOverEvent;

        // for debug
        public delegate void MessageDelegate(string text);
        public event MessageDelegate MessageDelegateEvent;

        public Dictionary<SpaceObjectTypes, SpaceObjectAttributes> SpaceObjectAttributesMap { get; set; }
        public Dictionary<SpaceObjectTypes, List<SpaceObject>> ObjectMap { get; set; }

        private Border border;
        private int waveNum;
        private int enemiesPerWave;
        private int score;

        private int ufoCount;
        private int asteroidCount;
        private System.Random random;

        private Timer ufoSpawnTimer;
        private Timer asteroidSpawnTimer;

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
            SpaceObjectAttributesMap.Add(type,
                new SpaceObjectAttributes(size, speed, scoreForDestroy));
        }

        public SpaceObjectAttributes GetUnitOfType(SpaceObjectTypes type)
        {
            return SpaceObjectAttributesMap[type];
        }

        public void StopGame()
        {
            ResetOptions();
            ufoSpawnTimer.Dispose();
            asteroidSpawnTimer.Dispose();
        }

        public void GameOver(object sender, SpaceObjectDestroyedEventArgs e)
        {
            GameOverEvent();
            StopGame();
        }

        public void UpdateScore(object sender, SpaceObjectDestroyedEventArgs e)
        {
            if (e != null)
            {
                score += e.ScoresForDestroy;
                ScoreUpdateEvent?.Invoke(score);
            }
        }

        public void StartSpawnObjects()
        {
            ObjectSpawner.SpawnOnPosition(SpaceObjectTypes.Player, Vector3.zero); // Spawn player

            SynchronizationContext mainContext = SynchronizationContext.Current;
            asteroidSpawnTimer = new Timer(SpawnAsteroid, mainContext, 10, Timeout.Infinite);
            ufoSpawnTimer = new Timer(SpawnUfo, mainContext, 10, Timeout.Infinite);
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
            int time = random.Next((int)(Ufo.minUfoSpawnTime / waveNum),
                                   (int)(Ufo.maxUfoSpawnTime / waveNum));
            ufoSpawnTimer.Change(time, time);
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

            int time = random.Next((int)(Asteroid.minAsteroidSpawnTime / waveNum),
                                   (int)(Asteroid.maxAsteroidSpawnTime / waveNum));
            asteroidSpawnTimer.Change(time, time);
        }

        private void SpawnSignal(object state)
        {
            SpaceObjectTypes type = (SpaceObjectTypes)state;
            ObjectSpawner.SpawnBehindBorder(type, border);
        }

        private void ResetOptions()
        {
            SpaceObjectAttributesMap = new Dictionary<SpaceObjectTypes, SpaceObjectAttributes>();

            ObjectMap = new Dictionary<SpaceObjectTypes, List<SpaceObject>>
            {
                { SpaceObjectTypes.Player, new List<SpaceObject>() },
                { SpaceObjectTypes.Asteroid, new List<SpaceObject>() },
                { SpaceObjectTypes.AsteroidFragment, new List<SpaceObject>() },
                { SpaceObjectTypes.Ufo, new List<SpaceObject>() }
            };

            waveNum = 1;
            enemiesPerWave = 10;
            score = 0;

            ufoCount = 0;
            asteroidCount = 0;
        }

        // Send message to sibscribed client
        public void SendMessage(string txt)
        {
            MessageDelegateEvent?.Invoke(txt);
        }
    }
}
