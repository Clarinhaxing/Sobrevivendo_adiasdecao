using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Itens")]
    public int itensColetados = 0;
    public int meta = 10;

    [Header("Tempo")]
    public float tempo = 60f;

    [Header("UI")]
    public TextMeshProUGUI textoItens;
    public TextMeshProUGUI textoTempo;

    public GameObject painelVitoria;
    public GameObject painelDerrota;

    [Header("Boss")]
    public GameObject carrocinha;

    public bool jogoAcabou = false;
    private bool bossLiberado = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        painelVitoria.SetActive(false);
        painelDerrota.SetActive(false);
        carrocinha.SetActive(false);

        AtualizarUI();
    }

    void Update()
    {
        if (jogoAcabou) return;

        if (!bossLiberado)
        {
            tempo -= Time.deltaTime;

            if (tempo <= 0)
            {
                tempo = 0;
                LiberarBoss();
            }

            textoTempo.text = "Tempo: " + Mathf.Ceil(tempo);
        }
    }

    public void Coletar(int valor)
    {
        if (jogoAcabou) return;

        itensColetados += valor;
        AtualizarUI();

        if (itensColetados >= meta)
        {
            Vitoria();
        }
    }

    void AtualizarUI()
    {
        textoItens.text = "Itens: " + itensColetados + "/" + meta;
    }

    void LiberarBoss()
    {
        bossLiberado = true;
        carrocinha.SetActive(true);

        textoTempo.text = "FUJA!";
    }

    public void Derrota()
    {
        jogoAcabou = true;
        painelDerrota.SetActive(true);
        Time.timeScale = 0f;
    }

    void Vitoria()
    {
        jogoAcabou = true;
        painelVitoria.SetActive(true);
        Time.timeScale = 0f;
    }
}