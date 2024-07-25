using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pebble : MonoBehaviour, IInteractable
{
    public static bool HasPebble = false;

    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        HasPebble = false;
    }

    public void Interact()
    {
        if (HasPebble)
            return;

        HasPebble = true;
        GameEvents.OnGetPebble?.Invoke();
        Destroy(gameObject);

    }

    public void Move()
    {
        _rb.AddForce(transform.up * _speed, ForceMode2D.Impulse);
    }

}
