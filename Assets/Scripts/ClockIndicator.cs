using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Clock))]
public class ClockIndicator : MonoBehaviour
{
    [Header("AnalogClock")]
    [SerializeField] private Image _hourArrow;
    [SerializeField] private Image _minuteArrow;
    [SerializeField] private Image _secondArrow;
    [Header("DigitalClock")]
    [SerializeField] private TMP_Text _hour;
    [SerializeField] private TMP_Text _minute;
    [SerializeField] private TMP_Text _second;
    private Clock _clock;
    private int _currentHour;
    private int _currentMinute;
    private int _currentSeconds;
    private const float _hourRotationFactor = 360 / 12 / 60f;
    private const float _minuteRotationFactor = 360 / 60 / 60f;
    private const float _secondRotationFactor = 360 / 60f;
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
        AnalogClockArrowsIndicator();
        DigitalClockIndicator();
    }

    private void AnalogClockArrowsIndicator()
    {
        _hourArrow.transform.rotation = Quaternion.Euler(0, 0, -(_currentHour * 60 + _currentMinute) * _hourRotationFactor);
        _minuteArrow.transform.rotation = Quaternion.Euler(0, 0, -(_currentMinute * 60 + _currentSeconds) * _minuteRotationFactor);
        _secondArrow.transform.rotation = Quaternion.Euler(0, 0, -_currentSeconds * _secondRotationFactor);
    }

    private void DigitalClockIndicator()
    {
        _hour.text = _currentHour.ToString("00");
        _minute.text = _currentMinute.ToString("00");
        _second.text = _currentSeconds.ToString("00");
    }

    private void OnDestroy()
    {
        _clock.NewHour -= NewHour;
        _clock.NewMinute -= NewMinute;
        _clock.NewSecond -= NewSecond;
    }
}
