using System;
using System.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteLight : MonoBehaviour
{
    private Material _material;
    private bool _fadeOutTriggered = false;
    public float fadeOutTime = 0.05f;
    [SerializeField] private SphereCollider _collider;
    private Light _light;
    private float _lightIntensityDefault;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _material.color = new Color(_material.color.r,_material.color.g,_material.color.b,0);
        _collider = GetComponent<SphereCollider>();
        _collider.enabled = false;
        _light = gameObject.GetComponentInChildren<Light>();
        _lightIntensityDefault = _light.intensity;
        _light.intensity = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_fadeOutTriggered) return;
        if (other.gameObject.CompareTag("Player"))
        {
            SpriteFadeOut();
        }
    }

    public void SpriteFadeIn()
    {
        _collider.enabled = true;
        StartCoroutine(FadeIn());
    }

    public void SpriteFadeOut()
    {
        _fadeOutTriggered  = true;
        StartCoroutine(FadeOut());
    }
    
    private IEnumerator FadeIn()
    {
        while (_material.color.a < 1)
        {
            if (_fadeOutTriggered)
            {
                yield break;
            }
            
            _light.intensity += _lightIntensityDefault/10;
            _material.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(fadeOutTime);
        }
    }

    private IEnumerator FadeOut()
    {
        while (_material.color.a > 0.1f)
        {
            _material.color -= new Color(0, 0, 0, 0.1f);
            _light.intensity -= _lightIntensityDefault/10;
            yield return new WaitForSeconds(fadeOutTime);
        }
        gameObject.SetActive(false);
        SpriteLightManager.instance.UpdateSpriteScript();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _collider.radius/2);
    }
}
