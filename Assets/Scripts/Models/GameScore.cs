using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore
{
    private int _score;
    private int _coins;
    private float _remainingTime;

    public GameScore(int score, int coins, float remainingTime) 
    {
        _score = score;
        _coins = coins;
        _remainingTime = remainingTime;
    }

    // Properties
    public int Score
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

    public int Coins
    {
        get 
        { 
            return _coins; 
        }
        set 
        { 
            _coins = value; 
        }
    }

    public float RemainingTime
    {
        get 
        { 
            return _remainingTime; 
        }
        set 
        { 
            _remainingTime = value; 
        }
    }
}
