using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GameOverDetector : MonoBehaviour
{
    private float timer;
    private float timeTillGameOver = 0.9f;
    
    //variables for flash of gameobject when gameover
    private static float flashDelay = 0.5f;
    private static int numberOfFlashes = 3;
    public static float gameOverDelay;
    //made static so can be accessed elsewhere without instance reference
    //also because these should not be changed even if 2nd instance of this is to be created

    void Start()
    {
        gameOverDelay = flashDelay*numberOfFlashes*2;
        //set variable to be referenced in game manager
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        //check if cat in the trigger has been dropped and isn't one being held
        //when a cat stays in collider over the timetillgameover the game ends
        if (collider.gameObject.GetComponent<Rigidbody2D>().simulated && !GameManager.instance.gameOver && collider.gameObject.CompareTag("Cat"))
        {
            timer += Time.deltaTime;
            if (timer > timeTillGameOver)
            {
                GameManager.instance.GameOver();
                StartCoroutine(FlashGameOverCat(collider.gameObject));
                Debug.Log("gameover");
            }
        }
        //Debug.Log("entered");
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        timer = 0f;
        //Debug.Log("exited");
    }
    public static IEnumerator FlashGameOverCat(GameObject cat)
    {
        SpriteRenderer sprite = cat.GetComponent<SpriteRenderer>();
        for (int i = 0; i < numberOfFlashes; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(flashDelay);
            sprite.enabled = true;
            yield return new WaitForSeconds(flashDelay);
        }
    }

}
