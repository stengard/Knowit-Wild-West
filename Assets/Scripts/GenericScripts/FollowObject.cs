using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour
{

    public GameObject ObjectToFollow;
    public float OffSetY;
    public float OffSetX;
    public float OffSetZ;

    private Vector3 _offsetVector;
    // Use this for initialization
    void Start () {
	    _offsetVector = new Vector3(OffSetX, OffSetY, OffSetZ);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    StartCoroutine(Move());
	    Rotate();
	}

    IEnumerator Move()
    {
        yield return StartCoroutine(MoveObject.use.TranslateTo(gameObject.transform, ObjectToFollow.transform.position + _offsetVector, 0.3f, MoveObject.MoveType.Time));

    }

    void Rotate()
    {
        gameObject.transform.rotation = ObjectToFollow.transform.rotation;

    }
}
