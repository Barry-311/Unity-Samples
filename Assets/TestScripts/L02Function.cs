using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime);
    }

    void OnMouseDown()
    {
        transform.Translate(Vector3.right * 5f);

        // ���� SendMessage ���� ChangeColor ����
        TriggerChangeColor();
    }

    void OnCollisionEnter(Collision collision)
    {
        objectRenderer.material.color = Color.red;
        Debug.Log("Collision Happens: Collide with" + collision.gameObject.name);
    }

    // SendMessage
    public void ChangeColor()
    {
        objectRenderer.material.color = Color.green;
        Debug.Log("Change to green");
    }

    // Use SendMessage to call ChangeColor method
    void TriggerChangeColor()
    {
        SendMessage("ChangeColor");
    }
}
