using UI_Scripts;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public Transform menuTeleportTarget;
    public Transform gameTeleportTarget;
    public GameObject player;

    
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player")) { player = GameObject.FindGameObjectWithTag("Player"); }
        else { Debug.LogError(name + ": Player not found"); }
        
        player.transform.position =  menuTeleportTarget.position;
        player.transform.rotation = menuTeleportTarget.rotation;
        OutOfBounds.Instance.SaveLastPlayerPosition();
    }

    public void TeleportGame()
    {
        player.transform.position = gameTeleportTarget.position;
        player.transform.rotation = gameTeleportTarget.rotation;
        OutOfBounds.Instance.SaveLastPlayerPosition();
    }
    
}
