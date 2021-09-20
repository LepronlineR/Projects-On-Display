using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SHMUPMenuController : MonoBehaviour
{
    public GameObject HighScoreText;

    void Start(){
        Cursor.visible = true;
        if(HighScoreText!=null){
            ShowHighScore();
        }
    }

    void ShowHighScore(){
        float high = PlayerPrefs.GetFloat("High Score SHMUP");
        string minute = Mathf.Floor((high%3600)/60).ToString("00");
        string second = (high%60).ToString("00");
        HighScoreText.GetComponent<Text>().text = ("High Score: "+minute+":"+second);
    }

    public void Menu(){
        SceneManager.LoadScene(6);
    }

    public void StartGame(){
        SceneManager.LoadScene(7);
    }

    public void Quit(){
        SceneManager.LoadScene(0);
    }
}
