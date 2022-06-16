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
    private int _currentHour;
    private int _currentMinute;
    private int _currentAlarmHour = 0;
    private int _currentAlarmMinute = 0;

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
            Debug.Log("Будильник");
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
            
        }
        _optionsPanel.SetActive(enable);
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
