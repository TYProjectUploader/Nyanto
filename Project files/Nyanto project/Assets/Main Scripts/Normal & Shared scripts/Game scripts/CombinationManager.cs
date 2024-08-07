using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CombinationManager : MonoBehaviour
{
    //reference to UI
    [SerializeField] private GameObject combo;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI scoreText;


    public static CombinationManager instance;

    // references to instances of the cats being combined
    private GameObject cat1;
    private GameObject cat2;
    private CatInfo cat1CatInfo;
    private CatInfo cat2CatInfo;
    private int cat1Index;
    private int cat2Index;
    private int cat1Value;

    //variables for handling the combination
    private float COMBINE_DELAY = 0.06f;
    private float REPULSION_FORCE = 2.5f; //force to push other cats in range away when spawning
    [SerializeField] private Material whiteMaterial; //material to swap the cat sprites out for

    //spawn boundary variables for combined cats
    private BoxCollider2D spawnBox;
    private Bounds spawnBounds;
    private float leftBound;
    private float rightBound;
    private float topBound;
    private float bottomBound;
    


    //int storage for the combo no
    private int comboCount;
    //time before comboresets
    private float COMBO_RESET_TIME = 1.5f;
    public int Score {get; set;}
    private int scoreToAdd;
    public bool handlingCombine {get; set;} = false;

    private void Awake()
    {
        //ensure only one instance exists and its this one attached to gamemanager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //set up spawnbox boundaries
        spawnBox = GameObject.Find("Combined Cat spawn boundary").GetComponent<BoxCollider2D>();
        if (spawnBox == null)
        {
            Debug.Log("spawn box wasn't found");
        }
        spawnBounds = spawnBox.bounds;
    }
    //handles combination and combo logic
    public void HandleCollision(GameObject catPrefab)
    {
        if (handlingCombine) //if already handling collision skip
        {
            return;
        }
        Debug.Log("handling collision");
        if (cat1 == null)
        {
            cat1 = catPrefab;
            cat1CatInfo = cat1.GetComponent<CatInfo>();
            cat1Index = cat1CatInfo.catIndex;
            cat1Value = cat1CatInfo.combineValue;
            Debug.Log("cat1 stored");
            return;
        }
        else if (cat2 == null)
        {
            cat2 = catPrefab;
            cat2CatInfo = cat2.GetComponent<CatInfo>();
            cat2Index = cat2CatInfo.catIndex;
            Debug.Log("cat2 stored");
        }
        //double check that stored cats are same before continuing
        if (cat1 != null & cat2 !=null && cat1Index == cat2Index)
        {
            handlingCombine = true;
            StartCoroutine(CombineCats(cat1, cat2));
            comboCount++;

            // Cancel the existing Invoke combo to reset combo timer
            CancelInvoke("ResetCombo");

            // Reset combo count after a certain time
            Invoke("ResetCombo", COMBO_RESET_TIME);
            //add score based on combocount
            scoreToAdd = cat1Value*comboCount;
            Score += scoreToAdd;
            scoreText.text = Score.ToString();
            //Display combo
            if (comboCount > 1)
            {
                combo.SetActive(true);
                comboText.text = comboCount.ToString() + "X";
            }
            
        }
        Debug.Log("clearing stored cats");
        cat1 = null;
        cat2 = null;
    }

    public IEnumerator CombineCats(GameObject cat1, GameObject cat2)
    {
        Debug.Log("ayaya");
        //turn the cats white
        cat1.GetComponent<SpriteRenderer>().material = whiteMaterial;
        cat2.GetComponent<SpriteRenderer>().material = whiteMaterial;
        //delay reduces load on calculations with multiple combinations and feels more smooth
        yield return new WaitForSeconds(COMBINE_DELAY);
        //Destroy collided objects only occurs after rest of frame logic is done
        Destroy(cat1);
        Destroy(cat2);
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXCombine);

        //if watermelon cat do nothing
        if (cat1Index == CatGenerator.instance.Cats.Length -1)
        {
            Debug.Log("Watermelon combine!!!");
        }
        else
        {
            //if not final cat, spawn a cat of next index 
            Vector3 collisionPosition = (cat1.transform.position + cat2.transform.position)/2;
            GameObject combinedCat = Instantiate(CatGenerator.instance.Cats[cat1Index+1]) ;
            combinedCat.GetComponent<Rigidbody2D>().simulated = true;

            //clamp the spawn position to prevent larger fruits from getting stuck in the side of walls when spawning
            //get width of cat to be spawned and clamp spawn position within bounds
            CompositeCollider2D combinedCatCollider = combinedCat.GetComponent<CompositeCollider2D>();
            float catWidth = (combinedCatCollider.bounds.max.x - combinedCatCollider.bounds.min.x)/2; 
            //top bound not considered coz no effect
            bottomBound = spawnBounds.min.y + catWidth;
            leftBound = spawnBounds.min.x + catWidth;
            rightBound = spawnBounds.max.x - catWidth;

            collisionPosition.y = Mathf.Clamp(collisionPosition.y, bottomBound, topBound+100); 
            collisionPosition.x = Mathf.Clamp(collisionPosition.x, leftBound, rightBound);

            //spawn the cat and inform the collider informer that collision occured due to cats combining
            combinedCat.GetComponent<Rigidbody2D>().position = collisionPosition;
            combinedCat.transform.position = collisionPosition;
            ColliderInformer informer = combinedCat.GetComponent<ColliderInformer>();
            informer.triggeredFromCatCombine = true;

            //To reduce overlap when spawning all overlapping colliders have their rigidbody receive a force away from each other
            Collider2D[] colliders = Physics2D.OverlapCircleAll(collisionPosition, catWidth);
            foreach (Collider2D collider in colliders)
            {
                //Debug.Log("pushing away others in range of spawn");
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 repulsionDirection = (rb.position - (Vector2)combinedCat.transform.position).normalized;
                    rb.AddForce(repulsionDirection * REPULSION_FORCE, ForceMode2D.Impulse);
                }
            }
        }
        handlingCombine = false;
        Debug.Log("Cats combined");
    }
    void ResetCombo()
    {
        comboCount = 0;
        combo.SetActive(false);
    }
}

