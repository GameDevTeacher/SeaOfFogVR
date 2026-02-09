using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class LeverManager : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] private TextMeshProUGUI text;
    
    [Header("Lever")]
    [SerializeField] private HingeJoint lever;
    [SerializeField] private Rigidbody leverRigidbody;
    [SerializeField] private Transform leverTransform;
    
    [Header("Limits")]
    [SerializeField] private float maxLimit;
    [SerializeField] private float minLimit;
    
    // Current issues: at the start, we get NaN. In the update, the value doesn't go below 50.
    // changing it to the transform doesn't help either.
    
    private void Start()
    {
        //todo: asign a starting value on start, starting value of 50,preferably then saved&pulled to a settings config
        leverTransform.eulerAngles = new Vector3(0, leverTransform.eulerAngles.y, 0);
        
        if (lever.useLimits)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            text.text = "Value: " + Mathf.Round(ScaleRegulator(value));
        }
    }

    private void Update()
    {
        if (leverRigidbody.angularVelocity != Vector3.zero)
        {
            var value = Mathf.Clamp(lever.angle, minLimit, maxLimit);
            // print("The current value is: " + value);
            // print("When regulated, that value becomes: " + ScaleRegulator(value));
            text.text = "Value: " + Mathf.Round(ScaleRegulator(value));
            // print(Mathf.Round(lever.angle));
        }
    }

    private float ScaleRegulator(float value)
    {
        // print("value is" + value);
        return (value + 75) * 2 / 3;
    }
}
