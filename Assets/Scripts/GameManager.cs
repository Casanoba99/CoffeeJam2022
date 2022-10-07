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

    public enum Estados { Menu, InGame }

    public Estados estado;

    bool musica = true;
    int ajustesPanel = 0;

    public Taza taza;

    [Header("Menu")]
    public GameObject title;
    public GameObject empezarB;
    public Image musicaI;
    public Sprite[] musicaS;
    public AudioSource source;
    public GameObject ajustesB;
    public GameObject ajustes;
    public GameObject[] ajustesP;

    [Header("Incrementos")]
    public LanzarTaza lanzar;
    public float incrFuerza;
    public Transform rango;
    public Transform meta;
    public int incrDistancia;

    [Header("Stats")]
    public Text intentosTxt;
    public Text aciertosTxt;
    public int intentos;
    public int aciertos;

    [Header("Parallax")]
    public SpriteRenderer back1;
    public SpriteRenderer back2;
    public SpriteRenderer back3;
    public int nAciertos = 5;
    public float incrParallax;

    private void Start()
    {
        CambiarEstado(Estados.Menu);
    }

    private void Update()
    {
        // Volver al menu
        if (Input.GetKeyDown(KeyCode.P))
        {
            CambiarEstado(Estados.Menu);
        }
    }

    public void CambiarEstado(Estados est)
    {
        estado = est;

        switch (estado)
        {
            case Estados.Menu:
                // Poner Menu
                title.SetActive(true);
                musicaI.enabled = true;
                empezarB.SetActive(true);
                ajustesB.SetActive(true);
                CerrarAjustes();
                // Textos
                intentosTxt.gameObject.SetActive(false);
                aciertosTxt.gameObject.SetActive(false);
                // Plato
                rango.position = new Vector3(3, -.64f, 0);
                meta.localPosition = new Vector3(2, 0 ,0);
                // Estado inicial
                ReiniciarIntento();
                lanzar.QuitarBarra();
                lanzar.enabled = false;

                break;
            case Estados.InGame:
                // Quitar Menu
                title.SetActive(false);
                musicaI.enabled = false;
                empezarB.SetActive(false);
                ajustesB.SetActive(false);
                // Textos
                intentos = PlayerPrefs.GetInt("intentos");
                intentosTxt.text = "" + intentos;
                intentosTxt.gameObject.SetActive(true);

                aciertos = PlayerPrefs.GetInt("aciertos");
                aciertosTxt.text = "" + aciertos;
                aciertosTxt.gameObject.SetActive(true);
                // Plato
                if (aciertos > 0) SetPosicionPlato();
                // Lanzar Taza
                lanzar.enabled = true;
                lanzar.PonerBarra();

                break;
        }
    }
    #region Menu
    public void Empezar()
    {
        CambiarEstado(Estados.InGame);
    }

    public void SwitchMusica()
    {
        musica = !musica;
        if (musica == true)
        {
            source.mute = false;
            musicaI.sprite = musicaS[0];
        }
        else
        {
            source.mute = true;
            musicaI.sprite = musicaS[1];
        }
    }

    public void AbrirAjustes()
    {
        ajustes.SetActive(true);
    }

    public void CerrarAjustes()
    {
        ajustes.SetActive(false);
    }

    public void FlechaD()
    {
        ajustesP[ajustesPanel].SetActive(false);
        ajustesPanel++;

        if (ajustesPanel > ajustesP.Length - 1) ajustesPanel = 0;
        ajustesP[ajustesPanel].SetActive(true);
    }

    public void FlechaI()
    {
        ajustesP[ajustesPanel].SetActive(false);
        ajustesPanel--;

        if (ajustesPanel < 0) ajustesPanel = ajustesP.Length - 1;
        ajustesP[ajustesPanel].SetActive(true);
    }
    #endregion
    #region In Game
    public void SiguienteNivel()
    {
        aciertos++;
        PlayerPrefs.SetInt("aciertos", aciertos);
        aciertosTxt.text = "" + aciertos;

        lanzar.factorFuerza += incrFuerza;
        PlayerPrefs.SetFloat("fuerza", lanzar.factorFuerza);

        ReiniciarIntento();

        rango.position += new Vector3(incrDistancia, 0, 0);

        float metaNewX = Random.Range(rango.GetChild(1).position.x, rango.GetChild(2).position.x);
        PlayerPrefs.SetFloat("metaX", metaNewX);
        meta.position = new Vector3(metaNewX, -.64f, 0);

        if (aciertos % nAciertos == 0)
        {
            Vector2 parallax = new Vector2(incrParallax, 0);
            back1.size += parallax;
            back2.size += parallax;
            back3.size += parallax;

            Vector2 box = new Vector2(incrParallax, 0);
            back3.GetComponent<BoxCollider2D>().size += box;
        }

        PlayerPrefs.Save();
    }

    public void SumarIntento()
    {
        intentos++;
        PlayerPrefs.SetInt("intentos", intentos);
        intentosTxt.text = "" + intentos;
        PlayerPrefs.Save();
    }

    void SetPosicionPlato()
    {
        for (int i = 0; i < aciertos; i++)
        {
            rango.position += new Vector3(incrDistancia, 0, 0);
        }

        meta.position = new Vector3(PlayerPrefs.GetFloat("metaX"), -.64f, 0);
    }
    #endregion
    public void ReiniciarIntento()
    {
        // Taza
        taza.lanzada = false;
        taza.transform.position = taza.initPos.position;
        taza.rb.velocity = Vector2.zero;

        // LanzarTaza
        lanzar.fuerzaImg.fillAmount = 0;
        lanzar.timer = 0;

        lanzar.click = false;
        if (estado == Estados.InGame) lanzar.PonerBarra();

        // Meta
        meta.GetComponent<Ganar>().contar = false;
        meta.GetComponent<Ganar>().acierto = false;
    }
}
