using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxToPush : MonoBehaviour
{
    public float boxWeigth;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyForce(Vector2 vector2)
    {
        rb.velocity = vector2 / boxWeigth;
    }
}
