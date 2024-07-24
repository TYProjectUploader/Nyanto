using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpeningMenuUINav : MonoBehaviour //opening ui nav with keyboard.
{
    [Header("---Input actions---")]
    [SerializeField] private InputActionReference cursorMovementDelta;
    [SerializeField] private InputActionReference Navigation;
    [SerializeField] private InputActionReference Submit;
    [SerializeField] private InputActionReference Click;
 
    [Header("---Default selected buttons---")]
    [SerializeField] private GameObject initialDefault;
    [SerializeField] private GameObject gameModeDefault;
    [SerializeField] private GameObject settingsDefault;
    [SerializeField] private GameObject creditsDefault;

    [Header("---Tutorial buttons---")]
    [SerializeField] private GameObject rightTutorialButton;
    [SerializeField] private GameObject leftTutorialButton;
    [SerializeField] private GameObject exitTutorialButton;
    [SerializeField] private TutorialButtonNav tuteButNav;

    [Header("---Pages Available---")]
    [SerializeField] private GameObject[] pages;
    //will need to update Check current page switch if more pages are added

    private bool defaultSelected = false;
    public bool usingCursor = true;
    

    // Update is called once per frame
    void Update()
    {
        //reset default selected when a button is pressed
        if (Submit.action.WasPressedThisFrame()) //if enter is pressed
        {
            defaultSelected = false;
            if (!usingCursor) //if not using cursor automatically select default
            {
                defaultSelected = true;
                foreach (GameObject page in pages) //for some reason cannot simplify this function to have loop in check current page otherwise it breaks
                {
                    StartCoroutine(setDefaultBtn(page));
                }
            }
        }
        //if cursor is moved switch to cursor as selection
        if (cursorMovementDelta.action.ReadValue<Vector2>().magnitude > 0) 
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultSelected = false;
            usingCursor = true;

        }
        //select default if switching back to keyboard navigation
        else if (Navigation.action.WasPressedThisFrame())
        {
            usingCursor = false;
            //move mouse to not interfere with keyboard navigation
            Mouse.current.WarpCursorPosition(Camera.main.ViewportToScreenPoint(new Vector2(0.85f, 0.5f)));
            if (!defaultSelected)
            {
                defaultSelected = true;
                foreach (GameObject page in pages) //for some reason cannot simplify this function to have loop in check current page otherwise it breaks
                {
                    StartCoroutine(setDefaultBtn(page));
                }
            }
        } 
    }
    private IEnumerator setDefaultBtn(GameObject obj)
    {
        if (obj.activeSelf) //check if object is active
        {
            Debug.Log("Set a default button for current page");
            switch (obj.name) //set button based on active page
            {
                case "InitialButtons":
                    yield return new WaitForEndOfFrame(); //wait until end of frame to determine default button
                    EventSystem.current.SetSelectedGameObject(initialDefault);
                    break;

                case "Settings":
                    yield return new WaitForEndOfFrame(); //wait until end of frame to determine default button
                    EventSystem.current.SetSelectedGameObject(settingsDefault);
                    break;

                case "GameModeButtons":
                    yield return new WaitForEndOfFrame(); //wait until end of frame to determine default button
                    EventSystem.current.SetSelectedGameObject(gameModeDefault);
                    break;
                case "Tutorial":
                    yield return new WaitForEndOfFrame(); //wait until end of frame to determine default button
                    //if right tutorial is not active set it to exit page otherwise default to right tutorial button (last page)
                    if (rightTutorialButton.activeSelf)
                    {
                        if (tuteButNav.lastButtonWasRight) //if right tutorial button is active and last button pressed was right then default to right button
                        {
                            rightTutorialButton.GetComponent<Outline>().enabled = true;
                            Debug.Log("ay");
                            EventSystem.current.SetSelectedGameObject(rightTutorialButton);
                            break;
                        }
                        //if using left button but ended on first page then set selected as right
                        if (!tuteButNav.lastButtonWasRight && leftTutorialButton.activeSelf)
                        {
                            leftTutorialButton.GetComponent<Outline>().enabled = true;
                            EventSystem.current.SetSelectedGameObject(leftTutorialButton);
                        }
                        else
                        {
                            Debug.Log("ya");
                            rightTutorialButton.GetComponent<Outline>().enabled = true;
                            EventSystem.current.SetSelectedGameObject(rightTutorialButton);
                        }
                    }
                    else //if at last page default to exit
                    {
                        EventSystem.current.SetSelectedGameObject(exitTutorialButton);
                    }
                    break;
                case "Credits":
                    yield return new WaitForEndOfFrame(); //wait until end of frame to determine default button
                    EventSystem.current.SetSelectedGameObject(creditsDefault);
                    break;

                default:
                    // Handle other cases in case page doesn't exist
                    Debug.Log("Page does not exist or have a default set yet");
                    break;
            }
        } 
    }
}
