using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UniversalButtons : MonoBehaviour
{
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("WarningGiven",0); //reset warning given from explosive mode when game is quit from any way
    }
    //methods for the other buttons
    public void ReloadScene()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        EventSystem.current.SetSelectedGameObject(null);
        //reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadOpeningMenu()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        EventSystem.current.SetSelectedGameObject(null);
        //load home screen
        SceneManager.LoadScene("OpeningMenu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
