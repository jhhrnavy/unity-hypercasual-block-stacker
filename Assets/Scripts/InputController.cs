using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    void Update()
    {
        // UI ��Ұ� �ƴ� ���� ������Ʈ�� ���� ��ġ�� ó��
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Screen");
                FindObjectOfType<BlockSpawner>().CurrentBlock.GetComponent<BlockCollision>()?.FixBlock();
            }
        }
    }
}
