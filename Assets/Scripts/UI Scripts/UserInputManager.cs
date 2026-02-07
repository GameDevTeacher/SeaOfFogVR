using UnityEngine;

public class UserInputManager : MonoBehaviour
{
    private XRIDefaultInputActions _inputSystem;

    public bool Pause;
    
    private void Awake() { _inputSystem = new XRIDefaultInputActions(); }
    
    private void OnEnable() { _inputSystem.Enable(); }
    
    private void OnDisable() { _inputSystem.Disable(); }

    private void Update()
    {
        Pause = _inputSystem.XRILeftInteraction.Pause.WasPressedThisFrame();
    }
}
