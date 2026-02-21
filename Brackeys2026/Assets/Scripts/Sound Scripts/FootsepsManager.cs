using UnityEngine;
using System.Collections.Generic;

public class FootsepsManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] float stepInterval = 0.4f;
    private float stepTimer;
    
    void Update()
    {
        stepTimer -= Time.deltaTime;

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

        source.pitch = Random.Range(0.9f, 1.1f);
        source.PlayOneShot(clips[index]);
    }

}
