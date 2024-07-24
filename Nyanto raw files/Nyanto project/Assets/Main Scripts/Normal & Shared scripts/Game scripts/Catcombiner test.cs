using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
    [SerializeField] public GameObject Catd1;
    [SerializeField] public GameObject Catd2;
    // to test CombineCats this dummy module was created so that cat prefabs can be directly injected as parameters
    //create an empty game object and attach this script to use to test
    private GameObject cat1;
    private GameObject cat2;
    void Start()
    {
        cat1 = Instantiate(Catd1);
        cat2 = Instantiate(Catd2);
        StartCoroutine(ForceCombine());
    }
    private IEnumerator ForceCombine()
    {
        yield return new WaitForSeconds(5f); // wait for 5s before forcing the combine
        StartCoroutine(CombinationManager.instance.CombineCats(cat1, cat2));
    }
}

