using System;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float idleSpeed = 1.5f;
    [SerializeField]
    private float stopDistance = 2f;
    [SerializeField]

    private float detectionRange = 10f;
    private Vector3 playerDirection;
    private Vector3 idleDirection;
    private float idleTimer;

    private float originalY;

    void Start()
    {
        idleTimer = 0f;
        originalY = transform.position.y;
    }

    void Update()
    {
        playerDirection = new Vector3(player.position.x - transform.position.x, 0, player.position.z - transform.position.z);
        if(playerDirection.magnitude < detectionRange && playerDirection.magnitude > stopDistance)
            transform.position += playerDirection.normalized * speed * Time.deltaTime;
        else if (playerDirection.magnitude > detectionRange)
            Idle();

        transform.position = new Vector3(transform.position.x, originalY, transform.position.z);
    }

    private void Idle()
    {
        if (idleTimer <= 0)
        {
            if(UnityEngine.Random.value < 0.5f)
                idleDirection = Vector3.zero;
            else
                idleDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0f, UnityEngine.Random.Range(-1f, 1f)).normalized;

            idleTimer = UnityEngine.Random.Range(0.2f, 1f);
        }
        else
        {
            transform.position += idleDirection * idleSpeed * Time.deltaTime;
            idleTimer -= Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            idleDirection = new Vector3(collision.contacts[0].normal.x, 0, collision.contacts[0].normal.z).normalized;
            idleTimer = UnityEngine.Random.Range(0.2f, 1f);
            Debug.Log("Collided with enemy, changing direction to: " + idleDirection);
        }
    }

    public float GetDistanceToPlayer()
    {
        return playerDirection.magnitude;
    }
}
