using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public class Player : SpaceObject
    {
        public Player(SpaceObjectTypes type, SpaceObjectAttributes attributes)
            : base(type, attributes)
        { }

        public override void OnSpaceObjectDestroyed(object sender, Vector3 position, int score)
        {
            base.OnSpaceObjectDestroyed(sender, position, score);
            SpaceObjectDestroyedEvent -= Game.GetInstance().GameOver;
        }
    }
}
