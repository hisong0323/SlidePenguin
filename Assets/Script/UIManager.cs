using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    #region Field
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI resultScoreText;

    [SerializeField]
    private GameObject highScoreText;

    [SerializeField]
    private GameObject gameOverView;

    [SerializeField]
    private Button restartButton;
    #endregion

    private void Start()
    {
        GameManager.Instance.OnChangeScore += UpdateScoreText;
        GameManager.Instance.OnGameOver += ShowGameOverView;
        GameManager.Instance.OnHighScoreChange += ShowHighScoreText;
        restartButton.onClick.AddListener(GoMainMenu);
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    private void ShowGameOverView()
    {
        gameOverView.SetActive(true);
        resultScoreText.text = scoreText.text;
    }

    private void ShowHighScoreText()
    {
        highScoreText.SetActive(true);
    }

    private void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}