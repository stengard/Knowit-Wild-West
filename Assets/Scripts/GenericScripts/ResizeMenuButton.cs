using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;

public class ResizeMenuButton : MonoBehaviour
{

    public enum ResizeMode
    {
        Enlarge = 0, Reduce = 1
    }

    public Text TextObject;
    public float _resizeStep = 0.1f;

    public ResizeMode CurrentResizeMode;

    private float _currentSize;

	// Use this for initialization
	void Start ()
	{
	    float.TryParse(TextObject.text, out _currentSize);
	    SetText();


	}

    void OnSelected()
    {
        float.TryParse(TextObject.text, out _currentSize);


        if (CurrentResizeMode == ResizeMode.Enlarge)
        {
            _currentSize = _currentSize + _resizeStep;

        }
        else
        {
            _currentSize = _currentSize - _resizeStep;
        }
        Debug.Log(_currentSize);
        if (_currentSize <= 0) _currentSize = _resizeStep;
        SetText();
    }


    void SetText()
    {
        TextObject.text = _currentSize + "";
    }
}
