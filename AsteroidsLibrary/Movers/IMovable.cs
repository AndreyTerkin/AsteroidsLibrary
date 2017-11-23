using System;
using UnityEngine;

namespace AsteroidsLibrary.Movers
{
    public interface IMovable
    {
        Vector3 UpdatePosition(Vector3 currentPosition);
    }
}
