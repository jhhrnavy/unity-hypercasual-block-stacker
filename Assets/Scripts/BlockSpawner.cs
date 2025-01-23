using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    public void SpawnBlock()
    {
        if (blockPrefab == null) return;

        Instantiate(blockPrefab, transform.position, Quaternion.identity);
    }
}
