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
   
   
   public void NextFog()
   {
      _fogList[_fogIndex].SetActive(false);
      if (_fogIndex == _fogList.Count - 1) _fogIndex = 0;
      else _fogIndex++;
      _fogList[_fogIndex].SetActive(true);
   }
}
