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
            FmodController.current.RowSection =  NextRowSection;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GetComponent<SphereCollider>().bounds.center, GetComponent<SphereCollider>().radius);
    }
}
