using UnityEngine;

public class OarTip : MonoBehaviour
{
    private Vector3 LastPosition;

    public Vector3 RowingVector
    {
        get
        {
            var foo = transform.position - LastPosition;
            return new Vector3(-foo.x, 0, -foo.z);
            
        }
    }
    
    public bool _touchingWater { get; private set; }

    private void Update()
    {
        LastPosition = transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _touchingWater = true;
            
            FmodController.current.OarSplash(gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            _touchingWater = false;
            FmodController.current.OarSplash(gameObject);
        }
    }
}
