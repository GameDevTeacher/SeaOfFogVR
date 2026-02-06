using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class Teleporter : MonoBehaviour, IInteractable
{
    public Transform teleportDestination;
    private GameObject player;
    private GameObject parent;
    public float defaultYOffset = 1.19f;
    public float sittingYOffset;
    public GameObject locomotion;
    public GameObject teleportation;
    private XROrigin _xrOrigin;


    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _xrOrigin = player.gameObject.GetComponent<XROrigin>();
        if (player == null) Debug.LogError("Player not found");
    }
    

    public void Interact()
    {
        print ("teleport");
        
        player.transform.position = teleportDestination.position;
        player.transform.localRotation = teleportDestination.localRotation;
    }

    public void SetParent(Transform newParent)
    {
            player.transform.parent = newParent == player.transform ? null : newParent;
    }

    public void TriggerSitting()
    {
        locomotion.SetActive(false);
        teleportation.SetActive(false);
        _xrOrigin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Device;
        _xrOrigin.CameraYOffset = sittingYOffset;
    }

    public void TriggerStanding()
    {
        locomotion.SetActive(true);
        teleportation.SetActive(true);
        _xrOrigin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Floor;
        _xrOrigin.CameraYOffset = defaultYOffset;
    }
    
}
