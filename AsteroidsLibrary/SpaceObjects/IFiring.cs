using System;
using UnityEngine;

namespace AsteroidsLibrary.SpaceObjects
{
    public interface IFiring
    {
        SpaceObject Shoot(SpaceObjectTypes type, Vector3 position);
    }
}
