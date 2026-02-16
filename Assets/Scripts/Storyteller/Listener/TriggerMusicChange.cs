using UnityEngine;

public class TriggerMusicChange : MonoBehaviour
{
    [Header("To change music and ambience")]
    [SerializeField] private bool _onFishManIsle;
    [SerializeField] private bool _onLightHouseReturn;
    [Header("The section to play")]
    [SerializeField] private int _fishmanIsleSections;
    [SerializeField] private int _lightHouseReturnSections;
    
    [SerializeField] private int _ambienceSectionChange;

    private void OnTriggerEnter(Collider other) 
    {   
        
        Debug.Log(other + "Entered Trigger " + _ambienceSectionChange);

        FmodController.current.UpdateSection(_ambienceSectionChange);
        if (_onFishManIsle)
        {
            /*  0 = Village
            1 = Church
            2 = Cave   */
            FmodController.current.UpdateFishmanIsle(_fishmanIsleSections);
        }

        if (_onLightHouseReturn)
        {
            /*  0 = Intro (loop)
            1 = Find the body (loop)
            2 = End (no loop)   */
            FmodController.current.UpdateLighthouseReturn(_lightHouseReturnSections);
        }
        
    }
}
