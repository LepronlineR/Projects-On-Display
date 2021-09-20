using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPDevModeController : MonoBehaviour
{
    private bool openMenuButton;
    [SerializeField] GameObject menuPanel;
    private string dev;
    private GameObject player;

    void Start(){
        dev = PlayerPrefs.GetString("Devmode");
        player = GameObject.Find("Player");
        if(dev.Equals("on")){
            openMenuButton = true;
        }
        else if(dev.Equals("off"))
            openMenuButton = false;
        else
            openMenuButton = false;
    }

    void Update(){
        if(openMenuButton){
            if(Input.GetKeyDown(KeyCode.G)){
                OpenMenu();
            }
        }
    }

    void OpenMenu(){
        menuPanel.SetActive(true);
    }
    public void CloseMenu(){
        menuPanel.SetActive(false);
    }

    public void SetLife(){
        player.GetComponent<SHMUPplayercontroller>().playerLife = 999;
    }

    public void AddDamage(){
        player.GetComponent<SHMUPplayercontroller>().playerDamage += 20;
    }

    public void MoreSpeed(){
        player.GetComponent<SHMUPplayercontroller>().shootingSpeed /= 2;
    }
}
