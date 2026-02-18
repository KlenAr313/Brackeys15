using UnityEngine;

public class TurnToPlayer : MonoBehaviour
{
    public Transform player; // Drag the camera here in the editor

    // Update is called once per frame

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0; 
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
