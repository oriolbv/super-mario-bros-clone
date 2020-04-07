using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : Singleton<GameplayManager>
{
    [Header("Game Score")]
    private GameScore _score;

    [Header("UI Components")]
    public GameObject RemainingTimeText;


    // Start is called before the first frame update
    void Start()
    {
        _score = new GameScore(0, 0, 400);
    }

    // Update is called once per frame
    void Update()
    {
        _score.RemainingTime -= Time.deltaTime;
        RemainingTimeText.GetComponentInChildren<Text>().text = ((int)_score.RemainingTime).ToString();
        if (_score.RemainingTime < 0)
        {
            GameOver();
        }
    }

    public void GameOver() 
    {
        Debug.Log("This is the end");
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
