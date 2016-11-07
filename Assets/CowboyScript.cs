using UnityEngine;
using System.Collections;

public class CowboyScript : MonoBehaviour {


    private Animator _animator;
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }

    void OnSelect()
    {
        //Debug.Log("Klickade Cowboy");
        //_animator.Play("stand_still_idle");
        //_animator.Play("stand_still_idle");
        ////_animator.SetBool("SitAndLook", true);
    }

    // Update is called once per frame
    void Update () {
	    
	}
}
