using System;
using UnityEngine;

public class BouncingTrampoline : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (other != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, bounceForce);
        }
    }
}
