using Unity.VisualScripting;
using UnityEngine;

public class FogMover : MonoBehaviour
{
    private bool _triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (_triggered) return;
        if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Boat"))
        {
            FogController.instance.NextFog();
        }
    }
}
