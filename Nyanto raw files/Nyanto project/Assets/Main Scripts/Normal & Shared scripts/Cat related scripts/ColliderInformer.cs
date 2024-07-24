using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInformer : MonoBehaviour
{
    public bool hasCollided = false;
    public bool triggeredFromCatCombine { get; set; } = false;//check if collision triggered by cats combining (logic in catcombiner)
    //public getter but private setter also defaults to false

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided && !triggeredFromCatCombine) 
        //condition check necessary or else causes bug where 2 cats are spawned if current cat collides with 2 objects in the same frame or from combine collision
        {
            hasCollided = true;
            Debug.Log("readying for next spawn");
            PlayerMovement.instance.canDrop = true; //enable spawning again
            CatGenerator.instance.SpawnCat(); //spawn next cat when cat has collided
            Destroy(this); //destroy this script as no longer needed and to improve performance
        }
        StartCoroutine(DestroyAfterDelay(3f)); //destroy the script after 5s to account for scripts being created when cats combine
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this);
    }
}
