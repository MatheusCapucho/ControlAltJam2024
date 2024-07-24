using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToHide : MonoBehaviour, IInteractable
{

    private GameObject _playerGO;
    private bool _enabled = true;

    void Start()
    {
        _playerGO = FindObjectOfType<Interactor>().gameObject;
        gameObject.layer = 8;
    }

    public void Interact()
    {
        ToggleHide();
    }

    private void ToggleHide()
    {
        _enabled = !_enabled;

        _playerGO.transform.GetChild(0).gameObject.SetActive(_enabled);
        // trigger animation
        // trigger sound cue

    }

}
