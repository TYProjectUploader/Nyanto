using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosiveCatCombiner : MonoBehaviour //handles sending calls to combination manager
{
    //cat info for combination manager
    private CatInfo thisCatInfo;
    private int thisCatIndex {get; set;}
    void Awake()
    {
        //get stats of cat the script is attached to
        thisCatInfo = GetComponent<CatInfo>();
        thisCatIndex = thisCatInfo.catIndex;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        //check if collided with a cat and if not already handling a collision
        if (collision.gameObject.CompareTag("Cat") && !ExplosiveCombinationManager.instance.handlingCombine)
        {
            int otherCatIndex = collision.gameObject.GetComponent<ExplosiveCatCombiner>().thisCatIndex;

            if (otherCatIndex == thisCatIndex) //only call handle collision if both are the same to reduce load
            {
                //use a singleton to handle the collision incase of multi-collisions, see Combinationmanager script for logic
                ExplosiveCombinationManager.instance.HandleCollision(gameObject);
            }
        }
    }
    //both on collisionenter and collisionstay are used to allow combination to occur in case a collision is already being handled
    void OnCollisionStay2D(Collision2D collision)
    {
        //check if collided with a cat and if not already handling a collision
        if (collision.gameObject.CompareTag("Cat") && !ExplosiveCombinationManager.instance.handlingCombine)
        {
            int otherCatIndex = collision.gameObject.GetComponent<ExplosiveCatCombiner>().thisCatIndex;

            if (otherCatIndex == thisCatIndex) //only call handle collision if both are the same to reduce load
            {
                //use a singleton to handle the collision incase of multi-collisions, see Combinationmanager script for logic
                ExplosiveCombinationManager.instance.HandleCollision(gameObject);
            }
        }
    }

}
