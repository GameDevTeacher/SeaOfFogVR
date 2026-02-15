using UnityEngine;

public class LighthouseSpotlight : MonoBehaviour
{
    public static LighthouseSpotlight instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void LookAtMe(Vector3 target)
    {
        instance.transform.LookAt(target);
    }
    
}
