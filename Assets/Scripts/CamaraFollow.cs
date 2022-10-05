using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    Vector2 initPos = new Vector2(1.25f, 0);

    public Transform target;
    public Vector3 offset;
    public float suavizado;

    void Start()
    {
        //transform.position = initPos;
    }

    void FixedUpdate()
    {
        Vector3 camPos = offset + target.position;
        transform.position = Vector3.Lerp(transform.position, camPos, suavizado * Time.deltaTime);
    }
}
