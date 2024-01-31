using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetUpSettings : MonoBehaviour
{
    void Start()
    {
        //set frame rate to be maxed at 120
        Application.targetFrameRate = -1;
    }
}
