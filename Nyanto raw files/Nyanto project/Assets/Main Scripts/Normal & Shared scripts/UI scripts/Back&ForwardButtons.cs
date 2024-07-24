using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BacknForwardButtons : MonoBehaviour
{
    [SerializeField] private GameObject previousPage;
    [SerializeField] private GameObject currentPage;
    [SerializeField] private GameObject nextPage;

    public void GoBack()
    {
        ResetCurrentPage();
        previousPage.SetActive(true);
    }
    public void GoForward()
    {
        ResetCurrentPage();
        nextPage.SetActive(true);
    }
    private void ResetCurrentPage()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXButtonClick);
        EventSystem.current.SetSelectedGameObject(null);
        currentPage.SetActive(false);
    }
}
