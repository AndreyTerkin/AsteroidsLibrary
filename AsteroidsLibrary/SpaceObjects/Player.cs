using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class Player : SpaceObject
    {
        public Player(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        { }

        public override void OnSpaceObjectDestroyed(object sender, Vector3 position, SpaceObjectTypes killer)
        {
            base.OnSpaceObjectDestroyed(sender, position, killer);
            SpaceObjectDestroyedEvent -= Game.GetInstance().GameOver;
        }
    }
}
