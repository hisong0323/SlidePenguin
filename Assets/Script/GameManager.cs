using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int _score;

    public event Action<int> OnChangeScore;

    public event Action OnGameOver;

    public event Action OnHighScoreChange;

    private int _highScore;

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        StartCoroutine(GameLoop());
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        OnGameOver?.Invoke();

        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
            OnHighScoreChange?.Invoke();
        }
    }

    public void ScoreUp(int score)
    {
        _score += score;
        OnChangeScore?.Invoke(_score);
    }

    private IEnumerator GameLoop()
    {
        var wait = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return wait;
            ScoreUp(1);
            if (Time.timeScale < 2)
            {
                Time.timeScale += 0.001f;
            }
        }
    }
}
