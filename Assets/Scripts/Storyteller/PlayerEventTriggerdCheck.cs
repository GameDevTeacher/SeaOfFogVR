using System;
using UnityEngine;

public class HasPlayerEnteredCave : MonoBehaviour
{
    public SphereCollider _collider;
    private void onTriggerEnter(Collider other)
    {
        StoryEventsController.current.caveEntered = true;
    }

    private void OnDrawGizmos()
    {
        _collider = gameObject.GetComponent<SphereCollider>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_collider.bounds.center, _collider.radius);
    }
}
