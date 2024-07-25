using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject[] _doorsToOpen;

    public void Interact()
    {
        foreach(var door in _doorsToOpen)
        {
            door.GetComponent<Door>().Toggle();
        }
    }
    
}
