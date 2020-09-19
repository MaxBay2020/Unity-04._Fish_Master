using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_scene_UI : MonoBehaviour
{
    public GameObject setting_panel;

    public void Mute_state_switch()
    {
        Audio_manager.Instance.Mute_state_change();
    }

    public void Btn_back_click()
    {
        //save game
        PlayerPrefs.SetInt("gold", Game_controller.Instance.gold);
        PlayerPrefs.SetInt("level", Game_controller.Instance.level);
        PlayerPrefs.SetFloat("small_count_down", Game_controller.Instance.small_timer);
        PlayerPrefs.SetFloat("big_count_down", Game_controller.Instance.big_timer);
        PlayerPrefs.SetInt("exp", Game_controller.Instance.exp);
        PlayerPrefs.SetInt("mute", Audio_manager.Instance.IsMute==false?0:1);
        SceneManager.LoadScene(0);
        
    }

    public void Btn_setting_click()
    {
        //display setting panel
        setting_panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Btn_close_click()
    {
        //hide setting panel
        setting_panel.SetActive(false);
        Time.timeScale = 1;
    }

}
