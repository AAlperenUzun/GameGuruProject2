using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour {
    private int score = 0;
    [Inject]private ICharacterController _characterController;
    [Inject]private CameraController _cameraController;
    [Inject] private UIManager _uiManager;

    public void EndGame(bool success, float delay)
    {
        StartCoroutine(End(success, delay));
    }

    IEnumerator End(bool success, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (success) {
            // Handle game win logic
            _characterController.Win();
            _cameraController.StartWinCameraMovement();
        } else {
            // Handle game over logic
        }
    }

    public void IncreaseScore(int increment) {
        score += increment;
        _uiManager.UpdateScoreDisplay(score);
    }
}