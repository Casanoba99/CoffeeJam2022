using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanzarTaza : MonoBehaviour
{
    bool cambiar = false;

    [HideInInspector]
    public float timer = 0;
    [HideInInspector]
    public bool click = false;

    public Image fuerzaImg;
    public Image marcoImg;
    public Rigidbody2D rbTaza;
    public int factorFuerza;
    public float tiempoTotal;

    void Start()
    {
        fuerzaImg.fillAmount = 0;
    }

    void Update()
    {
        if (!click)
        {
            LlenarFillAmount();

            if (Input.GetMouseButtonDown(0))
            {
                click = true;
                GameManager.manager.SumarIntento();

                QuitarBarra();
                rbTaza.AddForce(Vector2.right * (fuerzaImg.fillAmount * factorFuerza), ForceMode2D.Impulse);
                rbTaza.GetComponent<Taza>().lanzada = true;
            }
        }
    }

    void LlenarFillAmount()
    {
        if (timer <= tiempoTotal && !cambiar)
        {
            timer += Time.deltaTime;
            fuerzaImg.fillAmount = timer / tiempoTotal;

            if (timer >= tiempoTotal) cambiar = true;
        }
        else
        {
            timer -= Time.deltaTime;
            fuerzaImg.fillAmount = timer / tiempoTotal;

            if (timer <= 0) cambiar = false;
        }
    }

    public void QuitarBarra()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        fuerzaImg.enabled = false;
        marcoImg.enabled = false;
    }

    public void PonerBarra()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        fuerzaImg.enabled = true;
        marcoImg.enabled = true;
    }
}
