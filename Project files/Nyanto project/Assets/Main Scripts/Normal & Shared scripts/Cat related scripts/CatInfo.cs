using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Data store for the information when dealing with a given cat. Attached to all cat prefabs
public class CatInfo : MonoBehaviour
{
    public int catIndex; //index of the cat in its evolution
    public int combineValue; //value for combining this cat
    public float catMass;
    private Rigidbody2D catBody; 
    void Awake()
    {
        //Just for convenience purpose makes editing cat info all at once easier for future updates
        catBody = GetComponent<Rigidbody2D>();
        catBody.mass = catMass;
    }

}

