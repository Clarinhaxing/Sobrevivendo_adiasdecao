using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EfeitoBotao : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    private Vector3 tamanhoOriginal;

    [SerializeField] private float escalaPressionado = 0.9f;

    private Image imagemBotao;
    private Color corOriginal;

    void Start()
    {
        tamanhoOriginal = transform.localScale;

        imagemBotao = GetComponent<Image>();

        if (imagemBotao != null)
        {
            corOriginal = imagemBotao.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (imagemBotao != null)
        {
            imagemBotao.color = corOriginal * 1.3f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (imagemBotao != null)
        {
            imagemBotao.color = corOriginal;
        }

        transform.localScale = tamanhoOriginal;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = tamanhoOriginal * escalaPressionado;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = tamanhoOriginal;
    }
}