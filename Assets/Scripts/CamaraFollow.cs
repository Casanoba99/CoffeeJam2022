using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float suavizado;

    void FixedUpdate()
    {
        Vector3 camPos = offset + target.position;
        transform.position = Vector3.Lerp(transform.position, camPos, suavizado * Time.deltaTime);
    }
}
