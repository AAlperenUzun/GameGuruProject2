using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] private TMP_Text scoreText;

    public void UpdateScoreDisplay(int score) {
        scoreText.text = "Score: " + score;
    }
}