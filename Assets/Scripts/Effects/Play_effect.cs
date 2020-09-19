using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_effect : MonoBehaviour
{
    public GameObject[] effect_prefabs;

    public void Effect_play()
    {
        foreach (GameObject item in effect_prefabs)
        {
            Instantiate(item);
        }
    }
}
