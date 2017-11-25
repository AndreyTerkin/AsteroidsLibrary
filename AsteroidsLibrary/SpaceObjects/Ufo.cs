using System;

using AsteroidsLibrary;
using AsteroidsLibrary.Movers;

namespace AsteroidsLibrary.SpaceObjects
{
    class Ufo : SpaceObject
    {
        public static float minUfoSpawnTime = 4000.0f;
        public static float maxUfoSpawnTime = 12000.0f;

        public Ufo(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        {
            var playerList = Game.GetInstance().ObjectMap[SpaceObjectTypes.Player];
            if (playerList.Count == 1)
            {
                Mover = new MoverRelativeConstantAim(playerList[0], Attributes.Speed);
            }
        }
    }
}
