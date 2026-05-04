using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;

public class Teleporter : MonoBehaviour, IInteractable
{
    public Transform teleportDestination;
    private GameObject player;
    private GameObject parent;
    public GameObject newParent;
    public GameObject lantern;
    public GameObject boatLantern;
    
    private float defaultYOffset = 1.56f;
    public float sittingYOffset = 0.86f;
    public string teleportationObjectName = "Teleportation";
    public string locomotionObjectName = "locomotion";
    private GameObject locomotion;
    private GameObject teleportation;
    private XROrigin _xrOrigin;
    
    
    //Just making sure the fields are the same across all instances of the script
    private static Collider _boatCollider;
    [SerializeField] private Collider boatCollider;
    private static Collider _lHandCollider;
    [SerializeField] private Collider lHandCollider;
    private static Collider _rHandCollider;
    [SerializeField] private Collider rHandCollider;

    [SerializeField] private LayerMask _disembarkBoatLayer;
    [SerializeField] private float _radius;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _xrOrigin = player.gameObject.GetComponent<XROrigin>();
        locomotion = GameObject.Find(locomotionObjectName);
        teleportation = GameObject.Find(teleportationObjectName);
        defaultYOffset = _xrOrigin.CameraYOffset;
        if (player == null) Debug.LogError("Player not found");
        if (_boatCollider == null && boatCollider != null) _boatCollider = boatCollider;
        if(_lHandCollider == null && lHandCollider != null) _lHandCollider = lHandCollider;
        if (_rHandCollider == null && rHandCollider != null) _rHandCollider = rHandCollider;
        //_boatCollider = GetComponent<Collider>();
        //lantern =  GameObject.FindWithTag("Lantern"); //TODO: FIND LANTERN, THIS SHIT DONT WORK
    }

    public void UpdateDisembark()
    {
        Collider[] hit =  Physics.OverlapSphere(transform.position, _radius, LayerMask.GetMask("Default"));

        foreach (Collider col in hit)
        {
            
                teleportDestination.position = col.gameObject.transform.position;
                Interact();
                RemoveParent();
                TriggerStanding();
                _boatCollider.enabled = true;
                _lHandCollider.enabled = false;
                _rHandCollider.enabled = false;
                lantern.SetActive(true);
                if (boatLantern != null)
                {
                    boatLantern.SetActive(false);
                }
            
           
        }
     
    }

    public void EmbarkBoat()
    {
        Interact();
        SetParent(newParent.transform);
        TriggerSitting();
                    _boatCollider.enabled = false;
        _lHandCollider.enabled = true;
        _rHandCollider.enabled = true;
        lantern.SetActive(false);
        if (boatLantern != null)
        {
            boatLantern.SetActive(true);
        }
        
        
        FmodController.current.UpdateBoatSection();
        Debug.Log("EmbarkBoat " + FmodController.current.RowSection);
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
