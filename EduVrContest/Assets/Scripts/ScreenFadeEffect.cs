using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeEffect : MonoBehaviour
{
    private Animator FadeEffectAnimator;
    public bool AnimationFinished;
    public Image BlackScreenImage;

    void Start()
    {
        AnimationFinished = true;
        FadeEffectAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void FadeIdleEffect()
    {
        FadeEffectAnimator.SetTrigger("FadeIdle");
        AnimationFinished = true;
    }

    public void FadeInEffect()
    {
        FadeEffectAnimator.SetTrigger("FadeIn");
        AnimationFinished = false;
    }

    public void FadeOutEffect()
    {
        FadeEffectAnimator.SetTrigger("FadeOut");
        AnimationFinished = false;
    }
}
