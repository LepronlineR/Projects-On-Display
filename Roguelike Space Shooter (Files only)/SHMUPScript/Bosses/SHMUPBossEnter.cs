using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPBossEnter : MonoBehaviour
{
    private GameObject gameController;

    void Start(){
        gameController = GameObject.Find("GameController");
    }
    private void OnTriggerEnter2D(Collider2D other) {
        float enemiesCount = gameController.GetComponent<SHMUPGameController>().enemiesOnScreen.Count;
        if(other.tag.Equals("Player")&&enemiesCount==0){
            gameController.GetComponent<SHMUPGameController>().stage+=0.5f;
            gameController.GetComponent<SHMUPGameController>().getStage = false;
        }
    }
}
