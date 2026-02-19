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
        if (true)
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
