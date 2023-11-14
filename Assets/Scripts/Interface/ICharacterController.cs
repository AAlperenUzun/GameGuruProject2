using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterController {
    void MoveToPosition(Vector3 newPosition);
    public void SetTargetPosition(Vector3 newTarget);
    public void Win();
    public void Lose();
    public void Reset();
    public Transform GetTransform();
}

