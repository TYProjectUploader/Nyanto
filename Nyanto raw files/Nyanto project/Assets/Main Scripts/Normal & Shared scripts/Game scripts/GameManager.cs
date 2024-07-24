using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("---General UI---")]
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private GameObject menuPrompts;
    [SerializeField] private GameObject inGamePrompts;
    [Header("---GameOver UI---")]
    [SerializeField] private Image gameOverFadePanel;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject highScoreAlert;
    [SerializeField] private Image gameScreenShot;
    public bool gameOver = false;
    private float FADE_DURATION = 1.5f;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        //Ensure timescale is reset if the scene is reset from the pause menu or end game screen
        Time.timeScale = 1;
        //get Previous highscore
        highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
    public void GameOver()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.SFXGameOver);
        gameOver = true;
        DisableCatPhysics();
        //hide the next cat and aimline for screenshot purposes
        CatGenerator.instance.currentCat.SetActive(false);
        PlayerMovement.instance.aimLine.SetActive(false);
        StartCoroutine(FadeOutGame());
    }
    void DisableCatPhysics()
    {
        // Find all cats by finding all Rigidbody components in the scene
        GameObject[] allCatRigidbodies = GameObject.FindGameObjectsWithTag("Cat");
        Debug.Log("Freezing all cats state");
        // Iterate through each Rigidbody and disable physics simulation to ensure screenshot is what is expected instead of 
        foreach (GameObject rb in allCatRigidbodies)
        {
            Rigidbody2D catRigidBody = rb.GetComponent<Rigidbody2D>();
            catRigidBody.simulated = false;
        }
    }

    private IEnumerator FadeOutGame()
    {
        //wait for fruit flashing to end
        Debug.Log("fading in");
        yield return new WaitForSeconds(GameOverDetector.gameOverDelay);
        StartCoroutine(CaptureScreenshot());

        //wait for screenshot to finish before continuing
        yield return null;
        ActivateGameOverScreen();
        menuPrompts.SetActive(true);
        inGamePrompts.SetActive(false);
        Color panelColor = gameOverFadePanel.color;
        
        //initially set color's alpha to 0
        panelColor.a = 0f;
        gameOverFadePanel.color = panelColor;
        float elapsedTime = 0f;
        while (elapsedTime < FADE_DURATION)
        {
            // Increase the alpha value over time
            panelColor.a = Mathf.Lerp(0f, 0.7f, elapsedTime / FADE_DURATION);
            gameOverFadePanel.color = panelColor;
            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }
        Time.timeScale = 0; //pause the game so nothing unexpected can happen
    }

    private void ActivateGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        highScoreAlert.SetActive(false);
        //set new highscore if applicable
        if (CombinationManager.instance.Score > PlayerPrefs.GetInt("HighScore", 0))
        {
            highScoreAlert.SetActive(true);
            PlayerPrefs.SetInt("HighScore", CombinationManager.instance.Score);
        }
    }
    
    private IEnumerator CaptureScreenshot()
    {
        //create path for screenshot to be saved
        string ssPath;
        //check if done in unity editor or is final build to save the screenshot in different places
        #if UNITY_EDITOR
        ssPath = Path.Combine(Application.dataPath, "Resources/Screenshot/gameOverScreenShot.png");
        #else
        ssPath = Path.Combine(Application.persistentDataPath, "gameOverScreenShot.png");
        #endif
        //capture screenshot of game to display on game over screen
        ScreenCapture.CaptureScreenshot(ssPath);
        //wait for next frame before continuing to ensure screenshot is written before continuing
        yield return new WaitForSeconds(0.5f);

        //create a texture using the screenshot
        Texture2D texture = new Texture2D(Screen.width, Screen.height);
        byte[] imageData = File.ReadAllBytes(ssPath);
        texture.LoadImage(imageData);

        // Create a Sprite from the Texture2D
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        //Finally load the sprite
        gameScreenShot.sprite = sprite;
    }
}
