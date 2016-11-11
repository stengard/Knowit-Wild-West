using UnityEngine;
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
        _objecInTrashcan = false;
    }
	
	// Update is called once per frame
	void Update ()
	{

        if (_objecInTrashcan && IsCurrentFocusedObjectDeletable())
        {
            if (!GestureManager.Instance.hasHoldStarted)
            {
                Debug.Log("Deleting " + GestureManager.Instance.FocusedObject.name);
                Destroy(GestureManager.Instance.FocusedObject);
                _objecInTrashcan = false;
                ResetLid();

                gameObject.GetComponent<AudioSource>().PlayOneShot(SuccesfulDeleteSound);
            }
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == TagHelper.CURSOR_TAG || !IsCurrentFocusedObjectDeletable()) return;
        SetRenderers(GestureManager.Instance.FocusedObject, true);
        gameObject.GetComponent<AudioSource>().PlayOneShot(CloseLidSound);
        ResetLid();
        _objecInTrashcan = false;
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == TagHelper.CURSOR_TAG || !IsCurrentFocusedObjectDeletable()) return;
        _objecInTrashcan = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == TagHelper.CURSOR_TAG || !IsCurrentFocusedObjectDeletable()) return;
        gameObject.GetComponent<AudioSource>().PlayOneShot(OpenLidSound);
        StartCoroutine(RemoveLid());
        SetRenderers(GestureManager.Instance.FocusedObject, false);
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

    private bool IsCurrentFocusedObjectDeletable()
    {
        //If current focused object is a SpawnerButton or null, or is the trashcan itself it shouldn't be deleted; I know....
        //Should have done an Deleteable script on every prefab instead maybe..
        if (GestureManager.Instance.FocusedObject == null) return false;
        else if (GestureManager.Instance.FocusedObject.name == "Placer for Spawner") return false;
        else if (GestureManager.Instance.FocusedObject.GetComponent<LightGestureManager>() != null) return false;
        else if (GestureManager.Instance.FocusedObject.GetComponent<CanvasRenderer>() != null) return false;
        else if(GestureManager.Instance.FocusedObject.GetComponent<SpawnerButton>() != null) return false;
        else if(GestureManager.Instance.FocusedObject == gameObject) return false;

        return true;
    }

    IEnumerator RemoveLid()
    {
        Vector3 newPostition = Lid.transform.position + Lid.transform.up*0.3f;
        yield return StartCoroutine(MoveObject.use.TranslateTo(Lid.transform, newPostition, 0.3f, MoveObject.MoveType.Time));
    }

    void ResetLid()
    {
        Lid.transform.localPosition = _originalLidLocalPosition;
    }
}
