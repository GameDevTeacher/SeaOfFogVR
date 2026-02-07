using UnityEngine;

public class BulletController : MonoBehaviour
{
   public float projectileDamage;
   public float projectileSpeed;

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Enemy")) return;
      Destroy(gameObject);
   }
}