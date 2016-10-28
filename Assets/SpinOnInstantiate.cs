using UnityEngine;
using System.Collections;

public class SpinOnInstantiate : MonoBehaviour {


    public bool SpinOnStart = false;
    public float RotationSpeed = 2.0f;

    private bool _shouldRotate;
	// Use this for initialization
	void Start ()
	{
	    _shouldRotate = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(!SpinOnStart || !_shouldRotate) return;;
        RotateObject();
    }

    public void OnHoldStarted()
    {
        _shouldRotate = false;
    }

    public void RotateObject()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }
}
