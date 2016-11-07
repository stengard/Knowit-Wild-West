using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShowAndHide : MonoBehaviour
{


    public GameObject ObjectToShow;
    public List<GameObject> ObjectsToHide;
	// Use this for initialization


    void OnGazeEnter()
    {
        gameObject.GetComponent<Button>().Select();
    }

    void OnSelected()
    {
        ObjectToShow.SetActive(true);

        foreach (GameObject o in ObjectsToHide)
        {
            o.SetActive(false);
        }
    }
}
