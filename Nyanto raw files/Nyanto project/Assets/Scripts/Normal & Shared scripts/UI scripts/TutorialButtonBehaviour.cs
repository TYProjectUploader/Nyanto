using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TutorialButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private Outline outline;
    public bool isHovered = false;
    [SerializeField] private InputActionReference Submit;
    //this script exists because the buttons aren't replaced in the tutorial pages when a new page is pressed.
    void Start()
    {
        // Get the Outline component attached to the object
        outline = GetComponent<Outline>();
    }
    void Update()
    {
        if (isHovered && Submit.action.WasPressedThisFrame() && gameObject.activeSelf)
        {
            //simulate click
            ExecuteEvents.Execute(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
        // Activate the outline when the pointer enters the button
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
        // Deactivate the outline when the pointer exits the button
        outline.enabled = false;
    }
    public void OnSelect(BaseEventData eventData)
    {
        // Activate the outline when the button is selected
        StartCoroutine(EnableOutline());
    }

    public void OnDeselect(BaseEventData eventData)
    {
        outline.enabled = false;
    }

     private IEnumerator EnableOutline()
     {
        yield return new WaitForEndOfFrame(); 
        //force unity to wait to end of frame to preven on pointer exit from being processed first]
        //this is an issue with outline not correctly displaying when switching between the two.
        outline.enabled = true;
     }
}
