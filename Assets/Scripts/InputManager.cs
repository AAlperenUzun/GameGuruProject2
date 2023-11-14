using Interface;
using UnityEngine;
using Lean.Touch;
using Zenject;

public class InputManager : MonoBehaviour {
    public delegate void InputAction(Vector2 position);
    public event InputAction OnTouchStarted;
    public event InputAction OnTouchEnded;
    public event InputAction OnTouchMoved;
    [Inject]private IPlatformController _platformController;
    private void OnEnable() {
        LeanTouch.OnFingerDown += HandleFingerDown;
        LeanTouch.OnFingerUp += HandleFingerUp;
        LeanTouch.OnFingerUpdate += HandleFingerUpdate;
    }

    private void OnDisable() {
        LeanTouch.OnFingerDown -= HandleFingerDown;
        LeanTouch.OnFingerUp -= HandleFingerUp;
        LeanTouch.OnFingerUpdate -= HandleFingerUpdate;
    }

    private void HandleFingerDown(LeanFinger finger) {
        OnTouchStarted?.Invoke(finger.ScreenPosition);
        _platformController.AdjustPlatformSizeAndPosition();
    }

    private void HandleFingerUp(LeanFinger finger) {
        OnTouchEnded?.Invoke(finger.ScreenPosition);
    }

    private void HandleFingerUpdate(LeanFinger finger) {
        OnTouchMoved?.Invoke(finger.ScreenPosition);
    }
}