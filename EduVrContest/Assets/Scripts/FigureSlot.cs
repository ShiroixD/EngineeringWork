using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSlot : MonoBehaviour
{
    private FiguresContainerController _figuresContainerController;
    void Start()
    {
        _figuresContainerController = GameObject.FindWithTag("FiguresContainer").GetComponent<FiguresContainerController>();
    }

    void Update()
    {
        
    }

    void Disappear()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Item")
        {
            _figuresContainerController.DecreaseFiguresSlots();
            Disappear();
        }
    }
}
