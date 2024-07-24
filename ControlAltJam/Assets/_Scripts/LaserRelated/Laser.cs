using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private bool _timedLaser = false;
    [SerializeField] private float _resetAfterSeconds;

    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private bool _enabled;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _enabled = _collider.enabled;
        gameObject.layer = 6;
    }

    public void Toggle()
    {
        _enabled = !_enabled;
        _spriteRenderer.enabled = _enabled;
        _collider.enabled = _enabled;

        if(_timedLaser && !_enabled)
        {
            StopAllCoroutines();
            StartCoroutine(TimedLaserReset());
        }

    }

    IEnumerator TimedLaserReset()
    {
        yield return new WaitForSeconds(_resetAfterSeconds);
        Toggle();
    }



}
