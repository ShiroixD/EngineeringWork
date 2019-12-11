using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSlot : MonoBehaviour
{
    private FiguresContainerController _figuresContainerController;
    public FigureName FigureName;
    public GameObject Effect;
    void Start()
    {
        _figuresContainerController = GameObject.FindWithTag("FiguresContainer").GetComponent<FiguresContainerController>();
    }

    void Update()
    {
        
    }

    void Disappear()
    {
        _figuresContainerController.DecreaseFiguresSlots();
        Destroy(gameObject);
    }

    IEnumerator EffectDelay()
    {
        Effect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Disappear();
    }

    public void ShowEffect()
    {
        StartCoroutine("EffectDelay");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Item" &&
            gameObject.GetComponent<FigureSlot>().FigureName.Equals(
                collision.collider.GetComponent<FigureItem>().FigureName
            )
        )
        {
            ShowEffect();
        }
    }
}
