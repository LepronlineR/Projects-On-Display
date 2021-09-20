using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SHMUPBossTwo : MonoBehaviour
{

    private GameObject player;
    public float health;
    [SerializeField] GameObject[] bullets;

    private GameObject bossText;
    private float bossBulletSpeed;
    private List<GameObject> laserObjects = new List<GameObject>();
    

    void Start(){
        health = 475.0f;
        bossBulletSpeed = 5.5f;
        bossText = GameObject.Find("bossText");
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("StartBoss");
    }

    void Update(){
        if(health<=0){
            GameObject death = GameObject.Find("DeathController");
            GameObject boss = this.gameObject;
            death.GetComponent<SHMUPDeathController>().StartCoroutine(death.GetComponent<SHMUPDeathController>().OnBossDeath(boss));
        }
    }

    IEnumerator StartBoss(){
        bossText.GetComponent<Text>().text = "Mission 2 Boss";
        yield return new WaitForSeconds(2.0f);
        bossText.GetComponent<Text>().text = "Avoid the Middle";
        yield return new WaitForSeconds(1.5f);
        bossText.GetComponent<Text>().text = "";
        transform.position = Vector2.Lerp(transform.position, new Vector2(0.0f,0.0f),1.0f);
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShootMachineGun(20,0.0f,0)); //plasma
        yield return new WaitForSeconds(15.0f);
        StartCoroutine(ShootMachineGun(20,0.0f,1)); //plasma 2
        
    }



    IEnumerator ShootMachineGun(int amount,float newAngle,int gunType){
        yield return new WaitForSeconds(0.5f);
        //Debug.Log(angle);
        float angle = 0.0f+newAngle;
        float angleAmount = 360.0f/amount;
        for(int x=0; x<= amount -1; x++){
            GameObject firedLaser = (GameObject) Instantiate(bullets[gunType],transform.position,transform.rotation);
            //around a circle
            // change speed for different pattern
            float changePat = 1.0f;
            if(gunType==1){
                changePat = 4.0f;
            }
            firedLaser.GetComponent<Rigidbody2D>().velocity = 
            new Vector2(transform.position.x+Mathf.Sin((angle*Mathf.PI)/180.0f)*bossBulletSpeed/changePat,
            transform.position.x+Mathf.Cos((angle*Mathf.PI)/180.0f)*bossBulletSpeed/changePat);
            angle+=angleAmount;
        }
        StartCoroutine(ShootMachineGun(amount,newAngle+5.0f,gunType));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("playerlaser")){
            health-=player.GetComponent<SHMUPplayercontroller>().playerDamage;
        }
    }
}
