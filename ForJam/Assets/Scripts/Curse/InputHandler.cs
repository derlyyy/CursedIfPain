using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode TELEPORT_KEY = KeyCode.Z;
    [SerializeField] private KeyCode SCALE_KEY = KeyCode.X;
    [SerializeField] private KeyCode INVISIBLE_KEY = KeyCode.C;

    [SerializeField] private StationBehaviour stationBehaviour;
    
    private void Update()
    {
        if (Input.GetKeyDown(TELEPORT_KEY))
        {
            stationBehaviour.SwitchState<ChangeCurseState>();
            Debug.Log("z");
        }
        if (Input.GetKeyDown(SCALE_KEY))
        {
            stationBehaviour.SwitchState<UpgradeCurseState>();
            Debug.Log("x");
        }
        if (Input.GetKeyDown(INVISIBLE_KEY))
        {
            stationBehaviour.SwitchState<InvisibilityCurseState>();
            Debug.Log("c");
        }
    }
}
