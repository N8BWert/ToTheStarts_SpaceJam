using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCam : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float pitch = 1f;
    public float yawSpeed = 20f;
    public float zoom = 10f;

    private float currentYaw = 0f;

    // Update is called once per frame
    void Update()
    {
        currentYaw -= yawSpeed * Time.deltaTime;
        transform.position = target.position - offset * zoom;
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentYaw);
    }
}
