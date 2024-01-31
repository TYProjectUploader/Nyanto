using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour //simple script to display the fps
{
    [SerializeField] private TextMeshProUGUI fpsCounter;
    private float time;
    private float pollingTime = 1f;
    private int frameCount;
    // Update is called once per frame
    void Update()
    {
        frameCount ++;
        time += Time.unscaledDeltaTime;


        if(time >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount/time);
            fpsCounter.text = frameRate.ToString() + " FPS";
            time -= pollingTime;
            frameCount = 0;
        }

    }
}
