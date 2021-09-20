using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SHMUPBossThreePartTwo : MonoBehaviour
{
    private GameObject player;
    public float health;
    [SerializeField] GameObject[] bullets;

    private GameObject bossText;
    private float bossBulletSpeed;
    private List<GameObject> laserObjects = new List<GameObject>();
    

    void Start(){
        health = 675.0f;
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
        bossText.GetComponent<Text>().text = "Mission 3 Final Boss";
        yield return new WaitForSeconds(2.0f);
        bossText.GetComponent<Text>().text = "Defeat the boss";
        yield return new WaitForSeconds(1.5f);
        bossText.GetComponent<Text>().text = "";
        StartCoroutine("ShootAtPlayer");
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(ShootMachineGun(0,0)); //this should make two spiral patterns
        StartCoroutine(ShootMachineGun(180,0));
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShootMachineGun(90,1)); //double spiral :O
        StartCoroutine(ShootMachineGun(270,1));
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(ShootMachineGun(45,0)); 
        StartCoroutine(ShootMachineGun(225,1));
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(ShootMachineGun(-45,0)); 
        StartCoroutine(ShootMachineGun(-225,1));
    }



    IEnumerator ShootMachineGun(float angle,int gunType){
        yield return new WaitForSeconds(0.1f);
        //Debug.Log(angle);
        GameObject firedLaser = (GameObject) Instantiate(bullets[gunType],transform.position,transform.rotation);
        //around a circle
        firedLaser.GetComponent<Rigidbody2D>().velocity = 
        new Vector2(transform.position.x+Mathf.Sin((angle*Mathf.PI)/180.0f)*bossBulletSpeed,
        transform.position.x+Mathf.Cos((angle*Mathf.PI)/180.0f)*bossBulletSpeed);
        if(gunType==0)
            angle+=10.0f;
        else
            angle-=10.0f;
        StartCoroutine(ShootMachineGun(angle,gunType));
    }

    IEnumerator ShootAtPlayer(){
        for(int x=0; x<3; x++){
            yield return new WaitForSeconds(0.5f);
            GameObject firedLaser = (GameObject) Instantiate(bullets[0],transform.position,transform.rotation);
            //change velocity based on where the player is
            Transform playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            firedLaser.GetComponent<Rigidbody2D>().velocity = 
            new Vector2((playerPos.position.x-transform.position.x)*(bossBulletSpeed/6.0f),
            (playerPos.position.y-transform.position.y)*(bossBulletSpeed/6.0f));
        }
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(ShootAtPlayer());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("playerlaser")){
            health-=player.GetComponent<SHMUPplayercontroller>().playerDamage;
        }
    }
}
