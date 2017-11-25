using UnityEngine;

using AsteroidsLibrary.SpaceObjects;

namespace AsteroidsLibrary
{
    public class ObjectSpawner
    {
        public delegate void SpaceObjectSpawned(SpaceObjectSpawnEventArgs e);
        public static event SpaceObjectSpawned SpaceObjectSpawnEvent;

        /// <summary>
        /// Spawn object of type in random position behind any border with generated direction
        /// </summary>
        /// <param name="type">Type of SpaceObject</param>
        /// <param name="border">Borders of play area</param>
        public static void SpawnBehindBorder(
            SpaceObjectTypes type, Border border)
        {
            var attributes = Game.GetInstance().SpaceObjectMap[type];
            Vector3 position = Vector3.zero;
            Vector2 direction = new Vector2(1.0f, 1.0f);
            InitSpawnParameters(
                border, attributes.Size, ref position, ref direction);

            SpaceObject spaceObject = CreateSpaceObjectOfType(type, attributes);
            var arguments = new SpaceObjectSpawnEventArgs(spaceObject, position, direction);

            SpaceObjectSpawnEvent?.Invoke(arguments);
        }

        /// <summary>
        /// Spawn object of type in position with random direction
        /// </summary>
        /// <param name="type">Type of SpaceObject</param>
        /// <param name="position"></param>
        public static void SpawnOnPosition(SpaceObjectTypes type, Vector3 position)
        {
            var attributes = Game.GetInstance().SpaceObjectMap[type];
            Vector2 direction = new Vector2(
                Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

            SpaceObject spaceObject = CreateSpaceObjectOfType(type, attributes);
            var arguments = new SpaceObjectSpawnEventArgs(spaceObject, position, direction);

            SpaceObjectSpawnEvent?.Invoke(arguments);
        }

        private static SpaceObject CreateSpaceObjectOfType(
            SpaceObjectTypes type, SpaceObjectAttributes attributes)
        {
            switch (type)
            {
                case SpaceObjectTypes.Asteroid:
                    return new Asteroid(type, attributes);
                case SpaceObjectTypes.AsteroidFragment:
                    return new SpaceObject(type, attributes);
                case SpaceObjectTypes.Ufo:
                    return new SpaceObject(type, attributes);
                default:
                    return null;
            }
        }

        private static void InitSpawnParameters(Border border, Vector2 size, ref Vector3 position, ref Vector2 direction)
        {
            float deflection = Random.Range(-1.0f, 1.0f);
            int side = Random.Range(0, 4);

            switch (side)
            {
                case 0: // top
                    position = new Vector3(Random.Range(border.xMin, border.xMax),
                                           border.yMax + size.y,
                                           0.0f);
                    direction = new Vector3(deflection, -1.0f);
                    break;
                case 1: // bottom
                    position = new Vector3(Random.Range(border.xMin, border.xMax),
                                           border.yMin - size.y,
                                           0.0f);
                    direction = new Vector3(deflection, 1.0f);
                    break;
                case 2: // right
                    position = new Vector3(border.xMax + size.x,
                                           Random.Range(border.yMin, border.yMax),
                                           0.0f);
                    direction = new Vector3(-1.0f, deflection);
                    break;
                case 3: // left
                    position = new Vector3(border.xMin - size.x,
                                           Random.Range(border.yMin, border.yMax),
                                           0.0f);
                    direction = new Vector3(1.0f, deflection);
                    break;
                default:
                    break;
            }
        }
    }
}
