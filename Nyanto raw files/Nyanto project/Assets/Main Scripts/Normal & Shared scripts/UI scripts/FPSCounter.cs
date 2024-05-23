using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour //simple script to display the fps
{
    [SerializeField] private TextMeshProUGUI fpsCounter;
    private float timeSinceLastUpdate;
    private float pollingTime = 1f;
    private int frameCount;
    // Update is called once per frame
    void Update()
    {
        frameCount ++;
        timeSinceLastUpdate += Time.unscaledDeltaTime;

        if(timeSinceLastUpdate >= pollingTime)
        {
            int frameRate = Mathf.RoundToInt(frameCount/timeSinceLastUpdate);
            fpsCounter.text = frameRate.ToString() + " FPS";
            timeSinceLastUpdate -= pollingTime;
            frameCount = 0;
        }

    }
}
