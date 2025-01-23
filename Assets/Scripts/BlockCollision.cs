using UnityEngine;

public class BlockCollision : MonoBehaviour
{
    private Block block;
    public bool IsColliding { get; set; } = false;

    private void Start()
    {
        block = GetComponent<Block>();
    }

    public void FixBlock()
    {
        if (IsColliding) 
        { 
            Destroy(GetComponent<Rigidbody>());
            block.IsFixed = true;

            //FindObjectOfType<BlockSpawner>()?.SpawnNewBlock();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 이미 고정된 블록은 충돌 처리를 하지 않음
        if (block.IsFixed) return;

        if (other.gameObject.CompareTag("Block"))
        {
            Debug.Log("Now!");
            IsColliding = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (block.IsFixed) return;

        if (other.gameObject.CompareTag("Block"))
        {
            float currentBlockTop = transform.position.y + transform.localScale.y * 0.5f;
            float previousBlockTop = other.gameObject.transform.position.y + other.gameObject.transform.localScale.y * 0.5f;

            if (previousBlockTop < currentBlockTop) // 표면보다 아래로 내려가면 종료
            {
                Debug.Log("Now!");
            }
            else
            {
                Debug.Log("GameOver!");
                Destroy(gameObject);
            }
        }
    }
}
