using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private Button _gameStartButton;

    private void Start()
    {
        _gameStartButton.onClick.AddListener(GameStart);
    }

    private void GameStart()
    {
        SceneManager.LoadScene(1);
    }
}
