using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FogController : MonoBehaviour
{
   public static FogController instance;

   private void Awake()
   {
      if (instance == null)  instance = this;
      else if (instance != this) Destroy(gameObject);
      foreach (GameObject fog in _fogList)
      {
         if (fog != _fogList[_fogIndex]) fog.GetComponent<VisualEffect>().Stop();
      }
   }
   
   [SerializeField] private List<GameObject> _fogList;
   [SerializeField] private int _fogIndex;
   [SerializeField] private int _lastFog;
   [SerializeField] private bool _startWait;
   
   

   private void OnTriggerEnter(Collider other)
   {
      if ((other.gameObject.tag == "Player" || other.gameObject.layer == LayerMask.NameToLayer("Boat"))&& !_startWait)
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
         _fogList[_fogIndex].GetComponent<VisualEffect>().Play();
         yield return new WaitForSeconds(1f);
         _fogList[_lastFog].GetComponent<VisualEffect>().Stop();
         _startWait = false;
   }
}
