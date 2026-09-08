using UnityEngine;
using TMPro;

public class TutorialTimer : MonoBehaviour
{
    [Header("Configuração")]
    public float tempoInicial = 30f;

    [Header("UI")]
    public TextMeshProUGUI textoTimer;

    private float tempoAtual;

    private bool timerAtivo = false;

    public bool TempoAcabou { get; private set; }

    void Start()
    {
        tempoAtual = tempoInicial;

        TempoAcabou = false;

        AtualizarUI();

        // IMPORTANTE:
        // O timer NÃO começa automaticamente.
        // O TutorialFugaManager vai iniciar.
    }

    void Update()
    {
        if (!timerAtivo)
            return;

        tempoAtual -= Time.deltaTime;

        if (tempoAtual <= 0f)
        {
            tempoAtual = 0f;

            timerAtivo = false;

            TempoAcabou = true;

            AtualizarUI();

            QuandoTempoAcabar();

            return;
        }

        AtualizarUI();
    }

    // =====================================================
    // INICIAR
    // =====================================================

    public void IniciarTimer()
    {
        tempoAtual = tempoInicial;

        TempoAcabou = false;

        timerAtivo = true;

        AtualizarUI();

        Debug.Log("⏱️ Intervalo começou!");
    }

    // =====================================================
    // PARAR
    // =====================================================

    public void PararTimer()
    {
        timerAtivo = false;
    }

    // =====================================================
    // UI
    // =====================================================

    void AtualizarUI()
    {
        if (textoTimer == null)
            return;

        int segundos = Mathf.CeilToInt(tempoAtual);

        textoTimer.text = segundos.ToString();
    }

    // =====================================================
    // TEMPO ACABOU
    // =====================================================

    void QuandoTempoAcabar()
    {
        Debug.Log("🚨 O CHEFE VOLTOU!");

        if (TutorialManager.instance != null)
        {
            TutorialManager.instance.IntervaloTerminou();
        }
    }
}