using UnityEngine;

public class VisualCardsHandler : MonoBehaviour
{

    public static VisualCardsHandler instance;

    private void Awake()
    {
        instance = this;
    }
}