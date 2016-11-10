using UnityEngine;

namespace Assets.Scripts.GenericScripts
{
    public class MoveObject : MonoBehaviour
    {

        private bool _isMoving;
        // Called by GazeGestureManager when the user performs a Select gesture
        void OnSelected()
        {
            _isMoving = !_isMoving;
            // If the sphere has no Rigidbody component, add one to enable physics.

        }
    }
}
