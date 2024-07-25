using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _isTimed = false;
    [SerializeField] private float _timeToReset = 0f;
    private bool _closed = true;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    public void Toggle()
    {
        _closed = !_closed;
        //_spriteRenderer.enabled = _closed;
        _collider.enabled = _closed;

        if (_isTimed && !_closed)
        {
            StopAllCoroutines();
            StartCoroutine(TimedDoorReset());
        }
    }

    IEnumerator TimedDoorReset()
    {
        yield return new WaitForSeconds(_timeToReset);
        Toggle();
    }
}
