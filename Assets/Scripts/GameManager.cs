using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager manager;
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(manager);
        }
        else Destroy(gameObject);
    }
    #endregion

    public Taza taza;

    [Header("Incrementos")]
    public LanzarTaza lanzar;
    public int incrFuerza;
    public Transform meta;
    public int incrDistancia;

    [Header("Stats")]
    public int intentos;
    public Text intentosTxt;
    public int aciertos;
    public Text aciertosTxt;

    public void SiguienteNivel()
    {
        aciertos++;
        aciertosTxt.text = "" + aciertos;
        lanzar.factorFuerza += incrFuerza;
        meta.position = new Vector2(meta.position.x + incrDistancia, meta.position.y);
    }

    public void SumarIntento()
    {
        intentos++;
        intentosTxt.text = "" + intentos;
    }

    public void ReiniciarIntento()
    {
        // Taza
        taza.lanzada = false;
        taza.transform.position = taza.initPos.position;

        // LanzarTaza
        lanzar.fuerzaImg.fillAmount = 0;
        lanzar.timer = 0;

        lanzar.click = false;
        lanzar.fuerzaImg.enabled = true;
        lanzar.marcoImg.enabled = true;

        for (int i = 0; i < lanzar.transform.childCount; i++)
        {
            lanzar.transform.GetChild(i).gameObject.SetActive(true);
        }

        // Meta
        meta.GetComponent<Ganar>().contar = false;
        meta.GetComponent<Ganar>().acierto = false;
    }
}
