using System;
using System.Collections.Generic;
using Interface;
using UnityEngine;
using Zenject;

public class PlatformController : MonoBehaviour, IPlatformController
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float moveLimit = 5.0f;
    [SerializeField] private Transform oldPlatform;
    [SerializeField] private float minimumPositionDifference = 0.1f;
    [SerializeField] private int goalPlatformCount;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private List<Material> stackMats;
    
    private Transform _currentPlatform;
    private Vector3 _finishPos;
    private bool _movingRight = true;
    private float _platformScaleZ;
    private int _currentPlatformCount = 0;
    
    [Inject] private SoundManager _soundManager;
    [Inject] private ParticleManager _particleManager;
    [Inject] private GameManager _gameManager;
    [Inject] private PlatformFactory _platformFactory;
    
    public event Action<Vector3> OnTargetPositionChanged;

    void Start()
    {
        _platformScaleZ = oldPlatform.transform.localScale.z;
        SetupInitialPlatform();
        CreateFinish();
    }

    void Update()
    {
        if (_currentPlatform)
            MovePlatform();
    }

    private void MovePlatform()
    {
        Vector3 position = _currentPlatform.transform.position;
        position.x += (_movingRight ? moveSpeed : -moveSpeed) * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, -moveLimit, moveLimit);
        _currentPlatform.transform.position = position;

        if (Mathf.Abs(position.x) >= moveLimit)
            _movingRight = !_movingRight;
    }

    private void SetupInitialPlatform()
    {
        _currentPlatform = oldPlatform;
        OnTargetPositionChanged?.Invoke(_currentPlatform.transform.position);
        CreateNewPlatform();
    }

    public void CreateFinish()
    {
        _finishPos = oldPlatform.transform.position + Vector3.forward * goalPlatformCount * _platformScaleZ;
        Instantiate(finishPrefab, _finishPos, Quaternion.identity, transform);
    }

    public void AdjustPlatformSizeAndPosition()
    {
        float cutSize = Mathf.Abs(_currentPlatform.transform.position.x - oldPlatform.transform.position.x);
        
        if (cutSize <= minimumPositionDifference)
            HandlePerfectCut();
        else if (cutSize >= _currentPlatform.transform.localScale.x)
        {
            _currentPlatform.gameObject.AddComponent<Rigidbody>();
            _gameManager.EndGame(false, 0f);
            return;
        }
        else
            HandleNormalCut(cutSize);

        CreateNewPlatform();
    }

    private void HandlePerfectCut()
    {
        _currentPlatform.position = new Vector3(oldPlatform.transform.position.x, _currentPlatform.transform.position.y, _currentPlatform.position.z);
        _soundManager.PlayPerfectSound();
        _particleManager.PlayPerfectParticle(_currentPlatform.transform);
        UpdateOldPlatform();
    }

    private void HandleNormalCut(float cutSize)
    {
        UpdatePlatformSizeAndPosition(cutSize);
        CreateCutPart(cutSize);
        _soundManager.ResetPerfectSeries();
        UpdateOldPlatform();
    }

    private void UpdatePlatformSizeAndPosition(float cutSize)
    {
        float newPlatformSize = _currentPlatform.transform.localScale.x - cutSize;
        float newPlatformPositionX = (_currentPlatform.position.x < oldPlatform.transform.position.x)
            ? _currentPlatform.transform.position.x + cutSize / 2
            : _currentPlatform.transform.position.x - cutSize / 2;

        _currentPlatform.localScale = new Vector3(newPlatformSize, _currentPlatform.transform.localScale.y, _currentPlatform.localScale.z);
        var position = _currentPlatform.position;
        position = new Vector3(newPlatformPositionX, position.y, position.z);
        _currentPlatform.position = position;
    }

    private void CreateCutPart(float cutSize)
    {
        float cutPartPosX = oldPlatform.transform.position.x + (oldPlatform.localScale.x / 2) * (_currentPlatform.transform.position.x < oldPlatform.position.x ? -1 : 1);
        Vector3 cutPartPosition = new Vector3(cutPartPosX, _currentPlatform.transform.position.y, _currentPlatform.position.z);

        Transform cutPart = _platformFactory.CreatePlatform(cutPartPosition, Quaternion.identity, transform, true);
        cutPart.localScale = new Vector3(cutSize, cutPart.localScale.y, cutPart.transform.localScale.z);
    }

    private void UpdateOldPlatform()
    {
        oldPlatform = _currentPlatform;
        OnTargetPositionChanged?.Invoke(oldPlatform.transform.position);
    }

    private void CreateNewPlatform()
    {
        if (++_currentPlatformCount >= goalPlatformCount)
        {
            _currentPlatform = null;
            OnTargetPositionChanged?.Invoke(_finishPos);
            _gameManager.EndGame(true, 1.5f);
            return;
        }

        Vector3 newPos = oldPlatform.transform.position + Vector3.forward * _platformScaleZ + (_movingRight ? Vector3.right : Vector3.left) * moveLimit;
        _currentPlatform = _platformFactory.CreatePlatform(newPos, Quaternion.identity, transform);

        ApplyNextMaterial();

        _currentPlatform.transform.localScale = oldPlatform.transform.localScale;
    }

    private void ApplyNextMaterial()
    {
        if (stackMats.Count > 0)
        {
            Material nextMat = stackMats[0];
            stackMats.Add(nextMat);
            stackMats.RemoveAt(0);
            _currentPlatform.GetComponent<MeshRenderer>().material = nextMat;
        }
    }
}
