using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_to : MonoBehaviour
{
    private GameObject gold_collection;

    void Start()
    {
        gold_collection = GameObject.Find("gold_collection");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, gold_collection.transform.position, 5*Time.deltaTime);
    }
}
