using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour {
    [SerializeField] private TMP_Text scoreText;
    [Inject] private GameManager _gameManager;
    [SerializeField] private GameObject winScreen;
    public void UpdateScoreDisplay(int score) {
        scoreText.text = "Score: " + score;
    }

    public void Win()
    {
        winScreen.SetActive(true);
    }
    public void Next()
    {
        winScreen.SetActive(false);
        _gameManager.Next();
    }
}