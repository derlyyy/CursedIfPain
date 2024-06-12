using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private bool isReal;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isReal)
            {
                // респавн игрока
                other.GetComponent<CharacterData>().health.TakeDamage();
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
