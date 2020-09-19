using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web_attr : MonoBehaviour
{
    public float stay_time;
    public int damage;
    void Start()
    {
        Destroy(gameObject, stay_time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            collision.SendMessage("Take_damage", damage);
        }
    }
}
