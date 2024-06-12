using SonityTemplate;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public DialogueSystem dialogueSystem;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TemplateSoundMusicManager.Instance.PlayIngame();
        //StartDialogue();
    }

    public void StartDialogue(int index)
    {
        dialogueSystem.StartDialogue(index);
    }
}
