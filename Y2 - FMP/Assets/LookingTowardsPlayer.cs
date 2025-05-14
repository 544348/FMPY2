using UnityEngine;

public class LookingTowardsPlayer : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        gameObject.transform.LookAt(player.transform.position);
    }
}
