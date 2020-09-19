using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_rotate : MonoBehaviour
{
    public float angle_speed = 10f;



    void Update()
    {
        transform.Rotate(Vector3.forward, angle_speed * Time.deltaTime);
    }
}
