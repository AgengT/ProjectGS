using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int currentLevel {get; private set;}

    [SerializeField] private List<GameObject> levelPrefabs;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int levelIndex)
    {
        currentLevel = levelIndex;
    }

    public void NextLevel()
    {
        currentLevel ++;
        LoadLevel(currentLevel);
    }
}
