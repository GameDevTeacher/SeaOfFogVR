using System;
using UnityEngine;

public class RowCountCheck : MonoBehaviour
{
    public int NextRowSection;
    public bool triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
        {
            //Sets it so that the next rowtrigger will automaticaly be
            //the subsequent row without changing anything in the inspector.
            FmodController.current.RowSection =  NextRowSection;
        }
    }

        
}
