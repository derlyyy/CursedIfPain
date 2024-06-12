using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public int index;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.StartDialogue(index);
            if (DialogueSystem.instance.isTyping != false)
            {
                Destroy(gameObject);
            }
        }
    }
}
