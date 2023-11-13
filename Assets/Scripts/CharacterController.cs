using System;
using UnityEngine;
using Zenject;

public class CharacterController : MonoBehaviour, ICharacterController {
    private Vector3 _targetPosition;
    public float moveSpeed = 2f;
    private AnimationController _animationController;
    private void OnEnable()
    {
        FindObjectOfType<PlatformController>().OnTargetPositionChanged += SetTargetPosition;
    }

    private void OnDisable()
    {
        FindObjectOfType<PlatformController>().OnTargetPositionChanged -= SetTargetPosition;
    }
    
    private void Start()
    {
        _animationController = GetComponent<AnimationController>();
    }

    void Update() 
    {
        MoveToPosition(_targetPosition);
    }
    
    public void MoveToPosition(Vector3 newPosition) {
        _targetPosition = newPosition;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
    }

    public void Win()
    {
        _animationController.PlayAnimation("dance");
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetTargetPosition(Vector3 newTarget)
    {
        _targetPosition = newTarget;
        _targetPosition.y = transform.position.y;
    }
}