using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class FiguresContainerController : MonoBehaviour
{
    public ItemSlotSpawner ItemSlotSpawner;
    public GameObject Cube;
    public GameObject ShapeHandlers;
    public GameObject Effect;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ShowEffect();
        }
    }

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    void DisableInteractiveChildrenBehaviours(Transform objectTransform)
    {
        foreach (Transform child in objectTransform)
        {
            InteractionBehaviour behaviour = child.gameObject.GetComponent<InteractionBehaviour>();
            if (behaviour != null)
            {
                behaviour.ignoreGrasping = true;
                behaviour.ignoreContact = true;
                behaviour.ignorePrimaryHover = true;
            }
            DisableInteractiveChildrenBehaviours(child);
        }
    }

    IEnumerator EffectDelay()
    {
        Effect.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        Effect.SetActive(false);
        Disappear();
    }

    public void ShowEffect()
    {

        DisableInteractiveChildrenBehaviours(ShapeHandlers.transform);
        Cube.SetActive(false);
        ShapeHandlers.SetActive(false);
        StartCoroutine("EffectDelay");
    }

    public void DecreaseFiguresSlots()
    {
        if (!ItemSlotSpawner.SpawnNextItem())
        {
            ShowEffect();
        }
    }

}
