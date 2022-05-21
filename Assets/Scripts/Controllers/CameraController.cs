using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public Vector3 offset = new Vector3(0, -0.5f, 0.5f);
    public float pitch = 1.5f;
    private float currentZoom = 10;
    public float zoomZpeed = 1;
    public float minZoom = 5f, maxZoom = 15f;
    public float yawSpeed = 100f, currentYaw = 0f;


    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomZpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * pitch);

        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
