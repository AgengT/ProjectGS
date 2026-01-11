using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;
    // [SerializeField] private GameObject player;
    private void Awake()
    {
        if (spawnPos == null)
        {
            Debug.LogError("SpawnPos not found in Level object.");
        }
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null && spawnPos != null)
        {
            player.transform.position = spawnPos.position;
        }
        else
        {
            Debug.LogError("Player prefab or SpawnPos is not assigned.");
        }
    }

    public void LevelFinish()
    {
        LevelManager.Instance.NextLevel();
    }
}
