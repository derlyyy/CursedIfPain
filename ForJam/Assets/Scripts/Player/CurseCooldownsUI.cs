using UnityEngine;
using UnityEngine.UI;

public class CurseCooldownsUI : MonoBehaviour
{
    public CurseManager curseManager;

    public Image fillCurse;

    private void Update()
    {
        fillCurse.fillAmount = curseManager.cooldownTimer / curseManager.cooldown;
    }
}
