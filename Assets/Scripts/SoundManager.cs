using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    private float _initialPitch = 1.0f;
    private float _pitchStep = 0.1f;
    private float _maxPitch = 2.0f;
    private void Start()
    {
        audioSource.pitch = _initialPitch;
    }
    public void PlayPerfectSound()
    {
        audioSource.Play();
        
        if (audioSource.pitch < _maxPitch)
        {
            audioSource.pitch += _pitchStep;
        }
    }
    public void ResetPerfectSeries()
    {
        audioSource.pitch = _initialPitch;
    }
}