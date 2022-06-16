using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NetworkTime))]
public class Clock : MonoBehaviour, IPointerClickHandler
{
    private DateTime _currentTime;
    private int _currentHour;
    private int _currentMinute;
    private int _currentSeconds;
    private NetworkTime _networkTime;
    private readonly bool _synchronizationEnable=true;
    private readonly bool _cyclicSynchronization=true;
    private readonly int _timeCyclicSynchronization = 60;//в минутах
    public event UnityAction<int> NewHour;
    public event UnityAction<int> NewMinute;
    public event UnityAction<int> NewSecond;


    private void Awake()
    {
        _networkTime = GetComponent<NetworkTime>();
        _networkTime.NewNetworkTime += NewNetworkTime;
    }
   
    private void Start()
    {
        _currentTime = DateTime.Now;
        if(_synchronizationEnable) Synchronization();
    }
    private void Synchronization()
    {
        _networkTime.Synchronization();
        if (_cyclicSynchronization) Invoke(nameof (Synchronization), _timeCyclicSynchronization*60);
    }

    private void NewNetworkTime(DateTime newNetworkTime, string arg1)
    {
        _currentTime = newNetworkTime;
    }

    private void Update()
    {
        _currentTime = _currentTime.AddSeconds(Time.deltaTime);
        var currentHour = _currentTime.Hour;
        var currentMinute = _currentTime.Minute;
        var currentSeconds = _currentTime.Second;
        if (_currentHour != currentHour)
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
    private void OnDestroy()
    {
        _networkTime.NewNetworkTime -= NewNetworkTime;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
    }
}
