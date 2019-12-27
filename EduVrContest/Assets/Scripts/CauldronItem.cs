using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronItem : MonoBehaviour
{
    public FoodName FoodName;
    public GameObject Effect;

    void Start()
    {
        ShowEffect();
    }

    void Update()
    {
        
    }

    IEnumerator EffectDelay()
    {
        Effect.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        Effect.SetActive(false);
    }

    IEnumerator DelayedDisappear(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }

    public void ShowEffect()
    {
        StartCoroutine("EffectDelay");
    }

    void Disappear()
    {
        StartCoroutine(DelayedDisappear(3.0f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Disappear();
            return;
        }
    }
}
