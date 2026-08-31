using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public enum Etapa
    {
        Introducao,
        Corrida,
        Pulo,
        Latido,
        Coleta,
        Fuga,
        Final
    }

    public Etapa etapaAtual = Etapa.Introducao;

    private void Start()
    {
        IniciarTutorial();
    }

    void IniciarTutorial()
    {
        etapaAtual = Etapa.Corrida;

        Debug.Log("Tutorial iniciado!");
        Debug.Log("Etapa: CORRIDA");
    }

    public void CompletarCorrida()
    {
        if (etapaAtual != Etapa.Corrida)
            return;

        etapaAtual = Etapa.Pulo;

        Debug.Log("Etapa: PULO");
    }

    public void CompletarPulo()
    {
        if (etapaAtual != Etapa.Pulo)
            return;

        etapaAtual = Etapa.Latido;

        Debug.Log("Etapa: LATIDO");
    }

    public void CompletarLatido()
    {
        if (etapaAtual != Etapa.Latido)
            return;

        etapaAtual = Etapa.Coleta;

        Debug.Log("Etapa: COLETA");
    }

    public void CompletarColeta()
    {
        if (etapaAtual != Etapa.Coleta)
            return;

        etapaAtual = Etapa.Fuga;

        Debug.Log("Etapa: FUGA");

        IniciarFuga();
    }

    void IniciarFuga()
    {
        Debug.Log("A CARROCINHA APARECEU!");
    }

    public void CompletarFuga()
    {
        if (etapaAtual != Etapa.Fuga)
            return;

        etapaAtual = Etapa.Final;

        Debug.Log("FUGA CONCLUÍDA!");
    }
}