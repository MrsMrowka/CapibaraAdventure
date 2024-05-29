using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float maxTimer;

    private void Start()
    {
        GlobalVariables.Timer = maxTimer;
    }
}
