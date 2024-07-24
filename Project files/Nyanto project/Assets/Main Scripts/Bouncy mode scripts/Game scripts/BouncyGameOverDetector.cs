using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class BouncyGameOverDetector : MonoBehaviour
{
    private float timer;
    private float TIME_TILL_GAME_OVER = 0.05f;
    
    //variables for flash of gameobject when gameover
    private static float FLASH_DELAY = 0.5f;
    private static int NUMBER_OF_FLASHES = 3;
    public static float bouncygameOverDelay;
    //made static so can be accessed elsewhere without instance reference
    //also because these should not be changed even if 2nd instance of this is to be created

    void Start()
    {
        bouncygameOverDelay = FLASH_DELAY*NUMBER_OF_FLASHES*2;
        //set variable to be referenced in game manager
    }
    void OnTriggerStay2D(Collider2D collider) //triggerstay is used instead to prevent bug where player drops as something is flying into boundary.
    {
        //check if cat in the trigger has been dropped and isn't one being held
        //when a cat stays in collider over the TIME_TILL_GAME_OVER the game ends
        if (collider.gameObject.GetComponent<Rigidbody2D>().simulated && !BouncyGameManager.instance.gameOver && collider.gameObject.CompareTag("Cat"))
        {
            timer += Time.deltaTime;
            if (timer > TIME_TILL_GAME_OVER)
            {
                BouncyGameManager.instance.GameOver();
                StartCoroutine(flashGameOverCat(collider.gameObject));
                Debug.Log("gameover");
            }
        }
        //Debug.Log("entered");
    }
    public static IEnumerator flashGameOverCat(GameObject cat)
    {
        SpriteRenderer sprite = cat.GetComponent<SpriteRenderer>();
        for (int i = 0; i < NUMBER_OF_FLASHES; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(FLASH_DELAY);
            sprite.enabled = true;
            yield return new WaitForSeconds(FLASH_DELAY);
        }
    }

}
