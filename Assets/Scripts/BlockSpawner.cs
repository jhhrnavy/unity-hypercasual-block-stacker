using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject currentBlock;
    [SerializeField] private GameObject prevBlock;

    public GameObject CurrentBlock { get => currentBlock; }
    public GameObject PrevBlock { get => prevBlock; }

    public void SpawnNewBlock()
    {
        if (blockPrefab == null) return;

        GameObject block = Instantiate(blockPrefab, transform.position, Quaternion.identity);
        block.name = "currentBlock";
        
        if(currentBlock == null)
        {
            currentBlock = block;
            currentBlock.GetComponent<Collider>().isTrigger = false;
        }
        else
        {
            currentBlock.name = "prevBlock";
            prevBlock = currentBlock;
            block.AddComponent<BlockCollision>();
            currentBlock = block;
        }
    }
}
