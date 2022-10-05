using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ganar : MonoBehaviour
{
    float timer = 0;
    [HideInInspector]
    public bool contar = false;
    [HideInInspector]
    public bool acierto = false;

    public Transform taza;

    private void Update()
    {
        if (contar)
        {
            timer += Time.deltaTime;
            if (timer >= 2 && !acierto)
            {
                acierto = true;
                GameManager.manager.SiguienteNivel();
                Debug.Log("Victoria");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            contar = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            contar = false;
            timer = 0;
        }
    }
}
