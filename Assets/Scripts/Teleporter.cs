using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class Teleporter : MonoBehaviour, IInteractable
{
    public Transform teleportDestination;
    private GameObject player;
    private GameObject parent;
    private float defaultYOffset = 1.56f;
    public float sittingYOffset = 0.86f;
    public string teleportationObjectName = "Teleportation";
    public string locomotionObjectName = "locomotion";
    private GameObject locomotion;
    private GameObject teleportation;
    private XROrigin _xrOrigin;


    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _xrOrigin = player.gameObject.GetComponent<XROrigin>();
        locomotion = GameObject.Find(locomotionObjectName);
        teleportation = GameObject.Find(teleportationObjectName);
        defaultYOffset = _xrOrigin.CameraYOffset;
        if (player == null) Debug.LogError("Player not found");
    }
    

    public void Interact()
    {
        print ("teleport");
        
        player.transform.position = teleportDestination.position;
        player.transform.rotation = teleportDestination.rotation;
    }

    public void SetParent(Transform newParent)
    {
            player.transform.parent = newParent;
    }

    public void RemoveParent()
    {
        player.transform.parent = null;
    }

    public void TriggerSitting()
    {
        locomotion.SetActive(false);
        teleportation.SetActive(false);
        //_xrOrigin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Device;
        _xrOrigin.CameraYOffset = sittingYOffset;
    }

    public void TriggerStanding()
    {
        locomotion.SetActive(true);
        teleportation.SetActive(true);
        //_xrOrigin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Device;
        _xrOrigin.CameraYOffset = defaultYOffset;
    }
    
}
