using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory_self : MonoBehaviour
{
    public int stay_time;

    void Start()
    {
        Destroy(this.gameObject, stay_time);
    }

}
