using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractable
{
    public Transform teleportDestination;
    private GameObject player;


    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null) Debug.LogError("Player not found");
    }
    

    public void Interact()
    {
        print ("teleport");
        player.transform.position = teleportDestination.position;
        
    }
    
}
