using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyLevel : MonoBehaviour
{
    private Button button;
    private SpawnManager spawnManager;

    public int difficulty;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        //function of onclick in button...
        button.onClick.AddListener(setDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDifficulty()
    {
        Debug.Log(gameObject.name + " was clicked.");

        // starting spawning wave when button is pressed
        deactivateStartScreen();
        spawnManager.startUI(difficulty);
    }

    // deactivate start screen
    void deactivateStartScreen()
    {
        spawnManager.titleScreen.gameObject.SetActive(false);
    }
}
