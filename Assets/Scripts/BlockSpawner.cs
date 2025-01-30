using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public int _blockCount = 0; // 생성된 블록 개수를 저장하는 변수
    public float _cubeBlockEdgeLength = 0; // 정육면체 블록 한 모서리의 길이

    [SerializeField] private GameObject cubeBlockPrefab;
    [SerializeField] private GameObject currentBlock; // 현재 움직이는 블록
    [SerializeField] private GameObject prevBlock; // 이전에 생성된 블록

    public GameObject CurrentBlock { get => currentBlock; }
    public GameObject PrevBlock { get => prevBlock; }

    public void SpawnNewBlock()
    {
        if (cubeBlockPrefab == null) return;

        if (_blockCount == 0) // 첫번째 블록 생성 시
        {
            GameObject cubeBlock = Instantiate(cubeBlockPrefab, transform.position, Quaternion.identity);
            prevBlock = cubeBlock;
            prevBlock.GetComponent<Collider>().isTrigger = false;
            prevBlock.name = "prevBlock";

            _cubeBlockEdgeLength = cubeBlock.transform.lossyScale.x; // 초기 정육면체 블록의 한 모서리의 길이
        }
        else // 두 번째 블록부터 생성 시
        {

            if (currentBlock != null) // 이미 생성된 블록이 있는 경우
            {
                // 이전 블록과 현재 블록의 Bounds를 가져옵니다.
                Bounds prevBounds = prevBlock.GetComponent<Collider>().bounds;
                Bounds currentBounds = currentBlock.GetComponent<Collider>().bounds;

                // 스폰될 블록의 한 모서리의 길이를 구합니다.
                _cubeBlockEdgeLength = CalculateCubeEdgeLength(currentBounds, prevBounds);

                // 겹치는 부분의 부피를 제외한 새로운 정육면체 큐브를 생성합니다.
                GameObject block = Instantiate(cubeBlockPrefab, transform.position, Quaternion.identity);
                block.transform.localScale = Vector3.one * _cubeBlockEdgeLength;

                prevBlock = currentBlock; // 현재 블록을 이전 블록으로 설정
                prevBlock.name = "prevBlock";
                block.AddComponent<BlockCollision>(); // 새 블록에 BlockCollision 컴포넌트 추가
                currentBlock = block; // 새 블록을 현재 블록으로 설정

            }
            else // 생성된 블록이 이전 블록 아래로 완전히 빠져나가 파괴된 경우
            {
                GameObject block = Instantiate(cubeBlockPrefab, transform.position, Quaternion.identity);
                block.transform.localScale = Vector3.one * _cubeBlockEdgeLength;

                block.name = "currentBlock";
                block.AddComponent<BlockCollision>(); // 새 블록에 BlockCollision 컴포넌트 추가
                currentBlock = block; // 새 블록을 현재 블록으로 설정
            }
        }

        _blockCount++;
    }

    public float CalculateCubeEdgeLength(Bounds currentBounds, Bounds prevBounds)
    {
        // 겹치는 부분의 부피를 계산합니다.
        float yOverlappingLength = prevBounds.max.y - currentBounds.min.y;
        float xzOverlappingLength = currentBounds.size.x;
        float overlappingAreaVolume = yOverlappingLength * xzOverlappingLength * xzOverlappingLength;

        // 기존에 생성되던 정육면체의 부피에서 겹치는 부분의 부피를 제외한 정육면체의 부피를 구합니다.
        float nonOverlappingAreaVolume = Mathf.Pow(xzOverlappingLength, 3) - overlappingAreaVolume;

        // nonOverlappingAreaVolume의 정육면체의 한 모서리의 길이
        float edgeLength = Mathf.Pow(nonOverlappingAreaVolume, 1f / 3f);

        //Debug.Log($"{Mathf.Pow(xzOverlappingLength, 3)}, {overlappingAreaVolume}, {nonOverlappingAreaVolume}, {edgeLength}");
        return edgeLength;
    }
}
