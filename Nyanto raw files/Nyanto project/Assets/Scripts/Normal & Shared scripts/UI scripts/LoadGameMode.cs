using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class LoadGameMode : MonoBehaviour
{
    public void NormalModeSelect()
    {
        ResetCurrentPage();
        SceneManager.LoadScene("NormalMode");
    }
    public void BouncyModeSelect()
    {
        ResetCurrentPage();
        SceneManager.LoadScene("BouncyMode");
    }
    public void ExplosiveModeSelect()
    {
        ResetCurrentPage();
        if(PlayerPrefs.GetInt("WarningGiven",0) == 0)
        {
            //set warninggiven as true using 1 as a placeholder for true boolean, reset when game is closed.
            PlayerPrefs.SetInt("WarningGiven",1); 
            SceneManager.LoadScene("ExplosiveModeWarning");
        }
        else{ //skip warning if already played before
            SceneManager.LoadScene("ExplosiveMode");
        }
    }

    private void ResetCurrentPage()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
