using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private float maxDistance;
    [SerializeField] private Transform target;
    [SerializeField] private PauseScrub pauseScrub;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    private void Update()
    {
        if (Vector3.Distance(transform.position, target.position) > maxDistance)
        {
            pauseScrub.isPaused = false;
            Destroy(gameObject);
        }
    }
}
