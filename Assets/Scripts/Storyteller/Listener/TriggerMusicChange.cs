using System;
using UnityEngine;

public class TriggerMusicChange : MonoBehaviour
{
    #region VARIABLES

    [Header("To change music and ambience")]
    
        [SerializeField] private bool _onFishManIsle;
        [SerializeField] private bool _onLightHouseReturn;
        [Header("The section to play")]
        [SerializeField] private int _fishmanIsleSections;
        [SerializeField] private int _lightHouseReturnSections;
        
        [SerializeField] private int _sectionChange;
        
        [Header("To change music on EXIT")]
        [SerializeField] private bool _changSectionOnExit;
        //Ex = Exit
        [Space]
        [SerializeField] private int _Ex_fishmanIsleSections;
        [SerializeField] private int _Ex_lightHouseReturnSections;
        
        [SerializeField] private int _Ex_sectionChange;
    
        private BoxCollider _collider;

    #endregion
    
    
    //private void Start() => FmodController.current.UpdateSection(0);
    private void OnTriggerEnter(Collider other) 
    {

        Debug.Log(other + "Entered Trigger " + _sectionChange);

        FmodController.current.UpdateSection(_sectionChange);
        
        if (_onFishManIsle)
        {
            /*  0 = Village 1 = Church 2 = Cave   */
            FmodController.current.UpdateFishmanIsle(_fishmanIsleSections);
        }

        if (_onLightHouseReturn)
        {
            /*  0 = Intro (loop) 1 = Find the body (loop) 2 = End (no loop)   */
            FmodController.current.UpdateLighthouseReturn(_lightHouseReturnSections);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other + "Exited Trigger " + _Ex_sectionChange);
        if (_changSectionOnExit)
        {
            FmodController.current.UpdateSection(_Ex_sectionChange);
        
            if (_onFishManIsle)
            {
                FmodController.current.UpdateFishmanIsle(_Ex_fishmanIsleSections);
            }

            if (_onLightHouseReturn)
            {
                FmodController.current.UpdateLighthouseReturn(_Ex_lightHouseReturnSections);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        _collider =  gameObject.GetComponent<BoxCollider>();
        Gizmos.color = Color.purple;
        Gizmos.DrawWireCube(_collider.bounds.center, _collider.size);
    }
}
