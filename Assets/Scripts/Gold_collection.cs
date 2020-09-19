using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_collection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gold")
        {
            Destroy(collision.gameObject);
            Audio_manager.Instance.Play_clip(Audio_manager.Instance.gold_clip);

        }
    }
}
