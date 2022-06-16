using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlarmClockArrow : MonoBehaviour, IDragHandler
{
    private Image _image;
    private int _alarmClockHour;
    private int _alarmClockMinute;
    private const float _alarmClockRotationFactor = 360 / 12 / 60f;
    public event UnityAction<int> NewAlarmClockHour;
    public event UnityAction<int> NewAlarmClockMinute;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        _image.raycastTarget=true;
        NewAlarmClockHour += AlarmClockHour;
        NewAlarmClockMinute += AlarmClockMinute;
    }
    public void AlarmClockHour(int hour)
    {
        _alarmClockHour= hour;
        MovementAlarmClockArrows();
    }

    public void AlarmClockMinute(int minute)
    {
        _alarmClockMinute= minute;
        MovementAlarmClockArrows();
    }


    private void MovementAlarmClockArrows()
    {
        transform.rotation = Quaternion.Euler(0, 0, -(_alarmClockHour * 60 + _alarmClockMinute) * _alarmClockRotationFactor);
    }

    private void OnDisable()
    {
        _image.raycastTarget = false;
        NewAlarmClockHour -= AlarmClockHour;
        NewAlarmClockMinute -= AlarmClockMinute;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var direction = (eventData.position - (Vector2)transform.position).normalized;
        var angle=Vector2.SignedAngle(direction, Vector2.up);
        if (angle < 0) angle += 360;
        var totalMinute= (int)(angle / _alarmClockRotationFactor);
        var alarmClockHour = totalMinute / 60;
        var alarmClockMinute = totalMinute % 60;
        NewAlarmClockHour?.Invoke(alarmClockHour);
        NewAlarmClockMinute?.Invoke(alarmClockMinute);
    }
}
