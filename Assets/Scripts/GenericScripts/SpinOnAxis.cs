using UnityEngine;
using System.Collections;




public class SpinOnAxis : MonoBehaviour
{


    public enum Axis
    {
        X = 0, Y = 1, Z = 2
    }


    public float RotationSpeed = 2.0f;
    public Axis RotateAround;
    public bool CounterClockwise = false;
    public bool SpinAroundWorldCoordinates = true;


    private Vector3 _rotationAxis;
    private int _direction;
    void Start()
    {
        if (RotateAround == Axis.X) _rotationAxis = SpinAroundWorldCoordinates ? Vector3.right: transform.right;
        else if (RotateAround == Axis.Y) _rotationAxis = SpinAroundWorldCoordinates ? Vector3.up : transform.up;
        else if (RotateAround == Axis.Z) _rotationAxis = SpinAroundWorldCoordinates ? Vector3.forward : transform.forward;

        _direction = CounterClockwise ? -1 : 1;
    }


    void Update()
    {
        RotateObject();
    }

    public void RotateObject()
    {
        transform.Rotate(_rotationAxis * RotationSpeed, Space.Self);
    }
}
