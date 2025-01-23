using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    void Update()
    {
        // UI 요소가 아닌 게임 오브젝트에 대한 터치만 처리
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
