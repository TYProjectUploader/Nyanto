using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private float shakeDuration = 0.5f; // Duration of the shake effect
    private float shakeMagnitude; // Intensity of the shake effect

    private Vector3 originalPosition; // Original position of the camera
    private float shakeTimer = 0f; // Timer to control the duration of the shake effect

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //original position of camera
        originalPosition = transform.localPosition;
    }
    void Update()
    {
        if (shakeTimer > 0)
        {
            Vector3 shakeAmount = new Vector3(Random.Range(-shakeMagnitude, shakeMagnitude),
                                              Random.Range(-shakeMagnitude, shakeMagnitude),
                                              0);
            // Shake the camera
            transform.localPosition = originalPosition + shakeAmount;

            // Decrease the shake timer
            shakeTimer -= Time.deltaTime;
        }
        else
        {
            // Reset the camera position to its original position
            shakeTimer = 0f;
            transform.localPosition = originalPosition;
        }
    }
    public void bombCombineShake()
    {
        shakeTimer = 2;
        shakeMagnitude = 10;
    }

    public void Shake(int catIndex)
    {
        // Start the shake effect
        shakeTimer = shakeDuration;
        shakeMagnitude = catIndex*0.15f;
    }
}
