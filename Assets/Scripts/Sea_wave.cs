using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea_wave : MonoBehaviour
{
    private Vector3 goal;

    private void Start()
    {
        goal = -transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, 10*Time.deltaTime);
    }
}
