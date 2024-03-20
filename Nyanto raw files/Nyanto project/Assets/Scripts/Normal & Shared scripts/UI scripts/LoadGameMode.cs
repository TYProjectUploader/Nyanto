using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoadGameMode : MonoBehaviour
{
    public void NormalModeSelect()
    {
        ResetCurrentPage();
        //reload current scene
        SceneManager.LoadScene("NormalMode");
    }
    public void BouncyModeSelect()
    {
        ResetCurrentPage();
        //reload current scene
        SceneManager.LoadScene("BouncyMode");
    }
    public void ExplosiveModeSelect()
    {
        ResetCurrentPage();
        //reload current scene
        SceneManager.LoadScene("ExplosiveModeWarning");
    }

    private void ResetCurrentPage()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
