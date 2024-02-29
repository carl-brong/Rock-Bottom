using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] float flickerIntensity = 0.2f;
    [SerializeField] float flickerPerSec = 3.0f;
    [SerializeField] float speedRand = 1.0f;

    private float time;
    private float startIntensity;
    private new Light2D light;
    // Start is called before the first frame update
    void Start()
    {
       light = GetComponent<Light2D>();
       startIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * (1 - Random.Range(-speedRand, speedRand)) * Mathf.PI;
        light.intensity = startIntensity + Mathf.Sin(time * flickerPerSec) * flickerIntensity;
    }
}
