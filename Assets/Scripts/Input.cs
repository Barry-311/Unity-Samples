using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] // enforces dependency on character controller
[AddComponentMenu("Control Script/FPS Input")]  // add to the Unity editor's component menu
public class FPSInput : MonoBehaviour
{
    // movement sensitivity
    public float speed = 6.0f;

    private float verticalSpeed = 0f;      // 当前垂直速度
    public float jumpForce = 8.0f;         // 跳跃初速度
    private int jumpCount = 0;             // 已跳次数（0/1/2）
    public int maxJumps = 2;               // 最大跳跃次数（双跳=2）

    // gravity setting
    public float gravity = -9.8f;

    public float sensitivityHor = 9.0f; // 鼠标水平灵敏度（Yaw）

    // reference to the character controller
    private CharacterController charController;

    public Transform cameraTransform; // 拖入主相机

    // Start is called before the first frame update
    void Start()
    {
        // get the character controller component
        charController = GetComponent<CharacterController>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // 自动找主相机
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * sensitivityHor;
        transform.Rotate(0f, mx, 0f); // 只转 Y（水平）

        // changes based on WASD keys
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // make diagonal movement consistent
        movement = Vector3.ClampMagnitude(movement, speed);

        // --- 跳跃逻辑 ---
        if (charController.isGrounded)
        {
            // 着地时重置跳跃计数与垂直速度
            jumpCount = 0;
            verticalSpeed = 0f;

            if (Input.GetButtonDown("Jump")) // 默认空格键
            {
                verticalSpeed = jumpForce;
                jumpCount++;
            }
        }
        else
        {
            // 空中再次按下空格且未超过最大跳跃次数
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                verticalSpeed = jumpForce;
                jumpCount++;
            }
        }

        // 应用重力
        verticalSpeed += gravity * Time.deltaTime;

        // add gravity in the vertical direction
        movement.y = verticalSpeed; // 使用跳跃系统计算后的垂直速度

        // ensure movement is independent of the framerate
        movement *= Time.deltaTime;

        // transform from local space to global space
        movement = transform.TransformDirection(movement);

        // pass the movement to the character controller
        charController.Move(movement);
    }
}
