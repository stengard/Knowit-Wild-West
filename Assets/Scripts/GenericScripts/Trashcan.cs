﻿using UnityEngine;
using System.Collections;
using Assets;
using HoloToolkit.Unity;

[RequireComponent(typeof(AudioSource))]
public class Trashcan : MonoBehaviour
{


    public GameObject Lid;
    public AudioClip OpenLidSound;
    public AudioClip CloseLidSound;
    public AudioClip SuccesfulDeleteSound;


    private bool _objecInTrashcan;
    private Transform _lastObjectInTrashcan;
    private Vector3 _originalSize;
    private Collider _currentCollider;
    private Vector3 _originalLidLocalPosition;

    // Use this for initialization
    void Start ()
    {
        _originalLidLocalPosition = Lid.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update ()
	{

        if (_objecInTrashcan && GestureManager.Instance.FocusedObject && !IsCurrentFocusedObjectThis())
        {
            if (!GestureManager.Instance.hasHoldStarted)
            {
                Destroy(GestureManager.Instance.FocusedObject);
                _objecInTrashcan = false;
                ResetLid();

                gameObject.GetComponent<AudioSource>().PlayOneShot(SuccesfulDeleteSound);
            }
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == TagHelper.CURSOR_TAG || IsCurrentFocusedObjectThis()) return;
        SetRenderers(GestureManager.Instance.FocusedObject, true);
        gameObject.GetComponent<AudioSource>().PlayOneShot(CloseLidSound);
        ResetLid();
        _objecInTrashcan = false;
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.tag == TagHelper.CURSOR_TAG || IsCurrentFocusedObjectThis()) return;
        gameObject.GetComponent<AudioSource>().PlayOneShot(OpenLidSound);
        StartCoroutine(RemoveLid());
        SetRenderers(GestureManager.Instance.FocusedObject, false);
        _objecInTrashcan = true;
    }

    private void SetRenderers(GameObject go, bool enable)
    {
        if (!go) return;
        if (go.GetComponent<Renderer>())
        {
            go.GetComponent<Renderer>().enabled = enable;
        }

        Renderer[] renderersInChildren = go.GetComponentsInChildren<Renderer>();

        foreach (var renderersInChild in renderersInChildren)
        {
            renderersInChild.enabled = enable;
        }
        
    }

    private bool IsCurrentFocusedObjectThis()
    {
        return GestureManager.Instance.FocusedObject == gameObject;
    }

    IEnumerator RemoveLid()
    {
        Vector3 newPostition = Lid.transform.position + Lid.transform.up*0.3f;
        Debug.Log(newPostition);
        yield return StartCoroutine(MoveObject.use.TranslateTo(Lid.transform, newPostition, 0.3f, MoveObject.MoveType.Time));
    }

    void ResetLid()
    {
        Lid.transform.localPosition = _originalLidLocalPosition;
    }
}
