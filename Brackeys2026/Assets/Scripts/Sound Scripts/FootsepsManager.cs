using UnityEngine;
using System.Collections.Generic;

public class FootsepsManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioClip[] waterClips;
    [SerializeField] float stepInterval = 0.4f;
    private Rigidbody rb;
    private float stepTimer;

    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    void Update()
    {
        stepTimer -= Time.deltaTime;

        float horizontalSpeed = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        //PlayerControllerScript.Instance.controller.velocity
        bool grounded = GameObject.Find("Player").GetComponent<CharacterController>().isGrounded;

        if (PlayerControllerScript.Instance.controller.velocity.magnitude > 4.5f && grounded)
        {

            if (stepTimer <= 0)
            {
                PlayRandom();
                stepTimer = stepInterval;
            }
        }
    }

    public void PlayRandom()
    {
        int index = Random.Range(0, clips.Length);
        if (PlayerControllerScript.Instance.GetInWater())
        {
            source.pitch = Random.Range(0.9f, 1.1f);
            source.PlayOneShot(waterClips[index]);
        }
        else
        {
            source.pitch = Random.Range(0.9f, 1.1f);
            source.PlayOneShot(clips[index]);
        }
    }

}
