using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatGenerator : MonoBehaviour
{
    public static CatGenerator instance;

    [SerializeField] private GameObject spawnerPosition; 
    //get position of player using an empty object attached to player to spawn cat
    [SerializeField] private Sprite[] catImages; //storage of cat sprites for the next cat bubble
    [SerializeField] private Image nextCatImage; //reference to next cat bubble image to be rendered
    [SerializeField] public GameObject[] Cats; //storage of the cat prefabs

    [SerializeField] private GameObject fish;
    [SerializeField] private Sprite fishSprite;
    private int fishChance = 50; // chance is calculated as 1 out of fishChance

    private int highestCatSpawnIndex = 4; //change to allow spawning of higher level cats
    public GameObject currentCat; //variable to store current cat prefab
    private GameObject nextCat; //variable to store next cat prefab

    void Awake()
    {
        //set instance to this script
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //generate and spawn the first cat and next cat
        generateRandomcat(); 
        Debug.Log("generate first cat");
        spawnCat();
    }

    private void generateRandomcat()
    {
        int isFish = Random.Range(0, fishChance);
        if (isFish == 1)
        {
            nextCat = fish;
            nextCatImage.sprite = fishSprite;
        }
        else
        {
            int nextCatIndex = Random.Range(0, highestCatSpawnIndex);
            nextCat = Cats[nextCatIndex];
            nextCatImage.sprite = catImages[nextCatIndex];
        }
    }
    public void spawnCat()
    {
        //spawn cat at the location of an empty game object called catspanwer
        currentCat = Instantiate(nextCat, spawnerPosition.transform);
        //get size of cat radius to update movement boundary
        CompositeCollider2D currentCatCollider = currentCat.GetComponent<CompositeCollider2D>();
        float catWidth = (currentCatCollider.bounds.max.x - currentCatCollider.bounds.min.x)/2; 
        PlayerMovement.instance.updateMovementBoundary(catWidth);
        currentCat.SetActive(false); //set false for now and display when dropcooldown over
        //once next cat instantiated generate the next cat
        generateRandomcat();
    }
    public void simulateCurrentCat() //simulate the cat and unparent its transform position from spawner
    {
        Debug.Log("simulating Current Cat");
        currentCat.GetComponent<Rigidbody2D>().simulated = true;
        currentCat.transform.parent = null;
    }
}
