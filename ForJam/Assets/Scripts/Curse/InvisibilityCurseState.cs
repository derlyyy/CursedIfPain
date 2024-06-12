public class InvisibilityCurseState : CurseState
{
    public InvisibilityCurseState(Player player, CurseManager curseManager, DialogueSystem dialogueSystem, IStationStateSwitcher stateSwitcher) : base(player, curseManager, dialogueSystem, stateSwitcher)
    {
        
    }
    
    public override void Start()
    {
        if (!_curseManager.curse_3)
        {
            _dialogueSystem.StartDialogue(7);
            _curseManager.curse_3 = true;
            _curseManager.Tutorial();
            
        }
        else
        {
            _curseManager.ActivateShield();
        }
    }

    public override void Stop()
    {
        
    }

    public override void Change()
    {
        
    }

    public override void Upgrade()
    {
        
    }

    public override void Reset()
    {
        ;
    }
}
