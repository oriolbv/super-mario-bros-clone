﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : Singleton<GameScore>
{
    private int _lives;
    private int _score;
    private int _coins;
    private float _remainingTime;

    private bool _isPlaying;

    public GameScore() 
    {
    }

    public void initGameScore() 
    {
        _score = 0;
        _coins = 0;
        _remainingTime = 300;
        _isPlaying = true;
    }

    public void resetLives() 
    {
        _lives = 3;
    }

    // Properties
    public int Lives
    {
        get 
        { 
            return _lives; 
        }
        set 
        { 
            _lives = value; 
        }
    }

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

    public bool IsPlaying
    {
        get 
        { 
            return _isPlaying; 
        }
        set 
        { 
            _isPlaying = value; 
        }
    }
}
