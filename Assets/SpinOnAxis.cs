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


    private Vector3 _rotationAxis;
    private int _direction;
    void Start()
    {
        if (RotateAround == Axis.X) _rotationAxis = Vector3.right;
        else if (RotateAround == Axis.Y) _rotationAxis = Vector3.up;
        else if (RotateAround == Axis.Z) _rotationAxis = Vector3.forward;

        _direction = CounterClockwise ? -1 : 1;
    }


    void Update()
    {
        RotateObject();
    }

    public void RotateObject()
    {
        transform.Rotate(new Vector3(0, 5, 0), Space.Self);
    }
}
