using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloorPlaneFix : MonoBehaviour {

    public enum FixFeature
    {
        Higher = 0, Lower = 1, Toggle = 2
    }

    public FixFeature Feature;
    public GameObject FloorObject;
    public Material OriginalMaterial;
    public Material TransparentMaterial;

    private bool  _toggleMaterial = true;
    // Use this for initialization
    void Start () {
	}

    public void Go()
    {

    }

    // Update is called once per frame
    void Update () {
	
	}


    void OnSelected()
    {
        if (Feature == FixFeature.Higher) HigherUp();
        if (Feature == FixFeature.Lower) LowerDown();
        if (Feature == FixFeature.Toggle) ToggleMaterial();
    }

    private void ToggleMaterial()
    {
        if (_toggleMaterial)
        {
            FloorObject.GetComponentInChildren<Renderer>().material = TransparentMaterial;
            _toggleMaterial = false;
        }
        else
        {
            FloorObject.GetComponentInChildren<Renderer>().material = OriginalMaterial;
            _toggleMaterial = true;
        }
    }

    void OnGazeEnter()
    {
        GetComponent<Button>().Select();  
    }

    private void LowerDown()
    {
        FloorObject.transform.position -= FloorObject.transform.up*0.05f;
    }

    private void HigherUp()
    {
        FloorObject.transform.position += FloorObject.transform.up * 0.05f;
    }
}
