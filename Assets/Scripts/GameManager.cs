using System;
using System.Collections;
using Interface;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour {
    private int score = 0;
    [Inject]private ICharacterController _characterController;
    [Inject]private CameraController _cameraController;
    [Inject] private UIManager _uiManager;
    [Inject] private IPlatformController _platformController;

    public void EndGame(bool success, float delay)
    {
        StartCoroutine(End(success, delay));
    }

    IEnumerator End(bool success, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (success) {
            _characterController.Win();
            _cameraController.StartWinCameraMovement();
            _uiManager.Win();
        } else {
            _characterController.Lose();
            _platformController.Init();
            _characterController.Reset();
        }
    }
    public void Next()
    {
        _platformController.Init();
        _characterController.Reset();
        _cameraController.ResetCameraMovement();
    }

    public void IncreaseScore(int increment) {
        score += increment;
        _uiManager.UpdateScoreDisplay(score);
    }
}