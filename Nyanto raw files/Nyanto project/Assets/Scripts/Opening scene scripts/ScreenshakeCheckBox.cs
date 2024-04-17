using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshakeCheckBox : MonoBehaviour
{
    //logic for toggle
    public Toggle screenShakeToggle;
    //set toggle based on preset values
    void Awake()
    {
        if (PlayerPrefs.GetInt("Screenshake", 1) == 1)
        {
            screenShakeToggle.isOn = true;
        }
        else
        {
            // Set the checkbox to unchecked
            screenShakeToggle.isOn = false;
        }
    }
    public void updateScreenshakePrefs()
    {
        if (screenShakeToggle.isOn)
        {
            PlayerPrefs.SetInt("Screenshake", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Screenshake", 0);
        }
    }
}
