using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int currentLevel { get; private set; }
    
    [SerializeField] private List<GameObject> levelPrefabs;
    [SerializeField] private Transform levelParent;
    [SerializeField] private GameObject playerPrefab;
    
    private GameObject currentLevelObject;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start() => LoadLevel(0);

    public void LoadLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        
        if (currentLevelObject != null)
            Destroy(currentLevelObject);
        
        currentLevelObject = Instantiate(levelPrefabs[currentLevel], levelParent);
    }

    public void NextLevel()
    {
        currentLevel++;
        LoadLevel(currentLevel);
    }

    public void ResetLevels()
    {
        currentLevel = 0;
        
        Destroy(currentLevelObject);
    }
}
