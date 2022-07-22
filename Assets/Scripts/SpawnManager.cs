using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Prefabs;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameoverText;
    public Button resetButton;
    public GameObject titleScreen;
    public Slider volumeSlider;
    public Camera mainCamera;
    public Button pauseScreen;

    private AudioSource backgroundAudio;

    public int score = 0;
    public int lives = 5;
    float spawnTime = 2f;
    public bool isGameOver = false;
    public bool isPaused = false;



    // Start is called before the first frame update
    void Start()
    {
        backgroundAudio = mainCamera.GetComponent<AudioSource>();
        volumeSlider.value = 0.5f;
        backgroundAudio.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //update score every frame
        updateScore();

        //update lives
        updateLives();
        
        if (isGameOver)
            displayGameover();

        if (Input.GetKeyDown(KeyCode.P) && !isPaused)
            PauseGame();
        else if (Input.GetKeyDown(KeyCode.P) && isPaused)
            ResumeGame();
    }


    IEnumerator spawnWave()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnTime);

            int randIndex = Random.Range(0, Prefabs.Length);
            GameObject x = Prefabs[randIndex];
            Instantiate(x, x.transform.position, x.transform.rotation);
        }
    }


    void updateScore()
    {
        scoreText.text = "Score : " + score.ToString();
    }


    public void displayGameover()
    {
        gameoverText.gameObject.SetActive(true);
        resetButton.gameObject.SetActive(true);

    }


    public void startNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startUI(int difficulty)
    {
        spawnTime /= difficulty;
        StartCoroutine(spawnWave());
    }


    public void updateLives()
    {
        if(lives <= 0)
        {
            lives = 0;
            isGameOver = true;
        }
        livesText.text = "Lives : " + lives.ToString();
    }


    public void VolumeSider()
    {
        backgroundAudio.volume = volumeSlider.value;
    }

    void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        pauseScreen.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
}
