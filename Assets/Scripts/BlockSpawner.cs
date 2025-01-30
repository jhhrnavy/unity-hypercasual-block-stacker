using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public int _blockCount = 0; // 생성된 블록 개수를 저장하는 변수
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject currentBlock; // 현재 움직이는 블록
    [SerializeField] private GameObject prevBlock; // 이전에 생성된 블록

    public GameObject CurrentBlock { get => currentBlock; }
    public GameObject PrevBlock { get => prevBlock; }

    public void SpawnNewBlock()
    {
        if (blockPrefab == null) return;

        GameObject block = Instantiate(blockPrefab, transform.position, Quaternion.identity);
        block.name = "currentBlock";

        if (_blockCount == 0) // 첫번째 블록 생성 시
        {
            prevBlock = block;
            prevBlock.GetComponent<Collider>().isTrigger = false;
            prevBlock.name = "prevBlock";
        }
        else // 두 번째 블록부터 생성 시
        {
            if(currentBlock != null) // 이미 생성된 블록이 있는 경우
            {
                prevBlock = currentBlock; // 현재 블록을 이전 블록으로 설정
                prevBlock.name = "prevBlock";
            }

            block.AddComponent<BlockCollision>(); // 새 블록에 BlockCollision 컴포넌트 추가
            currentBlock = block; // 새 블록을 현재 블록으로 설정
        }

        _blockCount++;
    }
}
