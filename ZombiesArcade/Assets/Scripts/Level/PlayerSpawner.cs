using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Transform playerParent;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PlayerPathSO playerPath;

    public void SpawnPlayer()
    {
        GameObject player = GetPlayer();
        Transform spawnPosition = GetSpawnPosition(playerPath.path);

        player.transform.position = spawnPosition.transform.position;
        player.transform.parent = playerParent.transform;

        playerPath.path = null;
    }

    private GameObject GetPlayer()
    {
        GameObject playerInScene = GameObject.FindGameObjectWithTag("Player");

        if (playerInScene == null)
        {
            playerInScene = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
        
        return playerInScene;
    }

    private Transform GetSpawnPosition(LevelEntranceSO playerEntrance)
    {
        if (playerEntrance == null)
            return transform.GetChild(0).transform;
        
        var levelEntrances = FindObjectsOfType<LevelEntrance>();

        foreach (LevelEntrance levelEntrance in levelEntrances)
        {
            if (levelEntrance.Entrance == playerEntrance)
            {
                return levelEntrance.gameObject.transform;
            }
        }

        return transform.GetChild(0).transform;
    }
}
