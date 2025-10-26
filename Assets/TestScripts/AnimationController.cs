using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    // 创建一个全局hash变量，避免每帧重复转换字符串
    int idleToWalkHash = Animator.StringToHash("idleToWalk");

    void Start()
    {
        anim = GetComponent<Animator>(); // 获取Animator
    }

    void Update()
    {
        // 如果按下方向键，就触发Idle→Walk过渡
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
