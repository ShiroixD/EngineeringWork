using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using System;

[RequireComponent(typeof(InteractionBehaviour))]
public class CauldronItem : MonoBehaviour
{
    public FoodName FoodName;
    public GameObject Effect;
    private InteractionBehaviour _intObj;

    void Start()
    {
        _intObj = GetComponent<InteractionBehaviour>();
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

    public void DisappearLater()
    {
        StartCoroutine(DelayedDisappear(3.0f));
    }

    public void DisappearNow()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("My item tag: " + gameObject.tag);
        //Debug.Log("My item name: " + FoodName.ToString());
        //Debug.Log("Coll tag: " + collision.collider.tag);
        if (collision.collider.tag == "Ground")
        {
            _intObj.ignoreGrasping = true;
            _intObj.ignoreContact = true;
            _intObj.ignorePrimaryHover = true;
            DisappearLater();
            return;
        }

        //if (collision.collider.tag == "FoodContainer")
        //{
        //    _intObj.ignoreGrasping = true;
        //    _intObj.ignoreContact = true;
        //    _intObj.ignorePrimaryHover = true;
        //    DisappearNow();
        //}
    }
}
