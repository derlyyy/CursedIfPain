using System.Collections.Generic;
using UnityEngine;

public class ChangeCurseState : CurseState
{
    public ChangeCurseState(Player player, CurseManager curseManager, DialogueSystem dialogueSystem, IStationStateSwitcher stateSwitcher) : base(player, curseManager, dialogueSystem, stateSwitcher)
    {
        
    }

    public override void Start()
    {
        Debug.Log("teleporting started");
        if (!_curseManager.curse_1)
        {
            _dialogueSystem.StartDialogue(5);
            _curseManager.curse_1 = true;
            _curseManager.Tutorial();
        }
        else
        {
            _curseManager.InitiateTeleport();
        }
    }

    public override void Stop()
    {
        
    }

    public override void Change()
    {
        Debug.Log("change");
    }

    public override void Upgrade()
    {
        // ничего не делаем = улучшение невозможно в данный момент
    }

    public override void Reset()
    {
        
    }
}
