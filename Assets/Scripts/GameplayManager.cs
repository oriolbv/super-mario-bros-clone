using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : ExtendedBehaviour
{

    [Header("UI Components")]
    public GameObject RemainingTimeText;


    public AudioClip gameSongAudioClip;
    public AudioClip marioDeadAudioClip;

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
        // TODO
        GameScore.Instance.RemainingTime -= Time.deltaTime;
        // RemainingTimeText.GetComponentInChildren<Text>().text = ((int)GameScore.Instance.RemainingTime).ToString();
        if (GameScore.Instance.RemainingTime < 0 || !GameScore.Instance.IsPlaying)
        {
            GameOver();
        }
        else if (GameScore.Instance.IsWinner) 
        {
            WinGame();
        }
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
        Wait(3f, () => {
            SceneManager.LoadScene("MenuScene");
        });
    }
}
