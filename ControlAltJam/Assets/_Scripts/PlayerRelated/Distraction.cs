using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Distraction : MonoBehaviour
{
    private Inputs _input;

    private Vector2 _mousePosition;
    private Camera _camera;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private GameObject _pebblePrefab;

    private void Awake()
    {
        _input = new Inputs();
        _camera = Camera.main;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.MouseMove.performed += OnMouseMove;
        _input.Player.MouseInteract.performed += OnTriggerDistraction;
        GameEvents.OnGetPebble += GetPebbleLogic;


    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.MouseMove.performed -= OnMouseMove;
        _input.Player.MouseInteract.performed -= OnTriggerDistraction;
        GameEvents.OnGetPebble -= GetPebbleLogic;

    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        _mousePosition = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());

        Vector2 direction = _mousePosition - new Vector2(transform.position.x, transform.position.y);
        transform.up = direction;


    }
    private void OnTriggerDistraction(InputAction.CallbackContext context)
    {
        if (!Pebble.HasPebble)
            return;

        _spriteRenderer.enabled = false;
        var clone =Instantiate(_pebblePrefab, transform.GetChild(0).position, transform.rotation);
        clone.GetComponent<Pebble>().Move();

    }

    public void GetPebbleLogic() { _spriteRenderer.enabled = true; }


}
