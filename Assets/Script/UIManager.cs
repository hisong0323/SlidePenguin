using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _resultScoreText;

    [SerializeField]
    private GameObject _highScoreText;

    [SerializeField]
    private GameObject _gameOverView;

    private void Start()
    {
        GameManager.Instance.OnChangeScore += UpdateScoreText;
        GameManager.Instance.OnGameOver += ShowGameOverView;
        GameManager.Instance.OnHighScoreChange += ShowHighScoreText;
    }

    private void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }

    private void ShowGameOverView()
    {
        _gameOverView.SetActive(true);
        _resultScoreText.text = _scoreText.text;
    }

    private void ShowHighScoreText()
    {
        _highScoreText.SetActive(true);
    }
}
