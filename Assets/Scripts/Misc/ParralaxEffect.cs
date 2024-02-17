using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    Vector2 startPos;
    float startZ;
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startPos;
    float ZdistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (ZdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(ZdistanceFromTarget) / clippingPlane;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startZ = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = startPos + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startZ);
    }
}
