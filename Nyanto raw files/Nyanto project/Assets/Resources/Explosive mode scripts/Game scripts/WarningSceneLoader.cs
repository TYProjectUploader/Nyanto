using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WarningSceneLoader : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("ExplosiveMode");
    }
}
