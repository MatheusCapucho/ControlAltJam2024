using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask _interationMask;
    [SerializeField] private float _interactionRange;

    private Collider2D _collider;

    private Inputs _input;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _input = new Inputs();
    }
    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Interact.performed += TryToInteract;
    }

    private void OnDisable()
    {
        _input.Player.Interact.performed -= TryToInteract;
    }
    private void TryToInteract(InputAction.CallbackContext ctx)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _interactionRange, _interationMask);
        foreach (var hitCollider in hitColliders)
        {
            IInteractable interactable = hitCollider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactionRange);
    }
}
