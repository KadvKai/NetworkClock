using System;
using UnityEngine;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
    private DateTime _currentTime;
    private int _currentHour;
    private int _currentMinute;
    private int _currentSeconds;
    public event UnityAction<int> NewHour;
    public event UnityAction<int> NewMinute;
    public event UnityAction<int> NewSecond;

    private void Start()
    {
        _currentTime = DateTime.Now;
        var time = NetworkTime.GetNetworkTime();
        Debug.Log(time);
    }
    private void Update()
    {
        _currentTime = _currentTime.AddSeconds(Time.deltaTime);
        var currentHour = _currentTime.Hour;
        var currentMinute = _currentTime.Minute;
        var currentSeconds = _currentTime.Second;
        if (_currentHour!= currentHour)
        {
            _currentHour = currentHour;
            NewHour.Invoke(currentHour);
        }
        if (_currentMinute != currentMinute)
        {
            _currentMinute = currentMinute;
            NewMinute.Invoke(currentMinute);
        }
        if (_currentSeconds != currentSeconds)
        {
            _currentSeconds = currentSeconds;
            NewSecond.Invoke(currentSeconds);
        }
    }
}
