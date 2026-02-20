using UnityEngine;
using System.Collections.Generic;

public class FootsepsManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] float stepInterval = 0.4f;
    private Rigidbody rb;
    private float stepTimer;

    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        bool grounded = GameObject.Find("Player").GetComponent<CharacterController>().isGrounded;
        if (horizontalSpeed > 0.01f && grounded)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                PlayRandom();
                stepTimer = stepInterval;
            }
        }
    }

    void PlayRandom()
    {
        int index = Random.Range(0, clips.Length);

        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(clips[index]);
    }

}
