using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [Header("Tile Values")]
    public int row;
    public int column;
    public Item item;
    public Image icon;

    [Header("Match Settings")]
    public bool isMatched;

    [Header("References")]
    private TileManager tileManager;
    private Coroutine hintCoroutine;

    //Inicializa refer�ncias
    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    //Coloca essa pe�a como selecionada
    public void Select()
    {
        tileManager.SelectTile(this);
    }

    // Inicia a anima��o de dica 
    public void StartHintAnimation()
    {
        if (hintCoroutine == null)
        {
            hintCoroutine = StartCoroutine(HintAnimation());
        }
    }

    // Limpa a anima��o de dica, retornando a cor original
    public void ClearHintAnimation()
    {
        if (hintCoroutine != null)
        {
            StopCoroutine(hintCoroutine);
            hintCoroutine = null;
        }
        GetComponent<Image>().color = Color.white;
    }

    // Anima��o de dica que alterna entre duas cores (normal e destaque)
    private IEnumerator HintAnimation()
    {
        Color highlightColor = Color.yellow;
        Color normalColor = Color.white;
        float duration = 0.5f;

        while (true)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                GetComponent<Image>().color = Color.Lerp(normalColor, highlightColor, elapsed / duration);
                yield return null;
            }

            elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                GetComponent<Image>().color = Color.Lerp(highlightColor, normalColor, elapsed / duration);
                yield return null;
            }
        }
    }

    // Destaca a pe�a selecionada
    public void HighlightSelect()
    {
        GetComponent<Image>().color = Color.red;
    }

    // Restaura a cor original
    public void ResetHighlight()
    {
        GetComponent<Image>().color = Color.white;
    }
}