using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject doorsToOpen;
    
    public void OpenDoor()
    {
        doorsToOpen.GetComponent<DoorForKey>().OpenDoor();
    }
}
