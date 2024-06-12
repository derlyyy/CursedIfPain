using UnityEngine;

public class UpgradeCurseState : CurseState
{
    
    public UpgradeCurseState(Player player, CurseManager curseManager, DialogueSystem dialogueSystem, IStationStateSwitcher stateSwitcher) : base(player, curseManager, dialogueSystem, stateSwitcher)
    {
        
    }

    public override void Start()
    {
        Debug.Log("upgrade started");
        if (!_curseManager.curse_2)
        {
            _dialogueSystem.StartDialogue(6);
            _curseManager.curse_2 = true;
            _curseManager.Tutorial();
        }
        else
        {
            _curseManager.ChangeSize();
        }
    }

    public override void Stop()
    {
        
    }

    public override void Change()
    {
        _player.data.stats.sizeMultiplier = 0.65f;
        _player.data.stats.ConfigureMassAndSize();
    }

    public override void Upgrade()
    {
        Debug.Log("upgrade");
    }

    public override void Reset()
    {
        _player.data.stats.sizeMultiplier = 0.81f;
        _player.data.stats.ConfigureMassAndSize();
    }
}
