using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientationIndicator : MonoBehaviour
{
    public bool OrientationLandscape { get; private set; }
    private void Start()
    {
        OnRectTransformDimensionsChange();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
        {
            OrientationLandscape=true;
        }
        else if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            OrientationLandscape = false;
        }
    }
}
