﻿using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CallFunctionOnTap : MonoBehaviour {

    public UnityEvent Response;

    void OnSelect()
    {
        Response.Invoke();
    }
}