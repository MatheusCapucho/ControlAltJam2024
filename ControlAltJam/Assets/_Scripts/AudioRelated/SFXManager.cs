using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static SFXManager _instance;
    public static SFXManager Instance => _instance;

    [SerializeField] private AudioSource _audioSourcePrefab;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if(_instance != null && _instance != this)
            Destroy(_instance);
    }

    public void PlaySFX(AudioClip clip, Transform transform)
    {

        AudioSource audioSource = Instantiate(_audioSourcePrefab, transform.position, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.Play();
        var clipLenght = audioSource.clip.length;
        Destroy(audioSource, clipLenght + .5f);
    }

}
