using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour
{
   public static FogController instance;

   private void Awake()
   {
      if (instance == null)  instance = this;
      else Destroy(gameObject);
   }
   
   [SerializeField] private List<GameObject> _fogList;
   [SerializeField] private int _fogIndex;
   [SerializeField] private int _lastFog;
   [SerializeField] private bool _startWait;


   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Boat"))
      _startWait = true;
   }

   public void NextFog()
   {
      _lastFog = _fogIndex;
      if (_fogIndex == _fogList.Count - 1) _fogIndex = 0;
      else _fogIndex++;
      StartCoroutine(_nextFog());
   }
   
   
   private IEnumerator _nextFog()
   {
         _fogList[_fogIndex].SetActive(true);
         yield return new WaitForSeconds(1f);
         _fogList[_lastFog].SetActive(false);
         _startWait = false;
   }
}
