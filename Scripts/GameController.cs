using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnvalues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    public Text PlugText;
    public Text finchat;

    public bool gameOver;
    private bool restart;
    private int score;
    private int Fs;

    public AudioClip music_background;
    public AudioClip music_win;
    public AudioClip music_lose;
    public AudioSource musicSource;

    private void Start()
    {
        gameOver = false;
        restart = false;
        RestartText.text = "";
        GameOverText.text = "";
        PlugText.text = "";
        finchat.text = "Hey, just so you know, You shouldn't press F.";
        Fs = 0;
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            InteractiveF();
        }
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Space Shooter");
            }
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }
    IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                if (gameOver)
                {
                    break;
                }
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnvalues.x, spawnvalues.x), spawnvalues.y, spawnvalues.z);
                Instantiate(hazard, spawnPosition, transform.rotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                RestartText.text = "Press 'Enter' to Restart";
                restart = true;
                break;
            }
        }
        }
    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;
        if (score >= 100)
        {
            GameOverText.text = "You win!";
            PlugText.text = "Game Created By Samwise Majchrzak";
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(pickups[i]);
            }
            GameObject Background = GameObject.Find("Background");
            BGScroller BGScroller = Background.GetComponent<BGScroller>();
            BGScroller.scrollSpeed = -50;
            musicSource.clip = music_win;
            musicSource.Play();
            gameOver = true;
            restart = true;
        }
    }

    void InteractiveF()
    {
        if (Fs == 0)
        {
            finchat.text = "I told you not to press F. Do you want to break the game?";
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
        if (Fs == 1)
        {
            finchat.text = "I know you feel powerful making weird stuff happen, but I must insist you stop pressing F.";
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
        if (Fs == 2)
        {
            finchat.text = "This is your last chance. Don't Press F.";
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                Destroy(enemies[i]);
            }
        }
        if (Fs == 3)
        {
            Application.Quit();
        }
        Fs = Fs + 1;
    }

    public void GameOver()
    {
        GameOverText.text = "Game Over!";
        PlugText.text = "Game Created By Samwise Majchrzak";
        musicSource.clip = music_lose;
        musicSource.Play();
        gameOver = true;
    }
}
