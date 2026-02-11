using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class PlayerSaving : MonoBehaviour
{
    private Transform _position;
    private XROrigin _xrOrigin;
    private GameObject _locomotion;
    private GameObject _teleportation;

    private void Awake()
    {
        _position = gameObject.transform;
        _xrOrigin = gameObject.GetComponent<XROrigin>();
        _locomotion = GameObject.Find("locomotion");
        _teleportation = GameObject.Find("Teleportation");
    }
    
    public void Save(ref PlayerSaveData data) //refs let you read and write with the original PlayerSaveData struct
    {
        data.position = _position.position;
        data.rotation = _position.rotation;
        data.parent =  _position.parent;
        data.cameraOffset = _xrOrigin.CameraYOffset;
        data.sitting = _teleportation;
    }

    public void Load(PlayerSaveData data)
    {
        transform.position = data.position;
        transform.rotation = data.rotation;
        _xrOrigin.CameraYOffset = data.cameraOffset;
        if (data.parent == null)
        {
            transform.parent = null;
        }
        else
        {
            transform.SetParent(data.parent, false);
        }

        _teleportation.SetActive(data.sitting);
        _locomotion.SetActive(data.sitting);

    }
    
}

[System.Serializable]
public struct PlayerSaveData
{
    public Vector3 position;
    public Quaternion rotation;
    public Transform parent;
    public float cameraOffset;
    public bool sitting;
}
