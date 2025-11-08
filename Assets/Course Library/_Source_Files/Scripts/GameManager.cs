using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public float spawnRate = 1f;
    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;

    public int score = 0;
    public int nLives = 3;

    public static GameManager instance;

    public List<GameObject> lifeImages;

    public bool gameIsActive = true;

    public GameObject gameOverScreen;

    public AudioSource gameMusic;

    public PausePanel pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        

        instance = this;
        gameOverScreen.SetActive(false);

        if (gameMusic != null)
        {
            gameMusic.volume = GameSettings.MusicVolume;
        }
        if (SaveSystem.IsLoadingGame)
            {
            
            GameState loadedState = SaveSystem.LoadStateFromSave();
            if (loadedState != null)
            {
                score = loadedState.score;
                nLives = loadedState.lives;
                spawnRate = loadedState.difficulty;
            }
        }

        StartCoroutine(SpawnTargets());
        UpdateScore();
        UpdateLives();
        
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        SaveSystem.IsLoadingGame = false;
        SceneNavigator.StartGame();

    }

    public void GameOver()
    {
        gameIsActive = false;

        gameOverScreen.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (gameIsActive && pausePanel != null)
            {
                
                if (pausePanel.gameObject.activeSelf)
                {
                    pausePanel.ClosePanel();
                    
                }
                else
                {
                    pausePanel.OpenPanel();
                   
                }
            }
        }
        
    }

    public void SetPause(bool val = true)
    {
        Time.timeScale = val ? 1f : 0f;
    }

    public void UpdateScore(int scoreToAdd = 0)
    {
        score += scoreToAdd;

        scoreText.text = $"Score: {score}";
    }

    public void UpdateLives(int livesToAdd = 0)
    {
        nLives += livesToAdd;

        for(int i = 0; i < lifeImages.Count; i++)
        {
            lifeImages[i].SetActive(i < nLives);
        }

        if (nLives <= 0) GameOver();
    }

    private IEnumerator SpawnTargets()
    {
        while (gameIsActive)
        {
            yield return new WaitForSeconds(1f / spawnRate);
            var index = Random.Range(0, targets.Count);

            Instantiate(targets[index]);
        }
    }
}
