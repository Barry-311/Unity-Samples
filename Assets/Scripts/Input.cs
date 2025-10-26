using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))] // enforces dependency on character controller
[AddComponentMenu("Control Script/FPS Input")]  // add to the Unity editor's component menu
public class FPSInput : MonoBehaviour
{
    // movement sensitivity
    public float speed = 6.0f;

    private float verticalSpeed = 0f;      // ��ǰ��ֱ�ٶ�
    public float jumpForce = 8.0f;         // ��Ծ���ٶ�
    private int jumpCount = 0;             // ����������0/1/2��
    public int maxJumps = 2;               // �����Ծ������˫��=2��

    // gravity setting
    public float gravity = -9.8f;

    public float sensitivityHor = 9.0f; // ���ˮƽ�����ȣ�Yaw��

    // reference to the character controller
    private CharacterController charController;

    public Transform cameraTransform; // ���������

    // Start is called before the first frame update
    void Start()
    {
        // get the character controller component
        charController = GetComponent<CharacterController>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // �Զ��������
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * sensitivityHor;
        transform.Rotate(0f, mx, 0f); // ֻת Y��ˮƽ��

        // changes based on WASD keys
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // make diagonal movement consistent
        movement = Vector3.ClampMagnitude(movement, speed);

        // --- ��Ծ�߼� ---
        if (charController.isGrounded)
        {
            // �ŵ�ʱ������Ծ�����봹ֱ�ٶ�
            jumpCount = 0;
            verticalSpeed = 0f;

            if (Input.GetButtonDown("Jump")) // Ĭ�Ͽո��
            {
                verticalSpeed = jumpForce;
                jumpCount++;
            }
        }
        else
        {
            // �����ٴΰ��¿ո���δ���������Ծ����
            if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
            {
                verticalSpeed = jumpForce;
                jumpCount++;
            }
        }

        // Ӧ������
        verticalSpeed += gravity * Time.deltaTime;

        // add gravity in the vertical direction
        movement.y = verticalSpeed; // ʹ����Ծϵͳ�����Ĵ�ֱ�ٶ�

        // ensure movement is independent of the framerate
        movement *= Time.deltaTime;

        // transform from local space to global space
        movement = transform.TransformDirection(movement);

        // pass the movement to the character controller
        charController.Move(movement);
    }
}
