using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Start()
    {
        Player player = GameObject.Find("Sphere1").GetComponent<Player>();

        if (player != null)
        {
            player.health -= 10; 
            Debug.Log("Player's health after damage: " + player.health); 
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }
}
