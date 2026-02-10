using System.Collections;
using UnityEngine;

namespace UI_Scripts
{
    public class OutOfBounds : MonoBehaviour
    {
        [Header("Ray Stuff")]
        [SerializeField] private Vector3 rayDirection = Vector3.down;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask layerMask;
        
        [Header("Player Stuff")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Vector3 lastPlayerPosition;
        
        [Header("Out of Bounds Effect")]
        [SerializeField] private VRNoPeeking vrNoPeeking;

        private void Update()
        {
            if (IsTouchingGround())
            {
                print("Player is touching ground.");
            }
            else
            {
                StartCoroutine(BlackInBlackOut(1f));
            }
        }

        private bool IsTouchingGround()
        {
            return Physics.Raycast(transform.position, rayDirection, rayDistance, layerMask);
        }

        public void SaveLastPlayerPosition()
        {
            print("Saving last player position.");
            lastPlayerPosition = playerTransform.position;
        }

        private IEnumerator BlackInBlackOut(float seconds)
        {
            print("I am not where I should be.");
            vrNoPeeking.CameraFadeOut(1f);
            playerTransform.position = lastPlayerPosition;
            
            yield return new WaitForSeconds(seconds);
            
            vrNoPeeking.CameraFadeIn(0f);
            print("I am where I should be.");
        } 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.deepPink;
            Gizmos.DrawRay(transform.position, rayDirection.normalized * rayDistance);
        }
    }
}
    
