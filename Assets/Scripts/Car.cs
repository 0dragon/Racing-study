using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 10.0f;
    void Start()
    {
        
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
