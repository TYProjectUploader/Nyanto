using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorVisibility : MonoBehaviour //script to turn off visibility of mouse, not visible in editor
{
    [SerializeField] private InputActionReference cursorMovementDelta;

    private float inactivityCounter = 0f;
    private float inactivityTime = 3f;
    public bool cursorIsActive = true;
    void Update()
    {
        Vector2 cursorChange = cursorMovementDelta.action.ReadValue<Vector2>();
        //Debug.Log(inactivityTimer);
        if (cursorChange.magnitude > 0)
        {
            Cursor.visible = true;
            inactivityCounter -= inactivityTime;
            cursorIsActive = true;
        }
        else if (inactivityCounter >= inactivityTime)
        {
            Cursor.visible = false;
            cursorIsActive = false;
            return; //stop adding time to inactivity timer
        }
        inactivityCounter += Time.unscaledDeltaTime;
    }
}
