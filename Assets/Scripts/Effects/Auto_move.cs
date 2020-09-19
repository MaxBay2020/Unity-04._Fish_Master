using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto_move : MonoBehaviour
{
    public float speed = 1f;
    //vector3.right is x direction
    public Vector3 dir = Vector3.right;


    void Update()
    {
        transform.Translate(dir * speed*Time.deltaTime);
    }
}
