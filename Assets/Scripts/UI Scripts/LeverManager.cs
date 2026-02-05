using UnityEngine;
using TMPro;

public class LeverManager : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] private TextMeshProUGUI text;
    
    [Header("Lever")]
    [SerializeField] private HingeJoint lever;
    [SerializeField] private Rigidbody leverRigidbody;

    private void Start()
    {
        // Mainly for testing
        if (lever.useLimits)
        {
            var value = Mathf.Round(Mathf.Clamp(lever.angle, lever.limits.min, lever.limits.max));
            text.text = "Value: " + ScaleRegulator(value);
            print(lever.angle);
        }
    }

    private void Update()
    {
        if (leverRigidbody.angularVelocity != Vector3.zero)
        {
            var value = Mathf.Round(Mathf.Clamp(lever.angle, lever.limits.min, lever.limits.max));
            text.text = "Value: " + ScaleRegulator(value);
            print(Mathf.Round(lever.angle));
        }
    }

    private float ScaleRegulator(float value)
    {
        return (value + 75) * 2 / 3;
    }
}
