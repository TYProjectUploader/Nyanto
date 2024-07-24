using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Runtime.ExceptionServices;

public class UINavigationInGame : MonoBehaviour
//UI navigation with anything other than mouse within a game mode
{
    [Header("---Input actions---")]
    [SerializeField] private InputActionReference cursorMovementDelta;
    [SerializeField] private InputActionReference pauseMenuOpen;
    [SerializeField] private InputActionReference Navigation;
 
    [Header("---Default selected buttons---")]
    [SerializeField] private GameObject gameOverDefault;
    [SerializeField] private GameObject pauseDefault;

    private bool defaultSelected = false;
    // Update is called once per frame
    void Update()
    {
        if (pauseMenuOpen.action.WasPressedThisFrame()) 
        {
            PauseRelatedButtons.instance.PauseScreen();
        }
        //if not paused or game over the following controls are ignored
        if (!PauseRelatedButtons.instance.Paused && !GameManager.instance.gameOver)
        {
            defaultSelected = false; //reset default selected
            return;
        }

        //if cursor is moved switch to cursor as selection
        if (cursorMovementDelta.action.ReadValue<Vector2>().magnitude > 0) 
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultSelected = false;
        }
        //select default if switching back to keyboard navigation
        else if (Navigation.action.WasPressedThisFrame() && !defaultSelected)
        {
            defaultSelected = true;
            if (PauseRelatedButtons.instance.Paused)
            {
                EventSystem.current.SetSelectedGameObject(pauseDefault);
                //move cursor out of way of buttons if using keyboard input tested this and doesn't affect user experience at all as navigation would be done with one or other
                Mouse.current.WarpCursorPosition(Camera.main.ViewportToScreenPoint(new Vector2(0.85f, 0.5f)));
                return;
            }
            if (GameManager.instance.gameOver)
            {
                EventSystem.current.SetSelectedGameObject(gameOverDefault);
                Mouse.current.WarpCursorPosition(Camera.main.ViewportToScreenPoint(new Vector2(0.85f, 0.5f)));
                return;
            } 
        } 
    }
}
