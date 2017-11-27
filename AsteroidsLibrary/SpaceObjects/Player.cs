using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class Player : SpaceObject, IFiring
    {
        public Player(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        { }

        public override void OnSpaceObjectDestroyed(object sender, Vector3 position, SpaceObject killer)
        {
            base.OnSpaceObjectDestroyed(sender, position, killer);
            SpaceObjectDestroyedEvent -= Game.GetInstance().GameOver;
        }

        // IFiring
        public SpaceObject Shoot(SpaceObjectTypes type, Vector3 position)
        {
            return ObjectSpawner.SpawnOnPosition(SpaceObjectTypes.Bullet, position, false);
        }
    }
}
