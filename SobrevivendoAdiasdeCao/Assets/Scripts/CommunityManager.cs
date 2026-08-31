using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommunityManager : MonoBehaviour
{
    // =========================================================
    // SINGLETON
    // =========================================================

    public static CommunityManager instance;


    // =========================================================
    // STATUS DA COMUNIDADE
    // =========================================================

    [Header("Status da Comunidade")]

    [Range(0, 100)]
    public float alimentacao = 100f;

    [Range(0, 100)]
    public float saude = 100f;

    [Range(0, 100)]
    public float felicidade = 100f;


    // =========================================================
    // BARRAS
    // =========================================================

    [Header("Barras")]

    public Slider barraAlimentacao;
    public Slider barraSaude;
    public Slider barraFelicidade;


    // =========================================================
    // TEXTOS DOS PERCENTUAIS
    // =========================================================

    [Header("Percentuais")]

    public TextMeshProUGUI textoAlimentacao;
    public TextMeshProUGUI textoSaude;
    public TextMeshProUGUI textoFelicidade;


    // =========================================================
    // CONSUMO NATURAL
    // =========================================================

    [Header("Consumo Natural")]

    [Tooltip("Quanto a alimentação diminui por segundo.")]
    public float consumoAlimentacaoPorSegundo = 0.8f;


    // =========================================================
    // INFLUÊNCIA ENTRE NECESSIDADES
    // =========================================================

    [Header("Influência entre necessidades")]

    [Tooltip("Perda de saúde quando alimentação está em atenção.")]
    public float perdaSaudeFome = 1f;

    [Tooltip("Perda de saúde quando alimentação está crítica.")]
    public float perdaSaudeFomeCritica = 2f;

    [Tooltip("Perda de felicidade causada pela fome.")]
    public float perdaFelicidadeFome = 0.5f;

    [Tooltip("Perda de felicidade causada pela saúde baixa.")]
    public float perdaFelicidadeSaude = 0.5f;


    // =========================================================
    // AWAKE
    // =========================================================

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        ConfigurarBarras();
        AtualizarUI();
    }


    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {
        AtualizarComunidade();
        AtualizarUI();
    }


    // =========================================================
    // CONFIGURAÇÃO DAS BARRAS
    // =========================================================

    private void ConfigurarBarras()
    {
        if (barraAlimentacao != null)
        {
            barraAlimentacao.minValue = 0;
            barraAlimentacao.maxValue = 100;
        }

        if (barraSaude != null)
        {
            barraSaude.minValue = 0;
            barraSaude.maxValue = 100;
        }

        if (barraFelicidade != null)
        {
            barraFelicidade.minValue = 0;
            barraFelicidade.maxValue = 100;
        }
    }


    // =========================================================
    // LÓGICA DA COMUNIDADE
    // =========================================================

    private void AtualizarComunidade()
    {
        // -----------------------------------------------------
        // ALIMENTAÇÃO DIMINUI COM O TEMPO
        // -----------------------------------------------------

        alimentacao -= consumoAlimentacaoPorSegundo * Time.deltaTime;


        // -----------------------------------------------------
        // ALIMENTAÇÃO → SAÚDE
        // -----------------------------------------------------

        if (alimentacao < 40f)
        {
            saude -= perdaSaudeFomeCritica * Time.deltaTime;
        }
        else if (alimentacao < 70f)
        {
            saude -= perdaSaudeFome * Time.deltaTime;
        }


        // -----------------------------------------------------
        // ALIMENTAÇÃO → FELICIDADE
        // -----------------------------------------------------

        if (alimentacao < 70f)
        {
            felicidade -= perdaFelicidadeFome * Time.deltaTime;
        }


        // -----------------------------------------------------
        // SAÚDE → FELICIDADE
        // -----------------------------------------------------

        if (saude < 70f)
        {
            felicidade -= perdaFelicidadeSaude * Time.deltaTime;
        }


        // -----------------------------------------------------
        // LIMITES
        // -----------------------------------------------------

        alimentacao = Mathf.Clamp(alimentacao, 0f, 100f);
        saude = Mathf.Clamp(saude, 0f, 100f);
        felicidade = Mathf.Clamp(felicidade, 0f, 100f);
    }


    // =========================================================
    // RECURSOS DAS FASES
    // =========================================================

    public void AdicionarComida(float valor)
    {
        alimentacao += valor;
        alimentacao = Mathf.Clamp(alimentacao, 0f, 100f);

        AtualizarUI();
    }


    public void AdicionarRemedios(float valor)
    {
        saude += valor;
        saude = Mathf.Clamp(saude, 0f, 100f);

        AtualizarUI();
    }


    public void AdicionarDiversao(float valor)
    {
        felicidade += valor;
        felicidade = Mathf.Clamp(felicidade, 0f, 100f);

        AtualizarUI();
    }


    // =========================================================
    // ALTERAÇÕES CAUSADAS POR EVENTOS
    // =========================================================

    public void AlterarAlimentacao(float valor)
    {
        alimentacao += valor;
        alimentacao = Mathf.Clamp(alimentacao, 0f, 100f);

        AtualizarUI();
    }


    public void AlterarSaude(float valor)
    {
        saude += valor;
        saude = Mathf.Clamp(saude, 0f, 100f);

        AtualizarUI();
    }


    public void AlterarFelicidade(float valor)
    {
        felicidade += valor;
        felicidade = Mathf.Clamp(felicidade, 0f, 100f);

        AtualizarUI();
    }


    // =========================================================
    // PASSAGEM DE DIA
    // =========================================================

    public void FinalizarDia()
    {
        // Reservado para a lógica dos dias.
    }


    // =========================================================
    // ESTADOS — CRÍTICO
    // =========================================================

    public bool AlimentacaoCritica()
    {
        return alimentacao < 40f;
    }

    public bool SaudeCritica()
    {
        return saude < 40f;
    }

    public bool FelicidadeCritica()
    {
        return felicidade < 40f;
    }


    // =========================================================
    // ESTADOS — ATENÇÃO
    // =========================================================

    public bool AlimentacaoEmAtencao()
    {
        return alimentacao >= 40f && alimentacao < 70f;
    }

    public bool SaudeEmAtencao()
    {
        return saude >= 40f && saude < 70f;
    }

    public bool FelicidadeEmAtencao()
    {
        return felicidade >= 40f && felicidade < 70f;
    }


    // =========================================================
    // ESTADOS — ESTÁVEL
    // =========================================================

    public bool AlimentacaoEstavel()
    {
        return alimentacao >= 70f;
    }

    public bool SaudeEstavel()
    {
        return saude >= 70f;
    }

    public bool FelicidadeEstavel()
    {
        return felicidade >= 70f;
    }


    // =========================================================
    // CONSULTAS
    // =========================================================

    public bool ComunidadeComFome()
    {
        return alimentacao < 40f;
    }

    public bool ComunidadeDoente()
    {
        return saude < 40f;
    }

    public bool ComunidadeTriste()
    {
        return felicidade < 40f;
    }


    // =========================================================
    // MÉDIA DA COMUNIDADE
    // =========================================================

    public float MediaComunidade()
    {
        return (alimentacao + saude + felicidade) / 3f;
    }


    // =========================================================
    // ATUALIZAÇÃO DA UI
    // =========================================================

    private void AtualizarUI()
    {
        // -----------------------------------------------------
        // BARRA DE ALIMENTAÇÃO
        // -----------------------------------------------------

        if (barraAlimentacao != null)
        {
            barraAlimentacao.value = alimentacao;
        }


        // -----------------------------------------------------
        // BARRA DE SAÚDE
        // -----------------------------------------------------

        if (barraSaude != null)
        {
            barraSaude.value = saude;
        }


        // -----------------------------------------------------
        // BARRA DE FELICIDADE
        // -----------------------------------------------------

        if (barraFelicidade != null)
        {
            barraFelicidade.value = felicidade;
        }


        // -----------------------------------------------------
        // PERCENTUAIS
        // -----------------------------------------------------

        if (textoAlimentacao != null)
        {
            textoAlimentacao.text = Mathf.RoundToInt(alimentacao) + "%";
        }

        if (textoSaude != null)
        {
            textoSaude.text = Mathf.RoundToInt(saude) + "%";
        }

        if (textoFelicidade != null)
        {
            textoFelicidade.text = Mathf.RoundToInt(felicidade) + "%";
        }
    }
}