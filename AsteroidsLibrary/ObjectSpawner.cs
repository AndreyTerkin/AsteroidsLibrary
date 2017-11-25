using UnityEngine;

namespace AsteroidsLibrary
{
    public class ObjectSpawner
    {
        public static void InitSpawnParameters(Border border, Vector2 size, ref Vector3 position, ref Vector2 direction)
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
