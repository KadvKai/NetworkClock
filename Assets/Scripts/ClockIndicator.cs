using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Clock))]
public class ClockIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _hourHand;
    [SerializeField] private SpriteRenderer _minuteHand;
    [SerializeField] private SpriteRenderer _secondHand;
    private Clock _clock;
    private int _currentHour;
    private int _currentMinute;
    private int _currentSeconds;
    private const float _hourRotationFactor=360/12/60f;
    private const float _minuteRotationFactor=360/60/60f;
    private const float _secondRotationFactor=360/60f;
    private void Awake()
    {
        _clock = GetComponent<Clock>();
        _clock.NewHour += NewHour;
        _clock.NewMinute += NewMinute;
        _clock.NewSecond += NewSecond;
    }
    private void NewHour(int hour)
    {
        _currentHour = hour;
    }
    private void NewMinute(int minute)
    {
        _currentMinute = minute;
    }
    private void NewSecond(int second)
    {
        _currentSeconds = second;
        MovementClockHands();
    }

    private void MovementClockHands()
    {
        _hourHand.transform.rotation = Quaternion.Euler(0, 0, -(_currentHour*60+ _currentMinute) * _hourRotationFactor);
        _minuteHand.transform.rotation = Quaternion.Euler(0, 0, -(_currentMinute*60+ _currentSeconds) * _minuteRotationFactor);
        _secondHand.transform.rotation=Quaternion.Euler(0,0, -_currentSeconds * _secondRotationFactor);
    }
    private void OnDestroy()
    {
        _clock.NewHour -= NewHour;
        _clock.NewMinute -= NewMinute;
        _clock.NewSecond -= NewSecond;
    }
}
