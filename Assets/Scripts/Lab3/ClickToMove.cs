// ClickToMove.cs
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public Camera cam;
    NavMeshAgent agent;

    void Start()
    {
        Cursor.visible = true;                    // ��ʾ���
        Cursor.lockState = CursorLockMode.None;   // ���������
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
