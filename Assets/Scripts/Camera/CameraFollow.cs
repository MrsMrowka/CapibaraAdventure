using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;

    private void Update()
    {
        Vector3 pos = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y, gameObject.transform.position.z);
        gameObject.transform.position = pos;
    }
}
