using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

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
        [SerializeField] private float secondsUntilReset;
        
        [Header("Out of Bounds Effect")]
        [SerializeField] private VRNoPeeking vrNoPeeking;
        
        [Header("fuck this shit")]
        [SerializeField] private List<TeleportationArea> teleportationAreas;
        private GameObject[] _teleportationAreasGameObjects;

        private void Start()
        {
            lastPlayerPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);

            _teleportationAreasGameObjects = GameObject.FindGameObjectsWithTag("Teleport");

            foreach (var area in _teleportationAreasGameObjects)
            {
                teleportationAreas.Add(area.gameObject.GetComponent<TeleportationArea>());
            }
            
            for (int i = 0; i < teleportationAreas.Count; i++)
            {
                teleportationAreas[i].teleporting.AddListener(arg0 => {SaveLastPlayerPosition();});
            }
        }

        private void Update()
        {
            if (!IsTouchingGround())
            {
                StartCoroutine(ResetPlayerPosition());
            }
        }

        private bool IsTouchingGround()
        {
            return Physics.Raycast(transform.position, rayDirection, rayDistance, layerMask);
        }

        public void SaveLastPlayerPosition()
        {
            lastPlayerPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        }

        private IEnumerator ResetPlayerPosition()
        {
            vrNoPeeking.CameraFadeOut(1f);
            
            
            yield return new WaitForSeconds(secondsUntilReset);
            
            vrNoPeeking.CameraFadeIn(0f);
            playerTransform.position = new Vector3(lastPlayerPosition.x, lastPlayerPosition.y, lastPlayerPosition.z);
        } 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.deepPink;
            Gizmos.DrawRay(transform.position, rayDirection.normalized * rayDistance);
        }
    }
}
    
