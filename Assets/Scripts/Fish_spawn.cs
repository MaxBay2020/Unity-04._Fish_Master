using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Fish_spawn : MonoBehaviour
{
    public Transform[] spawn_pos;
    public GameObject[] fish_prefabs;
    public Transform fish_holder;
    public float wave_interval = 0.3f;

    public float spawn_interval = 0.5f;

    void Start()
    {
        InvokeRepeating("Spawn_fish", 0, wave_interval);
    }

    void Spawn_fish()
    {
        //assign spawn position
        int spawn_pos_index = Random.Range(0, spawn_pos.Length);

        //assign fish type
        int fish_prefab_index = Random.Range(0, fish_prefabs.Length);

        //get the max spawn number of fish
        int max_num = fish_prefabs[fish_prefab_index].GetComponent<Fish_attr>().max_num;

        //get the max speed of fish
        int max_speed = fish_prefabs[fish_prefab_index].GetComponent<Fish_attr>().max_speed;

        //assign the number of fish spawned
        int num = Random.Range(max_num / 2 + 1, max_num+1);

        //assgin the speed of fish spawned
        int speed = Random.Range(max_speed / 2, max_speed+1);

        //0: move directly; 1: move curl
        int move_type = Random.Range(0, 2);

        //move directly: move angle
        int angle_offset;

        //move curl: the angle speed
        int angle_speed;

        if (move_type == 0)
        {
            //move directly
            angle_offset = Random.Range(-22,22);
            StartCoroutine(Spawn_fish_directly_move(spawn_pos_index, fish_prefab_index, num, speed, angle_offset));
        }
        else
        {
            //move curl
            if (Random.Range(0, 2) == 0)
            {
                //50% angle speed is between -9 and -15
                angle_speed = Random.Range(-15, -9);
            }
            else
            {
                //50% angle speed is between 9 and 15
                angle_speed = Random.Range(9, 15);
            }
            StartCoroutine(Spawn_fish_curly_move(spawn_pos_index, fish_prefab_index, num, speed, angle_speed));
        }
    }

    /// <summary>
    /// fish move directly
    /// </summary>
    /// <param name="spawn_pos_index"></param>
    /// <param name="fish_prefab_index"></param>
    /// <param name="num"></param>
    /// <param name="speed"></param>
    /// <param name="angle_offset"></param>
    /// <returns></returns>
    IEnumerator Spawn_fish_directly_move(int spawn_pos_index, int fish_prefab_index, int num, int speed, int angle_offset)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fish_prefabs[fish_prefab_index]);
            fish.transform.SetParent(fish_holder, false);
            fish.transform.localPosition = spawn_pos[spawn_pos_index].localPosition;
            fish.transform.localRotation = spawn_pos[spawn_pos_index].localRotation;
            fish.transform.Rotate(0, 0, angle_offset);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Auto_move>().speed = speed;
            yield return new WaitForSeconds(spawn_interval);
        };
    }

    /// <summary>
    /// fish move curly
    /// </summary>
    /// <param name="spawn_pos_index"></param>
    /// <param name="fish_prefab_index"></param>
    /// <param name="num"></param>
    /// <param name="speed"></param>
    /// <param name="angle_offset"></param>
    /// <returns></returns>
    IEnumerator Spawn_fish_curly_move(int spawn_pos_index, int fish_prefab_index, int num, int speed, int angle_speed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fish_prefabs[fish_prefab_index]);
            fish.transform.SetParent(fish_holder, false);
            fish.transform.localPosition = spawn_pos[spawn_pos_index].localPosition;
            fish.transform.localRotation = spawn_pos[spawn_pos_index].localRotation;
            fish.transform.Rotate(0, 0, angle_speed);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Auto_move>().speed = speed;
            fish.AddComponent<Auto_rotate>().angle_speed = angle_speed;
            yield return new WaitForSeconds(spawn_interval);
        };
    }
}
