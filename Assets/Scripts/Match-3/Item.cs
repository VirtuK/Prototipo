using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public int points;
}
