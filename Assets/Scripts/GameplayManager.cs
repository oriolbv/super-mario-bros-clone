using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : ExtendedBehaviour
{

    [Header("UI Components")]
    public GameObject RemainingTimeText;
    public GameObject ScoreText;
    public GameObject CoinsText;

    [Header("Sound Effects")]
    public AudioClip gameSongAudioClip;
    public AudioClip marioDeadAudioClip;
    public AudioClip stageClearedAudioClip;
    private AudioSource mainGameAudioSource;

    

    void Start()
    {
        GameScore.Instance.initGameScore();
        
        mainGameAudioSource = this.GetComponentInChildren<AudioSource>();
        mainGameAudioSource.clip = gameSongAudioClip;
        mainGameAudioSource.Play();
    }

    void Update()
    {
        UpdateHUD();
        
        if (GameScore.Instance.RemainingTime < 0 || !GameScore.Instance.IsPlaying)
        {
            GameOver();
        }
        else if (GameScore.Instance.IsWinner) 
        {
            WinGame();
        }
    }

    void UpdateHUD() 
    {
        // Remaining time
        GameScore.Instance.RemainingTime -= Time.deltaTime;
        RemainingTimeText.GetComponent<Text>().text = ((int)GameScore.Instance.RemainingTime).ToString();
        string score = ((int)GameScore.Instance.Score).ToString();
        for (int i = score.Length; i < 6; ++i) 
        {
            score = "0" + score;
        }
        ScoreText.GetComponent<Text>().text = score;
        CoinsText.GetComponent<Text>().text = "x" + ((int)GameScore.Instance.Coins).ToString();
    }

    public void GameOver() 
    {
        Debug.Log("This is the end");
        enabled = false;

        // Change clip from Audio Source
        mainGameAudioSource.clip = marioDeadAudioClip;
        mainGameAudioSource.Play();
        Wait(3f, () => {
            SceneManager.LoadScene("LevelMenuScene");
        });
    }

    public void WinGame() 
    {
        Debug.Log("You win!");
        enabled = false;
        mainGameAudioSource.clip = stageClearedAudioClip;
        mainGameAudioSource.Play();
        Wait(6f, () => {
            SceneManager.LoadScene("MenuScene");
        });
    }
}
