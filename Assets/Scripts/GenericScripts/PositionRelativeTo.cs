using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionRelativeTo : MonoBehaviour
{

    public GameObject PositionRelativeToGameObject;
    public bool ToTheRight;
    public bool ToTheLeft;
    public bool Above;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float offset = 0;
        Vector3 newPosition = new Vector3();
	    if (ToTheRight)
	    {

	        offset = PositionRelativeToGameObject.GetComponent<Collider>().bounds.size.x/2 + 0.1f;
            newPosition = PositionRelativeToGameObject.transform.position + (PositionRelativeToGameObject.transform.right * offset);

        }
        else if (ToTheLeft)
        {
            offset= -(PositionRelativeToGameObject.GetComponent<Collider>().bounds.size.x/2 + 0.1f);
            newPosition = PositionRelativeToGameObject.transform.position + (PositionRelativeToGameObject.transform.right * offset);
        }
        else if(Above)
	    {
            offset = (PositionRelativeToGameObject.GetComponent<Collider>().bounds.size.y/2 + 0.1f);
            newPosition = PositionRelativeToGameObject.transform.position + (PositionRelativeToGameObject.transform.up * offset);
        }
	    StartCoroutine(PositionObject(gameObject.transform, gameObject.transform.position, newPosition, 0.3f));
	    StartCoroutine(RotateObject(gameObject.transform, gameObject.transform.rotation, PositionRelativeToGameObject.transform.rotation, 0.3f));
    }

    IEnumerator RotateObject(Transform thisTransform, Quaternion startRotation, Quaternion endRotation, float time)
    {
        Vector3 targetPoint = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z) - transform.position;
        endRotation = Quaternion.LookRotation(-targetPoint, Vector3.up);
        thisTransform.rotation = Quaternion.Lerp(startRotation, endRotation, Time.deltaTime);
        yield return 0;
    }

    IEnumerator PositionObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
            thisTransform.position = Vector3.Lerp(startPos, endPos, Time.deltaTime);
            yield return 0;    
    }


}
