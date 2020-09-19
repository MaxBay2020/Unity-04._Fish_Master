using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_attr : MonoBehaviour
{
    public int speed;
    public int damage;
    public GameObject web_prefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit the border
        if (collision.tag == "border")
        {
            Destroy(gameObject);
        }

        //hit fish
        if (collision.tag == "Fish")
        {
            GameObject web = Instantiate(web_prefab);
            web.transform.SetParent(this.transform.parent, false);
            web.transform.position = this.transform.position;
            web.GetComponent<Web_attr>().damage = damage;
            Destroy(gameObject);
        }
    }
}
