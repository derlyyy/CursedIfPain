using System.Collections.Generic;

public abstract class CurseState
{
    protected readonly Player _player;
    protected readonly CurseManager _curseManager;
    protected readonly IStationStateSwitcher _stateSwitcher;
    protected readonly DialogueSystem _dialogueSystem;

    protected CurseState(Player player, CurseManager curseManager, DialogueSystem dialogueSystem, IStationStateSwitcher stateSwitcher)
    {
        _player = player;
        _curseManager = curseManager;
        _dialogueSystem = dialogueSystem;
        _stateSwitcher = stateSwitcher;
    }

    public abstract void Start();
    public abstract void Stop();
    
    public abstract void Change();
    public abstract void Upgrade();
    public abstract void Reset();
}
