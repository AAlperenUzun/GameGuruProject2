using System;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    private Transform _characterTransform;
    public float rotationSpeed = 20f;
    private bool _shouldRotate = false;
    private Quaternion firstRotation;
    [Inject] private ICharacterController _characterController;
    
    public void StartWinCameraMovement()
    {
        _characterTransform = _characterController.GetTransform();
        _shouldRotate = true;
    }

    private void Start()
    {
        firstRotation = transform.rotation;
    }

    public void ResetCameraMovement()
    {
        _shouldRotate = false;
        transform.rotation = firstRotation;
    }

    private void Update()
    {
        if (_shouldRotate)
        {
            transform.RotateAround(_characterTransform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}