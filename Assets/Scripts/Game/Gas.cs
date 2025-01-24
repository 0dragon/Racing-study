using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public float gasAmount = 20.0f; // 충전할 가스 양

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Car car = other.GetComponent<Car>();
            if (car != null)
            {
                car.AddGas(gasAmount);
                Destroy(gameObject);
            }
        }
    }
}
