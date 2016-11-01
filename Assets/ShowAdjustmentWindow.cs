using UnityEngine;
using System.Collections;



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

    void OnGazeEnter()
    {
        _currentStatus = CurrentStatus.WaitingForDelay;
    }

    void OnGazeLeave()
    {

        _exitedCount++;
        if (_exitedCount > 1)
        {
            Debug.Log("Gömmer rutan");
            _timeLeft = ShowWindowDelay;
            _currentStatus = CurrentStatus.Idle;
            _exitedCount = 0;
            AdjustmentWindow.SetActive(false);
        }
    }
}
