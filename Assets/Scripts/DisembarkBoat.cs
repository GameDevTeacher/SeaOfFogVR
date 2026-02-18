using UnityEngine;
using System.Collections;

public class DisembarkBoat : MonoBehaviour
{
   [SerializeField] private LayerMask _disembarkBoatLayer;
   [SerializeField] private float _radius;

   
   private void UpdateDisembark()
   {
     Collider[] hit =  Physics.OverlapSphere(transform.position, _radius, LayerMask.GetMask("Default"));

     foreach (Collider col in hit)
     {
         if (col.gameObject.CompareTag("disembark"))
         {
             
         }
     }
     
   }
}
