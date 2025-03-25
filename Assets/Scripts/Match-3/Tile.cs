using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int row;
    public int column;
    public Item item;
    public Image icon;
    public bool isMatched;
    private TileManager tileManager;
    private Coroutine hintCoroutine;

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
    }

    public void Select()
    {
        tileManager.SelectTile(this);
    }

    public void StartHintAnimation()
    {
        if (hintCoroutine == null)
        {
            hintCoroutine = StartCoroutine(HintAnimation());
        }
    }

    public void ClearHintAnimation()
    {
        if (hintCoroutine != null)
        {
            StopCoroutine(hintCoroutine);
            hintCoroutine = null;
        }
        GetComponent<Image>().color = Color.white;
    }

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

    public void HighlightSelect()
    {
        GetComponent<Image>().color = Color.red;
    }

    public void ResetHighlight()
    {
        GetComponent<Image>().color = Color.white;
    }
}
