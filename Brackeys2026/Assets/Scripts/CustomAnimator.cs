using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NUnit.Framework;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CustomAnimator : MonoBehaviour
{

    // Is converted to lowercase!
    [SerializeField] public String animationName; 

    [SerializeField] private List<Sprite> animations;
    [SerializeField] private float fps;
    [SerializeField] private bool isLooping;

    private float timer;
    private int currentFrame = 0;
    private SpriteRenderer parentSpriteRenderer;
    private Sprite originalSprite;
    private bool isPlaying = false;

    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 1/fps;
        parentSpriteRenderer = GetComponent<SpriteRenderer>();
        animationName = animationName.ToLower();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (IsPlaying)
        {
            NextFrame();
        }
    }

    public void PlayAnimation()
    {
        if (IsPlaying)
        {
            return;
        }
        originalSprite = parentSpriteRenderer.sprite;
        IsPlaying = true;
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
            IsPlaying = false;
            currentFrame = 0;
            parentSpriteRenderer.sprite = originalSprite;
            return;
        }

        parentSpriteRenderer.sprite = animations[currentFrame];
        currentFrame++;
        timer = 1/fps;
    }
}
