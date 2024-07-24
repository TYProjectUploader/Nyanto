using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ExplosiveGameOverDetector : MonoBehaviour
{
    //variables for flash of gameobject when gameover
    private static float FLASH_DELAY = 0.5f;
    private static int NUMBER_OF_FLASHES = 3;
    public static float explosivegameOverDelay;
    //made static so can be accessed elsewhere without instance reference
    //also because these should not be changed even if 2nd instance of this is to be created

    void Start()
    {
        explosivegameOverDelay = FLASH_DELAY*NUMBER_OF_FLASHES*2;
        //set variable to be referenced in game manager
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        //if a cat enters stratosphere game is over
        if (collider.gameObject.GetComponent<Rigidbody2D>().simulated && !ExplosiveGameManager.instance.gameOver && collider.gameObject.CompareTag("Cat"))
        {
            ExplosiveGameManager.instance.GameOver();
            StartCoroutine(flashGameOverCat(collider.gameObject));
            Debug.Log("gameover");
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
