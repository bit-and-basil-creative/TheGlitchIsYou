using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Vector3 startPosition;
    private Transform cam;

    [SerializeField] private float parallaxMultiplier = 0.5f;

    void Start()
    {
        startPosition = transform.position;
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        Vector3 camDelta = cam.position * parallaxMultiplier;
        transform.position = new Vector3(startPosition.x + camDelta.x, startPosition.y + camDelta.y, startPosition.z);
    }
}
