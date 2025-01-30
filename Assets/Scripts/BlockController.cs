using UnityEngine;

public class BlockController : MonoBehaviour
{
    [SerializeField]
    private BlockSpawner _blockSpawner;

    private void OnEnable()
    {
        InputEvents.OnTouch += FixCurrentBlock;
    }

    private void OnDisable()
    {
        InputEvents.OnTouch += FixCurrentBlock;

    }

    private void FixCurrentBlock()
    {
        GameObject currentBlock = _blockSpawner.CurrentBlock;

        if (currentBlock == null) return;

        currentBlock.GetComponent<BlockCollision>()?.FixBlock();
    }
}
