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
        // �̹� ������ ����� �浹 ó���� ���� ����
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

            if (previousBlockTop < currentBlockTop) // ǥ�麸�� �Ʒ��� �������� ����
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
