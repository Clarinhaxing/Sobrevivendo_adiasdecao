using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    // =====================================================
    // ETAPAS DO TUTORIAL
    // =====================================================

    public enum EtapaTutorial
    {
        Introducao,
        Corrida,
        Pulo,
        Latido,
        Coleta,
        Finalizado
    }

    [Header("Estado")]
    public EtapaTutorial etapaAtual = EtapaTutorial.Introducao;

    // =====================================================
    // UI
    // =====================================================

    [Header("UI do Tutorial")]
    public GameObject painelDialogo;
    public TextMeshProUGUI textoDuke;

    public GameObject tituloFase;
    public TextMeshProUGUI textoTitulo;

    public GameObject objetivo;
    public TextMeshProUGUI textoObjetivo;

    // =====================================================
    // TIMER
    // =====================================================

    [Header("Timer")]
    public TutorialTimer tutorialTimer;

    [Header("Chefe")]
    public ChefeCarrocinhaTutorial chefe;

    // =====================================================
    // CONFIGURAÇÃO
    // =====================================================

    [Header("Configuração")]
    public float tempoEntreFalase = 2f;

    private bool tutorialIniciado = false;

    // =====================================================
    // DIALOGOS DE DUKE
    // =====================================================

    [Header("Falas de Duke")]

    [TextArea(2, 5)]
    public string[] falasDuke =
    {
        "As coisas não estão boas para nós...",

        "Precisamos fugir deste lugar!",

        "Sandy, você conseguiu sair da sua gaiola, ajude os outros a saírem também!",

        "O chefe da carrocinha está no horário de intervalo agora, é a nossa chance!",

        "Mas o intervalo não vai durar para sempre. Quando ele voltar, vai começar a ronda pelas gaiolas.",

        "Precisamos libertar todos antes que ele termine a ronda!",

        "Você precisa coletar as 9 chaves das gaiolas que estão com ele! Rápido, estamos quase sem tempo!"
    };

    // =====================================================
    // AWAKE
    // =====================================================

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    // =====================================================
    // START
    // =====================================================

    void Start()
    {
        etapaAtual = EtapaTutorial.Introducao;

        if (objetivo != null)
            objetivo.SetActive(false);

        if (painelDialogo != null)
            painelDialogo.SetActive(false);

        if (tituloFase != null)
            tituloFase.SetActive(true);

        if (textoTitulo != null)
            textoTitulo.text = "A FUGA";

        StartCoroutine(IniciarTutorial());
    }

    // =====================================================
    // INÍCIO
    // =====================================================

    IEnumerator IniciarTutorial()
    {
        yield return new WaitForSeconds(1f);

        if (tituloFase != null)
            tituloFase.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(MostrarDialogos());

        ComecarCorrida();
    }

    // =====================================================
    // FALAS DE DUKE
    // =====================================================

    IEnumerator MostrarDialogos()
    {
        if (painelDialogo != null)
            painelDialogo.SetActive(true);

        for (int i = 0; i < falasDuke.Length; i++)
        {
            if (textoDuke != null)
                textoDuke.text = falasDuke[i];

            yield return new WaitForSeconds(tempoEntreFalase);
        }

        if (painelDialogo != null)
            painelDialogo.SetActive(false);
    }

    // =====================================================
    // CORRIDA
    // =====================================================

    void ComecarCorrida()
    {
        etapaAtual = EtapaTutorial.Corrida;

        MostrarObjetivo("Use as setas direcionais para andar!");
    }

    public void RegistrarCorrida()
    {
        if (etapaAtual != EtapaTutorial.Corrida)
            return;

        Debug.Log("Corrida concluída!");

        ComecarPulo();
    }

    // =====================================================
    // PULO
    // =====================================================

    void ComecarPulo()
    {
        etapaAtual = EtapaTutorial.Pulo;

        MostrarObjetivo("Use ESPAÇO para pular e fugir dos golpes!");
    }

    public void RegistrarPulo()
    {
        if (etapaAtual != EtapaTutorial.Pulo)
            return;

        Debug.Log("Pulo concluído!");

        ComecarLatido();
    }

    // =====================================================
    // LATIDO
    // =====================================================

    void ComecarLatido()
    {
        etapaAtual = EtapaTutorial.Latido;

        MostrarObjetivo("Use Z para latir e assustar o chefe!");
    }

    public void RegistrarLatido()
    {
        if (etapaAtual != EtapaTutorial.Latido)
            return;

        Debug.Log("Latido concluído!");

        ComecarColeta();
    }

    // =====================================================
    // COLETA / INÍCIO DO INTERVALO
    // =====================================================

    void ComecarColeta()
    {
        etapaAtual = EtapaTutorial.Coleta;

        MostrarObjetivo("Colete as chaves e nos liberte daqui!");

        Debug.Log("Tutorial das mecânicas concluído!");

        // Inicia o timer do intervalo
        if (tutorialTimer != null)
        {
            tutorialTimer.IniciarTimer();
        }
        else
        {
            Debug.LogWarning("TutorialTimer1 não foi configurado no Inspector!");
        }
    }

    // =====================================================
    // INTERVALO TERMINOU
    // =====================================================

    public void IntervaloTerminou()
    {
        Debug.Log("O CHEFE VOLTOU!");

        MostrarObjetivo("O chefe voltou!");

        if (chefe != null)
        {
            chefe.LiberarChefe();
        }
        else
        {
            Debug.LogWarning(
                "O Chefe Carrocinha não foi configurado no TutorialManager!"
            );
        }
    }
    // =====================================================
    // FINAL
    // =====================================================

    public void FinalizarTutorial()
    {
        etapaAtual = EtapaTutorial.Finalizado;

        if (objetivo != null)
            objetivo.SetActive(false);

        if (painelDialogo != null)
            painelDialogo.SetActive(true);

        if (textoDuke != null)
        {
            textoDuke.text =
                "Conseguimos! Vamos libertar todos!";
        }

        Debug.Log("Tutorial concluído!");
    }

    // =====================================================
    // MOSTRAR OBJETIVO
    // =====================================================

    void MostrarObjetivo(string mensagem)
    {
        if (objetivo != null)
            objetivo.SetActive(true);

        if (textoObjetivo != null)
            textoObjetivo.text = mensagem;
    }

    // =====================================================
    // VERIFICAR ETAPA
    // =====================================================

    public bool EstaNaEtapa(EtapaTutorial etapa)
    {
        return etapaAtual == etapa;
    }
}