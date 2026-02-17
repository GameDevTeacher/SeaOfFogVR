using System;
using System.Collections;
using UnityEngine;

public class EchoSprites : MonoBehaviour
{
   [SerializeField] private GameObject _player;
   [SerializeField] private GameObject _echoSprite;
   [SerializeField] private GameObject _spriteObject;
   private bool _fadeOutTriggered = false;


   private void Awake()
   {
      _player = GameObject.FindWithTag("Player");
      _spriteObject = Instantiate(_echoSprite,transform.position, Quaternion.identity, gameObject.transform);
      var color = _spriteObject.GetComponentInChildren<MeshRenderer>().material.color;
      color.a = 0f;
      _spriteObject.GetComponentInChildren<MeshRenderer>().material.color = color;
   }

   private void Update()
   {
      if (_spriteObject.GetComponentInChildren<MeshRenderer>().material.color.a != 0)
      {
         _spriteObject.transform.LookAt(new Vector3(
            _player.transform.position.x,
            _spriteObject.transform.position.y,
            _player.transform.position.z
         ),transform.up);
      }
   }
   private void OnTriggerEnter(Collider other)
   {
      if (other.tag != "Player")  return;
      if (_fadeOutTriggered) return;
      
      StartCoroutine(FadeIn());
   }
   private IEnumerator FadeIn()
   {
      while (_spriteObject.GetComponentInChildren<MeshRenderer>().material.color.a < 1)
      {
         if (_fadeOutTriggered)
         {
            yield break;
         }
         _spriteObject.GetComponentInChildren<MeshRenderer>().material.color += new Color(0, 0, 0, 0.1f);
         yield return new WaitForSeconds(0.05f);
      }
   }
}
