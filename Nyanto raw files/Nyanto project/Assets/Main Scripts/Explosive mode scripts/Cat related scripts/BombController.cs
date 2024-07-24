using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private float REPULSION_FORCE = 30000f; 
    private bool hasCollided = false;
    void OnCollisionEnter2D(Collision2D collision)
    {
        //prevent colliding with two bombs at same time
        if (collision.gameObject.CompareTag("Bomb") && !hasCollided)
        {
            if (gameObject.GetInstanceID()> collision.gameObject.GetInstanceID())
            {
                AudioManager.instance.PlaySFX(AudioManager.instance.SFXMegumin);
                hasCollided = true; 
                Destroy(gameObject);
                Destroy(collision.gameObject);

                //explosion fx
                Vector3 collisionPosition = (gameObject.transform.position + collision.transform.position)/2;
                GameObject explosionInstance = Instantiate(explosionPrefab, collisionPosition, Quaternion.identity);
                explosionInstance.transform.localScale = new Vector3(4, 4, 1f);
                CameraShake.instance.bombCombineShake();
                // Automatically destroy the explosion after the specified duration
                Destroy(explosionInstance, 1f);

                //Exploed everything in radius
                Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionPosition, 5);
                foreach (Collider2D collider in colliders)
                {
                    Debug.Log("KABOOM");
                    //Debug.Log("pushing away others in range of spawn");
                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        Vector2 repulsionDirection = (rb.position - (Vector2)collisionPosition).normalized;
                        rb.AddForce(repulsionDirection * REPULSION_FORCE, ForceMode2D.Impulse);
                    }
                }
            }
        }
        
    }
}
