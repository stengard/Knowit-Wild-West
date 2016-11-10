using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;

public class SpatialMappingFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.layer = SpatialMappingManager.Instance.PhysicsLayer;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
