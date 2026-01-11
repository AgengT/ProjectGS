using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;

    private void Awake()
    {
        if (spawnPos == null) Debug.LogError("SpawnPos not found in Level object.");
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = spawnPos.position;
            var playerScript = player.GetComponent<PlayerMovement>();
            if (playerScript != null) playerScript.PlayerReset();
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

    public void OnPlayerDeath()
    {
        SpawnPlayer();
    }
}
