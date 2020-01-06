using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public float RotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * RotationSpeed);
    }
}
