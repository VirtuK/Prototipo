using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public IEnumerator SwapItems(Tile tile1, Tile tile2)
    {
        Item tempItem = tile1.item;
        tile1.item = tile2.item;
        tile2.item = tempItem;

        Sprite tempSprite = tile1.icon.sprite;
        tile1.icon.sprite = tile2.icon.sprite;
        tile2.icon.sprite = tempSprite;

        HintManager hintManager = GameObject.FindObjectOfType<HintManager>();
        hintManager.ResetTimer();
        
        RectTransform icon1Transform = tile1.icon.GetComponent<RectTransform>();
        RectTransform icon2Transform = tile2.icon.GetComponent<RectTransform>();

        Vector3 icon1StartPos = icon1Transform.anchoredPosition;
        Vector3 icon2StartPos = icon2Transform.anchoredPosition;

        float duration = 0.3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            icon1Transform.anchoredPosition = Vector3.Lerp(icon1StartPos, icon2StartPos, t);
            icon2Transform.anchoredPosition = Vector3.Lerp(icon2StartPos, icon1StartPos, t);
            yield return null;
        }
        icon1Transform.anchoredPosition = icon2StartPos;
        icon2Transform.anchoredPosition = icon1StartPos;
        icon1Transform.anchoredPosition = Vector3.zero;
        icon2Transform.anchoredPosition = Vector3.zero;
    }

    public IEnumerator AnimateNewTile(Tile tile)
    {
        Vector3 originalScale = tile.transform.localScale;
        tile.transform.localScale = Vector3.zero;
        float duration = 0.3f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            tile.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, t);
            yield return null;
        }
        tile.transform.localScale = originalScale;
    }
}
