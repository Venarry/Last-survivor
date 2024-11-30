using UnityEngine;

namespace Inputs
{
    public interface IInputProvider
    {
        public Vector3 MoveDirection { get; }
    }
}