using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TutorialButtonNav : MonoBehaviour
{
    [Header("--tutorial pages--")]
    [SerializeField] private GameObject[] tutePages;
    [SerializeField] private GameObject tuteInstantiator;
    private GameObject currentTutePage;

    [Header("---Buttons---")]
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private TutorialButtonBehaviour leftButtonBehaviour;
    [SerializeField] private TutorialButtonBehaviour rightButtonBehaviour;

    private int currentPage = 0;
    public bool lastButtonWasRight;

    void OnEnable() //Reset the page to 0 on the tutorial being activated
    {
        lastButtonWasRight = true;
        currentPage = 0;
        UpdatePage();
    }

    public void RightButton()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        currentPage ++;
        Debug.Log(currentPage);
        lastButtonWasRight = true;
        UpdatePage();
    }
    
    public void LeftButton()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        currentPage --;
        Debug.Log(currentPage);
        lastButtonWasRight = false;
        UpdatePage();
    }
    private void UpdatePage()
    {
        if (currentPage == tutePages.Length-1) //if at end of page set right button inactive
        {
            rightButton.SetActive(false);
            rightButton.GetComponent<Outline>().enabled=false;
            rightButton.GetComponent<TutorialButtonBehaviour>().isHovered = false;
        }
        else
        {
            rightButton.SetActive(true);
        }
        if (currentPage == 0)
        {
            leftButton.SetActive(false);
            leftButton.GetComponent<Outline>().enabled=false;
            leftButton.GetComponent<TutorialButtonBehaviour>().isHovered = false;
        }
        else
        {
            leftButton.SetActive(true);
        }
        if (currentTutePage != null)
        {
            Destroy(currentTutePage);
        }
        //spawn in the required tutorial page
        currentTutePage = Instantiate(tutePages[currentPage], tuteInstantiator.transform);
    }
}
