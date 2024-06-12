using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public bool isGod;
    
    public void TakeDamage()
    {
        if (!isGod)
        {
            PlayerManager.instance.Die();
            Debug.Log("player die");
        }
    }
}
