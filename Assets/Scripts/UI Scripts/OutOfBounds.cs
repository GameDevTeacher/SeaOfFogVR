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
        public Vector3 lastPlayerPosition;
        public Quaternion lastPlayerRotation;
        [SerializeField] private float secondsUntilReset;
        
        [Header("Out of Bounds Effect")]
        [SerializeField] private VRNoPeeking vrNoPeeking;
        
        [Header("fuck this shit")]
        [SerializeField] private List<TeleportationArea> teleportationAreas;
        private GameObject[] _teleportationAreasGameObjects;

        private void Start()
        {
            SaveLastPlayerPosition();

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
                ResetPosition();
            }
        }

        private bool IsTouchingGround()
        {
            return Physics.Raycast(transform.position, rayDirection, rayDistance, layerMask);
        }

        public void SaveLastPlayerPosition()
        {
            lastPlayerPosition = playerTransform.position;
            lastPlayerRotation = playerTransform.rotation;
        }

        public void ResetPosition()
        {
            StartCoroutine(ResetPlayerPosition());
        }

        private IEnumerator ResetPlayerPosition()
        {
            vrNoPeeking.CameraFadeOut(1f, vrNoPeeking.defaultFadeOutSpeed);
            
            yield return new WaitForSeconds(secondsUntilReset);
            
            vrNoPeeking.CameraFadeIn(0f);
            playerTransform.position = lastPlayerPosition;
            playerTransform.rotation = lastPlayerRotation;
        } 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.deepPink;
            Gizmos.DrawRay(transform.position, rayDirection.normalized * rayDistance);
        }
    }
}
    
