using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public void SetRandomColour()
    {
        Color random = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        GetComponent<Renderer>().material.color = random;
    }
}
