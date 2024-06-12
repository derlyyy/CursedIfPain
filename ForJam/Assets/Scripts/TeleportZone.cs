using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        HideZone();
    }

    public void ShowZone()
    {
        _renderer.enabled = true;
    }

    public void HideZone()
    {
        _renderer.enabled = false;
    }
    
    public bool IsMouseOverZone(Vector3 mousePosition)
    {
        var bounds = GetComponent<Collider2D>().bounds;
        return bounds.Contains(mousePosition);
    }
}
