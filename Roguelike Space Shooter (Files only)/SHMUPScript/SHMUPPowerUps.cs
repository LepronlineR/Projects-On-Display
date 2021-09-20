using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPPowerUps : MonoBehaviour
{
    [SerializeField] string powerUpName;
    private GameObject player;
    private float shootingSpeedIncrement;
    private float playerDamageIncrement;

    void Awake(){
        player = GameObject.FindGameObjectWithTag("Player");
        shootingSpeedIncrement = 1.05f;
        playerDamageIncrement = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Equals("Player")){
            if(powerUpName.Equals("ShotSpeed")){
                other.GetComponent<SHMUPplayercontroller>().shootingSpeed/=shootingSpeedIncrement;
                shootingSpeedIncrement+=0.05f;
            }
            if(powerUpName.Equals("ShotDamage")){
                other.GetComponent<SHMUPplayercontroller>().playerDamage+=playerDamageIncrement;
                playerDamageIncrement+=0.5f;
            }
            Destroy(gameObject);
        }
    }
}
