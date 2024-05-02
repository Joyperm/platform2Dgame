using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    //make restartGAme accessible to all classes (globally)
    public static LevelManager instance;

    //variable to collect score
    public int score;

    public Text scoreUI;

    //create variable to keep HP
    public int health = 3;
    public Text healthUI;

    //variable to check if game is over
    public bool isGameOver;

    //show game UI
    public GameObject gameOverUI;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    private void Start()
    {
        //show score at the begging of the game
        scoreUI.text = "Score = " + score.ToString();

        healthUI.text = "HP x "+health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore() 
    {
        //update score
        score += 10;

        //show score
        scoreUI.text = "Score = " + score.ToString();
    }

    public void SubtractHP() 
    {
        //decrease score when hit enemy
        health -= 1;

        //show score remain
        healthUI.text = "HP x " + health.ToString();


        //if score hit 0, then restrat game.
        if (health <= 0 && !isGameOver) 
        {
            isGameOver = true;
            gameOverUI.SetActive(true);
        }
    }
}
