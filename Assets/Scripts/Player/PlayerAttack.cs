using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        #region PROJECTILE
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _projectileDamage;
        [SerializeField] private float _hitScanRange;
        #endregion
    
        #region SHOOTING
        [SerializeField] private bool _usingProjectile;
        [SerializeField] private float _currentAmmo;
        [SerializeField] private float _maxAmmo;
        [SerializeField] private float _fireRate = 0.3f;
        private float _fireRateCounter;
        #endregion
    
        [SerializeField] private Camera _camera;

        public void UpdateShooting(bool shoot)
        {
            if (!shoot || !(_currentAmmo > 0) || !(_fireRateCounter < Time.time)) return;
        
            if (_usingProjectile)
            {
                UpdateProjectile();
            }
            else
            {
                UpdateHitScan();
            }
            _currentAmmo--;
            _fireRateCounter = Time.time + _fireRate;
        }

        private void UpdateHitScan()
        {
            RaycastHit hit; // Variable to Store Raycast Hit info
            Physics.Raycast(
                _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)),
                out hit, _hitScanRange);
         
            if (hit.collider == null) return;
            if (!hit.collider.CompareTag("Enemy")) return;
         
            hit.collider.TryGetComponent(out EnemyController enemy);
            enemy.TakeDamage(_projectileDamage);
        }

        private void UpdateProjectile()
        {
            var projectileClone = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation);
            Destroy(projectileClone, 5f);
            
            projectileClone.TryGetComponent(out Rigidbody rigidBody);
            rigidBody.linearVelocity = GetMoveDirection() * _projectileSpeed;
            
            
            projectileClone.TryGetComponent(out BulletController bulletController);
            bulletController.projectileDamage = _projectileDamage;
        }

        private Vector3 GetMoveDirection()
        {
            float x = Screen.width / 2;
            float y = Screen.height / 2;

            var ray = _camera.ScreenPointToRay(new Vector2(x, y));
            return ray.direction;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Ray ray = _camera.ViewportPointToRay(
                new Vector3(0.5f, 0.5f, 0));
        
            Gizmos.DrawRay(
                _projectileSpawnPoint.position, 
                ray.direction * _hitScanRange);
        }
    }
}