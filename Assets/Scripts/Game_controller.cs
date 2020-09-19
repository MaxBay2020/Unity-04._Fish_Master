using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class Game_controller : MonoBehaviour
{
    private static Game_controller _instance;
    public static Game_controller Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gold = PlayerPrefs.GetInt("gold", gold);
        level = PlayerPrefs.GetInt("level", level);
        exp = PlayerPrefs.GetInt("exp", exp);
        big_timer = PlayerPrefs.GetFloat("big_count_down", big_timer);
        small_timer = PlayerPrefs.GetFloat("small_count_down", small_timer);
        UpdatUI();
    }

    public GameObject[] gun_gameobjects;

    private int cost_index = 0;

    //the cost of each shooting
    private int[] cost_each_shoot = { 5, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    public Text txt_cost_each_shoot;
    public int level = 0;

    public GameObject[] bullet1_gameobjects;
    public GameObject[] bullet2_gameobjects;
    public GameObject[] bullet3_gameobjects;
    public GameObject[] bullet4_gameobjects;
    public GameObject[] bullet5_gameobjects;

    public Transform bullet_holder;

    public Text txt_gold;
    public Text txt_level;
    public Text txt_title;
    public Text txt_small_time_count;
    public Text txt_big_time_count;
    public Button btn_big_count_down;
    public Button btn_back;
    public Button btn_setting;
    public Slider slider_exp;

    public int gold = 500;
    public string[] titles = {"Bronze", "Silver", "Gold", "Platinum", "Diamond","Master","Challenger", "King", "God"};
    public int big_count_down = 240;
    public int small_count_down = 60;
    public float big_timer= 240;
    public float small_timer = 60;
    public int exp = 0;

    public Color gold_color;

    public Text levelup_tip;
    public GameObject levelup_effect;

    public GameObject gun_switch_effect;
    public GameObject gun_fire_effect;
    public GameObject gold_effect;

    public Sprite[] bg_sprite;
    public Image bg_image;
    public GameObject sea_wave_prefab;
    public int bg_index;

    void UpdatUI()
    {
        big_timer -= Time.deltaTime;
        small_timer -= Time.deltaTime;

        if (small_timer <= 0)
        {
            small_timer = 60;
            gold += 50;
        }

        if (big_timer <= 0 && btn_big_count_down.gameObject.activeSelf==false)
        {
            txt_big_time_count.gameObject.SetActive(false);
            btn_big_count_down.gameObject.SetActive(true);
        }

        //demand levelup = 1000+200*current level
        while (exp>= 1000 + 200 * level)
        {
            exp -= 1000 + 200 * level;

            level++;

            //play levelup effect
            levelup_tip.gameObject.SetActive(true);
            levelup_tip.text = "Congrads! You are lv." + level + "!";
            Instantiate(levelup_effect);
            Audio_manager.Instance.Play_clip(Audio_manager.Instance.levelup_clip);

            StartCoroutine(levelup_tip.GetComponent<Hide_self>().Hide(0.7f));
            
        }
        txt_gold.text = "$"+gold;
        txt_level.text = level+"";

        if (level / 10 <= 9)
        {
            txt_title.text = titles[level / 10];
        }
        else
        {
            txt_title.text = titles[9];
        }

        txt_small_time_count.text = (int)small_timer / 10 + " " + (int)small_timer % 10;
        txt_big_time_count.text = (int)big_timer + "s";

        slider_exp.value = (float)exp / (1000 + 200 * level);



    }

    void Update()
    {
        Bullet_cost_change_by_scroll();

        Fire();

        UpdatUI();

        Bg_switch();
    }

    void Bullet_cost_change_by_scroll()
    {

        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            Plus_button_click();
            Instantiate(gun_switch_effect);
            Audio_manager.Instance.Play_clip(Audio_manager.Instance.gun_switch_clip);


        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Minus_button_click();
            Instantiate(gun_switch_effect);
            Audio_manager.Instance.Play_clip(Audio_manager.Instance.gun_switch_clip);


        }
    }

    /// <summary>
    /// plus button clicked, change gun
    /// </summary>
    public void Plus_button_click()
    {
        Instantiate(gun_switch_effect);
        Audio_manager.Instance.Play_clip(Audio_manager.Instance.gun_switch_clip);

        gun_gameobjects[cost_index / 4].SetActive(false);
        cost_index++;

        cost_index = cost_index > cost_each_shoot.Length - 1 ? 0 : cost_index;

        gun_gameobjects[cost_index / 4].SetActive(true);
        txt_cost_each_shoot.text = "$ "+cost_each_shoot[cost_index];

    }


    /// <summary>
    /// minus button clicked, change gun
    /// </summary>
    public void Minus_button_click()
    {
        Instantiate(gun_switch_effect);
        Audio_manager.Instance.Play_clip(Audio_manager.Instance.gun_switch_clip);

        gun_gameobjects[cost_index / 4].SetActive(false);
        cost_index--;

        cost_index = cost_index < 0 ? cost_each_shoot.Length - 1 : cost_index;

        gun_gameobjects[cost_index / 4].SetActive(true);

        txt_cost_each_shoot.text = "$ " + cost_each_shoot[cost_index];

    }

    /// <summary>
    /// press mouse left button shooting
    /// </summary>
    void Fire()
    {
        GameObject[] using_bullets = bullet5_gameobjects;

        int bullet_index;

        if (Input.GetMouseButtonDown(0)&& EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (gold - cost_each_shoot[cost_index] >= 0)
            {

            

                switch (cost_index/4)
                {
                    case 0:
                        using_bullets = bullet1_gameobjects;
                        break;
                    case 1:
                        using_bullets = bullet2_gameobjects;
                        break;
                    case 2:
                        using_bullets = bullet3_gameobjects;
                        break;
                    case 3:
                        using_bullets = bullet4_gameobjects;
                        break;
                    case 4:
                        using_bullets = bullet5_gameobjects;
                        break;
                    default:
                        break;
                }

                bullet_index = level % 10>=9?9:level%10;
                gold -= cost_each_shoot[cost_index];


                GameObject bullet = Instantiate(using_bullets[bullet_index]);
                bullet.transform.SetParent(bullet_holder, false);
                bullet.transform.position = gun_gameobjects[cost_index / 4].transform.Find("fire_pos").transform.position;
                bullet.transform.rotation = gun_gameobjects[cost_index / 4].transform.Find("fire_pos").transform.rotation;
                bullet.AddComponent<Auto_move>().dir = Vector3.up;
                bullet.GetComponent<Auto_move>().speed = bullet.GetComponent<Bullet_attr>().speed;
                bullet.GetComponent<Bullet_attr>().damage = cost_each_shoot[cost_index];

                Instantiate(gun_fire_effect);
                Audio_manager.Instance.Play_clip(Audio_manager.Instance.fire_clip);


            }
            else
            {
                //not enough gold
                StartCoroutine(Gold_not_enough_effect());

            }


        }
    }

    IEnumerator Gold_not_enough_effect()
    {
        txt_gold.color = gold_color;
        txt_gold.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        txt_gold.color = Color.yellow;
    }

    public void big_count_down_click()
    {
        gold += 500;
        Instantiate(gold_effect);
        btn_big_count_down.gameObject.SetActive(false);
        txt_big_time_count.gameObject.SetActive(true);

        big_timer = 240;

        Audio_manager.Instance.Play_clip(Audio_manager.Instance.reward_clip);

    }

    void Bg_switch()
    {
        if (bg_index != level / 20)
        {
            bg_index = level / 20;
            Instantiate(sea_wave_prefab);
            Audio_manager.Instance.Play_clip(Audio_manager.Instance.sea_wave_clip);
            if (bg_index >= 3)
            {
                bg_image.sprite = bg_sprite[3];
            }
            else
            {
                bg_image.sprite = bg_sprite[bg_index];
            }

        }
    }

}
