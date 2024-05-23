using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshakeCheckBox : MonoBehaviour
{
    //logic for toggle
    public Toggle screenShakeToggle;
    //set toggle based on preset values
    void Start()
    {
        //set screenshake to be on by default
        if (PlayerPrefs.HasKey("Screenshake"))
        {

            //when launching game determine previous saved setting
            if (PlayerPrefs.GetInt("Screenshake") == 1)
            {
                Debug.Log("saved 1 as screenshake");
                screenShakeToggle.isOn = true;
            }
            else
            {
                // Set the checkbox to unchecked
                Debug.Log("saved 0 as screenshake");
                screenShakeToggle.isOn = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Screenshake", 1);
            screenShakeToggle.isOn = true;
        }
    }
    public void UpdateScreenshakePrefs()
    {
        if (screenShakeToggle.isOn)
        {
            PlayerPrefs.SetInt("Screenshake", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Screenshake", 0);
            Debug.Log("set to 0");
        }
    }
}
