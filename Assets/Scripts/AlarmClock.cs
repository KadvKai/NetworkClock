using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlarmClock : MonoBehaviour
{
    [SerializeField] private Clock _clock;
    [SerializeField] private TMP_InputField _alarmHour;
    [SerializeField] private TMP_InputField _alarmMinute;
    [SerializeField] private AlarmClockArrow _alarmClockArrow;
    [SerializeField] private GameObject _optionsPanel;
    [SerializeField] private GameObject _wakeUpCall;
    [SerializeField] private ScreenOrientationIndicator _indicator;
    private int _currentHour;
    private int _currentMinute;
    private int _currentAlarmHour = 0;
    private int _currentAlarmMinute = 0;
    private Vector2 _startReferenceResolution;
    private CanvasScaler _canvasScaler;
    private readonly int _canvasScalerModifier=1000;

    private void OnEnable()
    {
        _alarmClockArrow.gameObject.SetActive(true);
        _clock.NewHour += NewHour;
        _clock.NewMinute += NewMinute;
        _alarmClockArrow.NewAlarmClockHour += NewAlarmClockArrowHour;
        _alarmClockArrow.NewAlarmClockMinute += NewAlarmClockArrowMinute;
    }



    private void Start()
    {
        _canvasScaler = _indicator.GetComponent<CanvasScaler>();
        _startReferenceResolution = _canvasScaler.referenceResolution;
        _alarmHour.text = _currentAlarmHour.ToString("00");
        _alarmMinute.text = _currentAlarmMinute.ToString("00");
    }
    private void NewHour(int hour)
    {
        _currentHour = hour;
        CheckingAlarmTime();
    }
    private void NewMinute(int minute)
    {
        _currentMinute = minute;
        CheckingAlarmTime();
    }


    private void CheckingAlarmTime()
    {
        if (_currentHour == _currentAlarmHour && _currentMinute == _currentAlarmMinute)
        {
            _wakeUpCall.SetActive(true);
        }
    }

    private void NewAlarmClockArrowHour(int hour)
    {
        if (hour >= 0 && hour < 24)
        {
            _currentAlarmHour = hour;
            _alarmHour.text = _currentAlarmHour.ToString("00");
        }
    }
    private void NewAlarmClockArrowMinute(int minute)
    {
        if (minute >= 0 && minute < 60)
        {
            _currentAlarmMinute = minute;
            _alarmMinute.text = _currentAlarmMinute.ToString("00");
        }
    }

    public void NewAlarmClockHour(string hourstring)
    {
        if (int.TryParse(hourstring, out int hour) && hour >= 0 && hour < 24)
        {
            _currentAlarmHour = hour;
            _alarmClockArrow.AlarmClockHour(_currentAlarmHour);
        }
        _alarmHour.text = _currentAlarmHour.ToString("00");
    }
    public void NewAlarmClockMinute(string minutestring)
    {
        if (int.TryParse(minutestring, out int minute) && minute >= 0 && minute < 60)
        {
            _currentAlarmMinute = minute;
            _alarmClockArrow.AlarmClockMinute(_currentAlarmMinute);
        }
        _alarmMinute.text = _currentAlarmMinute.ToString("00");
    }
    public void HourButton(int hour)
    {
        _currentAlarmHour += hour;
        if (_currentAlarmHour >= 24) _currentAlarmHour -= 24;
        if (_currentAlarmHour < 0) _currentAlarmHour += 24;
        _alarmHour.text = _currentAlarmHour.ToString("00");
        _alarmClockArrow.AlarmClockHour(_currentAlarmHour);

    }
    public void MinuteButton(int minute)
    {
        _currentAlarmMinute += minute;
        if (_currentAlarmMinute >= 60) _currentAlarmMinute -= 60;
        if (_currentAlarmMinute < 0) _currentAlarmMinute += 60;
        _alarmMinute.text = _currentAlarmMinute.ToString("00");
        _alarmClockArrow.AlarmClockMinute(_currentAlarmMinute);
    }

    public void OnOff(float value)
    {
        if (value > 0) this.enabled = true;
        else this.enabled = false;
    }

    public void OptionsButton(bool enable)
    {
        if (enable)
        {
            if (_indicator.OrientationLandscape)
            {
                _canvasScaler.referenceResolution = _startReferenceResolution + Vector2.right * _canvasScalerModifier;
                _clock.transform.localPosition = new Vector2(500, 0);
                _optionsPanel.transform.localPosition = new Vector2(-700, 0);
            }
            else
            {
                _canvasScaler.referenceResolution = _startReferenceResolution + Vector2.up * _canvasScalerModifier;
                _clock.transform.localPosition = new Vector2(0, 500);
                _optionsPanel.transform.localPosition = new Vector2(0, -700);
            }
        }
        else
        {
            _canvasScaler.referenceResolution = _startReferenceResolution;
            _clock.transform.localPosition = new Vector2(0, 0);
        }
        _optionsPanel.SetActive(enable);
        _alarmClockArrow.enabled = enable;
    }

   

    private void OnDisable()
    {
        _alarmClockArrow.gameObject.SetActive(false);
        _clock.NewHour -= NewHour;
        _clock.NewMinute -= NewMinute;
        _alarmClockArrow.NewAlarmClockHour -= NewAlarmClockArrowHour;
        _alarmClockArrow.NewAlarmClockMinute -= NewAlarmClockArrowMinute;
    }
}
