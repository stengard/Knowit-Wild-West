using UnityEngine;
using System.Collections;

public class OnTap : MonoBehaviour
{

    public GameObject ObjectToHide;
    public bool HideOnTap = true;
    public bool RemoveOnTap = true;

    void OnSelect()
    {
        if (HideOnTap)
        {
            HideObject();
        }
        else if(RemoveOnTap)
        {
            RemoveObject();
        }
    }

    private void HideObject()
    {
        ObjectToHide.SetActive(!ObjectToHide.activeInHierarchy);
    }

    private void RemoveObject()
    {

    }
}
