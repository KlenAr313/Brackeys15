using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{

    [SerializeField] public List<Sprite> animations;
    [SerializeField] private String animationName; 
    [SerializeField] private float fps;
    [SerializeField] private bool isLooping;

    private float timer;
    private int currentFrame = 0;
    private SpriteRenderer parentSpriteRenderer;
    private Sprite originalSprite;
    private bool isPlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 1/fps;
        parentSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (isPlaying)
        {
            NextFrame();
        }

    }

    public void PlayAnimation()
    {
        originalSprite = parentSpriteRenderer.sprite;
        isPlaying = true;
        Debug.Log("Playing animation: " + animationName);
        NextFrame();
    }

    private void NextFrame()
    {
        if (currentFrame >= animations.Count && isLooping)
        {
            currentFrame = 0;
        }
        else if (currentFrame >= animations.Count && !isLooping)
        {
            Debug.Log("Ending animation: " + animationName);
            isPlaying = false;
            currentFrame = 0;
            parentSpriteRenderer.sprite = originalSprite;
            return;
        }

        parentSpriteRenderer.sprite = animations[currentFrame];
        currentFrame++;
        timer = 1/fps;
        Debug.Log("Frame: " + currentFrame);
    }
}
