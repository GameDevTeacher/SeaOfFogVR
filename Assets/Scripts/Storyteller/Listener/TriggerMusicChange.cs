using UnityEngine;

public class TriggerMusicChange : MonoBehaviour
{


    [SerializeField] private int AmbienceSectionChange;

    private void OnTriggerEnter(Collider other) 
    {   
        
        Debug.Log(other + "Entered Trigger " + AmbienceSectionChange);

        FmodController.current.UpdateSection(AmbienceSectionChange);
        
    }
}
