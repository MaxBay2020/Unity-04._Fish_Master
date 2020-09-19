using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_attr : MonoBehaviour
{
    public int max_num;
    public int max_speed;
    public int hp;
    public int gold;
    public int exp;

    public GameObject die_prefab;

    public GameObject coin_prefab;

    /// <summary>
    /// when fish touch border, detory fish
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "border")
        {
            Destroy(gameObject);
        }
    }

    //when hit by web, damage
    void Take_damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            //die
            GameObject die = Instantiate(die_prefab);
            die.transform.SetParent(gameObject.transform.parent,false);
            die.transform.position = this.transform.position;
            die.transform.rotation = this.transform.rotation;

            Game_controller.Instance.gold += gold;
            Game_controller.Instance.exp += exp;

            GameObject coin = Instantiate(coin_prefab);
            coin.transform.SetParent(gameObject.transform.parent, false);
            coin.transform.position = this.transform.position;
            coin.transform.rotation = this.transform.rotation;

            if (gameObject.GetComponent<Play_effect>() != null)
            {
                gameObject.GetComponent<Play_effect>().Effect_play();
                Audio_manager.Instance.Play_clip(Audio_manager.Instance.reward_clip);

            }

            Destroy(gameObject);
            Destroy(die, 1);
        }
    }
}
