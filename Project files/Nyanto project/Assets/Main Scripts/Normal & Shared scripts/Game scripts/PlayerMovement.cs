using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    /*using singleton design so only one instance is used and updated.
     Also used over serializedfield so that prefabs can access it when instantiated
     easier to reference over serialize field at times
    */
    public static PlayerMovement instance;

    //references to other objects used
    [SerializeField] private InputActionReference cursorMovement, cursorMovementDelta, noncursorMovement, Drop;
    [SerializeField] private BoxCollider2D movementBoundary;
    [SerializeField] private Camera Cam; //get camera of the game to convert mouse position
    [SerializeField] private float moveSpeed; //lets movement be easily changed in unity development engine
    [SerializeField] public GameObject aimLine;
    // Boundaries
    private Bounds movementBounds;
    private float leftBound;
    private float rightBound;
    //other variables
    private Vector2 newPosition; 
    public bool canDrop = true;
    private bool coolDownOver = true;
    private float DROP_COOL_DOWN = 0.9f;

    void Awake()
    {
        //set instance to this script
        if (instance == null)
        {
            instance = this;
        }
        //create the initial boundaries for movement using pre-set box collider
        movementBounds = movementBoundary.bounds; 
        leftBound = movementBounds.min.x;
        rightBound = movementBounds.max.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver && !PauseRelatedButtons.instance.Paused)
        {
            UpdateMovement();
            CheckDropAbility();
        }
    }
    void CheckDropAbility() //check if player dropped cat and can drop cat (determined by collderinformer when cat has made first contact)
    {
        if (canDrop && coolDownOver)
        {
            aimLine.SetActive(true); //show aimline again
            CatGenerator.instance.currentCat.SetActive(true); //display the next preloaded cat
            if (Drop.action.WasPressedThisFrame())
            {
                Debug.Log("Spawning Cat... ");
                aimLine.SetActive(false); //turn off aim line while cat is dropping
                canDrop = false;
                coolDownOver = false;
                CatGenerator.instance.SimulateCurrentCat(); //simulate the current cat being held to make it drop
                StartCoroutine(ResetDropCoolDown());
            }
        }
    }

    //seperate DROP_COOL_DOWN so there is a minimum when near the top but otherwise cooldown based on when current dropped cat collides with something
    IEnumerator ResetDropCoolDown()
    {
        yield return new WaitForSeconds(DROP_COOL_DOWN);
        coolDownOver = true;
    }


    void UpdateMovement()
    {
        //movement logic. If mouse is being moved then only mouse input is used otherwise keyboard input is used
        Vector2 cursorChange = cursorMovementDelta.action.ReadValue<Vector2>();
        if (cursorChange.magnitude > 0)
        {
            Vector2 mousePosition = cursorMovement.action.ReadValue<Vector2>();
            //convert position by mouse to it being relative to camera
            Vector3 worldMousePosition = Cam.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
            newPosition = new Vector2(worldMousePosition.x, transform.position.y);
            /*
            Debugging used for finding position
            Debug.Log(worldMousePosition);
            */
        }
        else
        {
            //movement input from non-cursor devices such as keyboards
            Vector3 noncursorMovementInput = noncursorMovement.action.ReadValue<Vector2>() * Time.deltaTime * moveSpeed;
            newPosition = transform.position + noncursorMovementInput;
            //Debugging used for finding position
            //Debug.Log("reading noncursor: " + noncursorMovementInput);
            
        }
        //restrict player movement to within the box
        newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);
        transform.position = newPosition;
    }
    public void UpdateMovementBoundary(float catWidth) //update movement boundary based on size of cat
    {
        leftBound = movementBounds.min.x;
        rightBound = movementBounds.max.x;
        leftBound += catWidth;
        rightBound -= catWidth;
    }
}
