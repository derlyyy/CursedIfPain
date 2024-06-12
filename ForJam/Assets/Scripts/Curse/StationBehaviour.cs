using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StationBehaviour : MonoBehaviour, IStationStateSwitcher
{
    [SerializeField] private Player _player;
    [SerializeField] private CurseManager _curseManager;
    [SerializeField] private DialogueSystem _dialogueSystem;

    private CurseState _currentState;
    private List<CurseState> _allStates;

    private void Start()
    {
        _allStates = new List<CurseState>()
        {
            new ChangeCurseState(_player, _curseManager, _dialogueSystem, this),
            new UpgradeCurseState(_player, _curseManager, _dialogueSystem, this),
            new InvisibilityCurseState(_player, _curseManager, _dialogueSystem,this)
        };
        _currentState = _allStates[0];
    }

    public void ChangeCurse()
    {
        _currentState.Change();
    }
    
    public void UpgradeCurse()
    {
        _currentState.Upgrade();
    }

    public void SwitchState<T>() where T : CurseState
    {
        var state = _allStates.FirstOrDefault(s => s is T);
        _currentState.Stop();
        state.Start();
        _currentState = state;
    }
}
