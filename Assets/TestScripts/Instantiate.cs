using UnityEngine;

public class InstantiateAndDestroy : MonoBehaviour
{
    public GameObject prefab;


    void Start()
    {
        // Instantiate the prefab at position (0,0,0) with no rotation
        GameObject newObject = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 45, 0));

        // Destroy after 3seconds
        Destroy(newObject, 3f);
    }
}
