using UnityEngine;

public class TriggerMusicChange : MonoBehaviour
{


    [SerializeField] private int AmbienceSectionChange;
    void Start()
    {
        StoryEventsController.current.onTriggerEntered += TriggerEntered;
        StoryEventsController.current.onTriggerExited += TriggerExited;
        
    }

    private void TriggerEntered(int id)
    {   
        if (id == AmbienceSectionChange)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            Debug.Log("Entered Trigger " + AmbienceSectionChange);

            FmodController.current.UpdateSection(AmbienceSectionChange);
        }
    }

    private void TriggerExited(int id)
    { if (id == AmbienceSectionChange)
        {
            GetComponent<MeshRenderer>().material.color = Color.dodgerBlue;
            Debug.Log("Exited Trigger");
        }
    }
}
