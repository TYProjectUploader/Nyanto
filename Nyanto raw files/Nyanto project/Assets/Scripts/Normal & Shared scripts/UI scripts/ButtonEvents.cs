using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private Outline outline; // Reference to the Outline component
    //WHY DID THIS HAVE TO BE A SEPERATE SCRIPT
    //WHY DOES UNITY'S UI SYSTEM NOT DIRRECTLY SUPPORT OUTLINING BUTTONS ON HOVER?!
    //THE ONLY OPTIONS ARE TRANSITIONS LIKE COLOUR TINT, SPRITE SWAP OR ANIMATION

    void Start()
    {
        // Get the Outline component attached to the object
        outline = GetComponent<Outline>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Activate the outline when the pointer enters the button
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
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
        // Deactivate the outline when the button is deselected
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

