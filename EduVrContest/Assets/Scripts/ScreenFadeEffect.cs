using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeEffect : MonoBehaviour
{
    private Animator FadeEffectAnimator;
    public bool AnimationIsPlaying;
    public Image BlackScreenImage;

    private void Awake()
    {
        AnimationIsPlaying = false;
        FadeEffectAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AnimationStarted()
    {
        AnimationIsPlaying = true;
    }

    public void AnimationFinished()
    {
        AnimationIsPlaying = false;
    }

    public void FadeInEffect()
    {
        FadeEffectAnimator.SetTrigger("FadeIn");
    }

    public void FadeOutEffect()
    {
        FadeEffectAnimator.SetTrigger("FadeOut");
    }

    public void FadeBlackEffect()
    {
        FadeEffectAnimator.SetTrigger("FadeBlack");
    }
}
