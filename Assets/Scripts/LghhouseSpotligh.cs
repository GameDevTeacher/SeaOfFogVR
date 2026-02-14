using UnityEngine;

public class LighthouseSpotlight : MonoBehaviour
{
    public static LighthouseSpotlight instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    public void LookAtMe(GameObject target)
    {
        instance.transform.LookAt(target.transform);
    }
    
}
