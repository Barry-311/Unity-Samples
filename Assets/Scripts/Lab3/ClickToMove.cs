// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public Camera cam;
    NavMeshAgent agent;

    void Start()
    {
        Cursor.visible = true;                    // 显示鼠标
        Cursor.lockState = CursorLockMode.None;   // 不锁定鼠标
    }

    void Awake() { agent = GetComponent<NavMeshAgent>(); }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200f))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
