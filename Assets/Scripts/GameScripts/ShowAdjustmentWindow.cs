using UnityEngine;
using System.Collections;
using HoloToolkit.Unity;


public class ShowAdjustmentWindow : MonoBehaviour
{

    public enum CurrentStatus
    {
        Idle = 1,
        WaitingForDelay = 2,
        DelayDone = 3,
        ShowingAdjustmentWindow = 4,
    }

    public float ShowWindowDelay;
    public GameObject AdjustmentWindow;
    public bool ShowOnGaze = false;
    public bool ShowOnKeyWord = true;

    private float _timeLeft;
    private bool _gazeHasEnteredObject = false;
    private CurrentStatus _currentStatus;
    private int _exitedCount;

    void Start()
    {
        _timeLeft = ShowWindowDelay;
        _currentStatus = CurrentStatus.Idle;
        AdjustmentWindow.SetActive(false);
    }

    void Update()
    {
        if (_currentStatus == CurrentStatus.Idle) return;

        _timeLeft -= Time.deltaTime;

        if (_timeLeft < 0 && _currentStatus != CurrentStatus.DelayDone)
        {
            _currentStatus = CurrentStatus.DelayDone;
            AdjustmentWindow.SetActive(true);
        }

    }

    public void ShowMenuRecognized()
    {
        if (!ShowOnKeyWord) return;

        var focusObject = GestureManager.Instance.FocusedObject;

        if (focusObject == gameObject)
        {
            if (_currentStatus == CurrentStatus.Idle)
            {
                _currentStatus = CurrentStatus.WaitingForDelay;

            }
            else
            {
                HideMenu();
            }
        }
    }


    void OnGazeEnter()
    {
        if (!ShowOnGaze) return;
        _currentStatus = CurrentStatus.WaitingForDelay;
    }

    void OnGazeLeave()
    {
        if (!ShowOnGaze) return;
        _exitedCount++;
        if (_exitedCount > 1)
        {
            HideMenu();
        }
    }

    private void HideMenu()
    {
        Debug.Log("Gömmer rutan");
        _timeLeft = ShowWindowDelay;
        _currentStatus = CurrentStatus.Idle;
        _exitedCount = 0;
        AdjustmentWindow.SetActive(false);
    }
}
