using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : Singleton<GameplayManager>
{
    [Header("Game Score")]
    private GameScore _score;

    [Header("UI Components")]
    public GameObject RemainingTimeText;


    public AudioClip gameSongAudioClip;
    public AudioClip marioDeadAudioClip;

    private AudioSource mainGameAudioSource;

    void Start()
    {
        _score = new GameScore(0, 0, 400);

        mainGameAudioSource = this.GetComponentInChildren<AudioSource>();
        mainGameAudioSource.clip = gameSongAudioClip;
        mainGameAudioSource.Play();
    }

    void Update()
    {
        // TODO
        //_score.RemainingTime -= Time.deltaTime;
        //RemainingTimeText.GetComponentInChildren<Text>().text = ((int)_score.RemainingTime).ToString();
        //if (_score.RemainingTime < 0)
        //{
        //    GameOver();
        //}
    }

    public void GameOver() 
    {
        Debug.Log("This is the end");
        // Change clip from Audio Source
        mainGameAudioSource.clip = marioDeadAudioClip;
        mainGameAudioSource.Play();
        Wait(3f, () => {
            SceneManager.LoadScene("LevelMenuScene");
        });
    }

    // Properties
    public GameScore Score
    {
        get 
        { 
            return _score; 
        }
        set 
        { 
            _score = value; 
        }
    }
}
