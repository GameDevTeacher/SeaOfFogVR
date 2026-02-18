using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace UI_Scripts
{
    public class OutOfBounds : MonoBehaviour
    {
        public static OutOfBounds Instance;
        public bool resetTriggered;
        public bool outOfBoundsDisabled;
        
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
        
        [Header("multi-scene teleporter area stuff")]
        [SerializeField] private List<TeleportationArea> teleportationAreas;
        private GameObject[] _teleportationAreasGameObjects;

        private async void Start()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
            resetTriggered = false;
            
            SaveLastPlayerPosition();

            _teleportationAreasGameObjects = GameObject.FindGameObjectsWithTag("Teleport");

            //multi-scene teleport area fixer thinger
            foreach (var area in _teleportationAreasGameObjects)
            {
                teleportationAreas.Add(area.gameObject.GetComponent<TeleportationArea>());
            }
            for (int i = 0; i < teleportationAreas.Count; i++)
            {
                teleportationAreas[i].teleporting.AddListener(arg0 => {SaveLastPlayerPosition();});
            }
            
            outOfBoundsDisabled = true;
            await Awaitable.WaitForSecondsAsync(0.5f);
            outOfBoundsDisabled = false;

        }

        private void Update()
        {
            if (outOfBoundsDisabled) return;
            if (!IsTouchingGround() && !resetTriggered)
            {
                resetTriggered = true;
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
            Debug.Log("Reset Position");
            StartCoroutine(ResetPlayerPosition());
        }

        private IEnumerator ResetPlayerPosition()
        {
            print("Reset Position Coroutine");
            vrNoPeeking.CameraFadeOut(vrNoPeeking.defaultFadeOutSpeed);
            
            yield return new WaitForSeconds(1);
            playerTransform.position = lastPlayerPosition;
            playerTransform.rotation = lastPlayerRotation;
            vrNoPeeking.CameraFadeIn();
            
            resetTriggered = false;
        } 

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.deepPink;
            Gizmos.DrawRay(transform.position, rayDirection.normalized * rayDistance);
        }
    }
}
    
