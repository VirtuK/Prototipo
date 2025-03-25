using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Instance")]
    public static LevelManager instance;

    [Header("Level Settings")]
    public int widthInput;
    public int lengthInput;
    public int scoreMax;
    public int maxNumberOfMoves;
    public int customScore;

    // Garante que exista apenas uma instância do LevelManager
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
