using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitcher : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] _lasers;
    public void Interact()
    {
        ToggleLasers();
    }

    private void ToggleLasers()
    {
        foreach (var laser in _lasers)
        {
            laser.GetComponent<Laser>().Toggle();
        }
    }
}
