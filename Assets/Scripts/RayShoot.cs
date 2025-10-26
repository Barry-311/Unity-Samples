using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;     // stores camera component

    // Start is called before the first frame update
    void Start()
    {
        // gets the GameObject's camera component
        cam = GetComponent<Camera>();

        // hide the mouse cursor at the centre of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 12;

        // centre of screen and caters for font size
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;

        // displays "*" in the crentre of screen
        GUI.Label(new Rect(posX, posY, size * 2, size * 2), "+");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左键0 右键1 中键2
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            Ray ray = cam.ScreenPointToRay(point);

            RaycastHit hit; // 存储射线碰撞的信息

            // 如果射线与物体发生碰撞
            if (Physics.Raycast(ray, out hit))
            {
                // 获取碰撞到的 GameObject
                GameObject hitObject = hit.transform.gameObject;

                // 检查该对象是否有 ChangeColour 组件
                ChangeColour target = hitObject.GetComponent<ChangeColour>();

                RaycastLine();

                // 如果对象有 ChangeColour 组件，调用 SetRandomColour 方法
                if (target != null)
                    target.SetRandomColour();
            }
        }
    }

    void RaycastLine()
    {
        // just for debug: draw the ray in the Scene view
        Debug.DrawRay(
            cam.transform.position,
            cam.transform.forward * 100,
            Color.red,
            1.0f);
    }
}
