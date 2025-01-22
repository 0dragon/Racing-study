using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 10.0f;
    public float horizontalInput;
    
    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        
        transform.Translate(Vector3.right * horizontalInput * turnSpeed * Time.deltaTime);
    }

    public void SetHorizontalInput(float input)
    {
        horizontalInput = input;
    }

    public void OnPointerDownLeft()
    {
        SetHorizontalInput(-1);
    }

    public void OnPointerDownRight()
    {
        SetHorizontalInput(1);
    }

    public void OnPointerUp()
    {
        SetHorizontalInput(0);
    }
}
