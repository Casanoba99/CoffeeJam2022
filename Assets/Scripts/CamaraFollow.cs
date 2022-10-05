using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    Vector2 initPos = new Vector2(1.25f, 0);

    public Transform target;
    public Vector3 seguirV3;

    void Start()
    {
        transform.position = initPos;
    }

    void Update()
    {
        seguirV3 = new Vector2(transform.position.x + target.position.x, transform.position.y);
        transform.position = seguirV3 + new Vector3(0, 0, -10);
    }
}
