using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Taza : MonoBehaviour
{
    float timer;

    public Transform initPos;
    public Rigidbody2D rb;
    public bool lanzada = false;
    public bool meta = false;

    void Start()
    {
        transform.position = initPos.position;
    }

    private void FixedUpdate()
    {
        if (lanzada && rb.velocity == Vector2.zero)
        {
            timer += Time.deltaTime;
            
            if (timer >= 2 && !meta)
            {
                GameManager.manager.ReiniciarIntento();
            }
        }

        if (transform.position.y < -1.5f) GameManager.manager.ReiniciarIntento();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meta"))
        {
            meta = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Meta"))
        {
            meta = false;
        }
    }
}
