using UnityEngine;

namespace Enemy
{
   public class EnemyController : MonoBehaviour
   {
      [Header("Health")]
      [SerializeField] private float _health = 100;

      [Header("View")] 
      [SerializeField] private float _viewCastRadius;
      [SerializeField] private float _viewRange;
      private Transform _target;
      private RaycastHit _hitInfo;

      [Header("Attack")] [Space(5)]
      [SerializeField] private GameObject _projectilePrefab;
      [SerializeField] private Transform _projectileSpawnPoint;
      [SerializeField] private float _projectileDamage;
      [SerializeField] private float _projectileSpeed;
      [SerializeField] private float _projectileRange;
      [SerializeField] private float _fireRate;
   
      private float _fireRateCounter;

      private void Start() => _target = GameObject.FindWithTag("Player").transform;

      private void Update()
      {
         UpdateTargetVisibility();
      }

      private void UpdateTargetVisibility()
      {
         if (_target == null) return;
         if (Vector3.Distance(_target.position, transform.position) > _viewRange) return;
         
         Physics.SphereCast(_projectileSpawnPoint.position, _viewCastRadius, 
            GetProjectileDirection(), out _hitInfo);

         if (_hitInfo.collider == null) return;
         if (!_hitInfo.collider.CompareTag("Player")) return;

         UpdateProjectile();
      }
   
      private void UpdateProjectile()
      {
         if (_fireRateCounter > Time.time) return;

         var projectileClone = Instantiate(_projectilePrefab, 
            _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
         
         Destroy(projectileClone, 5f);
         projectileClone.tag = "EnemyProjectile";

         #region GET COMPONENTS
         projectileClone.TryGetComponent(out Rigidbody rb); 
         rb.linearVelocity = GetProjectileDirection() * _projectileSpeed;

         projectileClone.TryGetComponent(out BulletController bulletController);
         bulletController.projectileDamage = _projectileDamage;
         #endregion
         
         _fireRateCounter = Time.time + _fireRate;
      }
   
      private Vector3 GetProjectileDirection()
      {
         return (_target.position - transform.position).normalized;
      }
   
      private void OnTriggerEnter(Collider other)
      {
         if (!other.CompareTag("Projectile")) return;
         
         other.TryGetComponent(out BulletController bulletController);
         TakeDamage(bulletController.projectileDamage);
         
         Destroy(other.gameObject);
      }
   
      public void TakeDamage(float damage)
      {
         _health -= damage;
         if (_health > 0) return;
         Destroy(gameObject);
      }
      
      private void OnDrawGizmos()
      {
         if (_target == null) return;
         
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(_hitInfo.point, _viewCastRadius);
         Gizmos.DrawRay(_projectileSpawnPoint.position, GetProjectileDirection() * _projectileRange);
      }
   }
}