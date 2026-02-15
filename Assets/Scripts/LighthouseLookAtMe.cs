using UnityEngine;

public class LighthouseLookAtMe : MonoBehaviour
{
    [SerializeField] private Vector3 NextIsland;
    private void OnTriggerEnter(Collider other)
    {
        LighthouseSpotlight.instance.LookAtMe(NextIsland);
    }
}
