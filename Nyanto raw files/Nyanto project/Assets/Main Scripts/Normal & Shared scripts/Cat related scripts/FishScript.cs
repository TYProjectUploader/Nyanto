using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    private bool hasCollided = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        //delete fish and cat if they collide but only one if collides with two cats at same time
        if (collision.gameObject.CompareTag("Cat") && !hasCollided)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.SFXFishNya);
            hasCollided = true; 
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        
    }
}
