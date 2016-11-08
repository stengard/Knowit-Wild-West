using UnityEngine;
using System.Collections;

public class LightGestureManager : MonoBehaviour {

    public Light Light;
    public GameObject MenuGameObject;
    public float MaxDistance;
    public float MaxIntensity;

    private bool _holdStarted;
    private float _defaultLightDistance;
    private float _defaultLightIntensity;
    private bool _showMenu;
    // Use this for initialization
    void Start ()
    {
        _showMenu = true;
        _defaultLightIntensity = Light.intensity;
        _defaultLightDistance = Light.range;
        //Light.intensity = 0;
        Light.intensity = MaxIntensity;
        Light.range = MaxDistance;
    }




    // Update is called once per frame
    void Update ()
    {
        if (!_holdStarted) return;
        Light.range = Mathf.PingPong(Time.time * 2, MaxDistance);
        Light.intensity = Mathf.PingPong(Time.time * 1.5f, MaxIntensity);
    }


    void OnSelected()
    {
        _showMenu = !_showMenu;
        MenuGameObject.SetActive(_showMenu);
        if (_showMenu)
        {
            Light.intensity = MaxIntensity;
            Light.range = MaxDistance;
        }
        else
        {
            Light.intensity = _defaultLightIntensity;
            Light.range = _defaultLightDistance;
        }

    }

    void OnHoldStarted()
    {
        _holdStarted = true;
    }

    void OnHoldCanceled()
    {
        Light.intensity = 0;
        _holdStarted = false;
    }

    void OnHoldCompleted()
    {
        Light.intensity = 0;
        _holdStarted = false;
    }

    void OnGazeEnter()
    {
        if (!_showMenu)
        {
            Light.intensity = _defaultLightIntensity;
            Light.range = _defaultLightDistance;
        }

    }

    void OnGazeLeave()
    {
        if (!_showMenu) Light.intensity = 0;
    }
}
