using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    // ����һ��ȫ��hash����������ÿ֡�ظ�ת���ַ���
    int idleToWalkHash = Animator.StringToHash("idleToWalk");

    void Start()
    {
        anim = GetComponent<Animator>(); // ��ȡAnimator
    }

    void Update()
    {
        // ������·�������ʹ���Idle��Walk����
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool(idleToWalkHash, true);
        }
        else
        {
            anim.SetBool(idleToWalkHash, false);
        }
    }
}
