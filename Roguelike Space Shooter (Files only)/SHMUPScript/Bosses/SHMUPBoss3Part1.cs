using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SHMUPBoss3Part1 : MonoBehaviour
{
    private GameObject player;
    public float health;
    [SerializeField] GameObject[] bullets;

    private GameObject bossText;
    private float bossBulletSpeed;
    private float enemySpeed;
    private float enemylaserSpeed;
    

    void Start(){
        enemylaserSpeed = 5.0f;
        enemySpeed = 0.01f;
        health = 400.0f;
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
        bossText.GetComponent<Text>().text = "Mission 3 Boss";
        yield return new WaitForSeconds(2.0f);
        bossText.GetComponent<Text>().text = "Finish the Mission";
        yield return new WaitForSeconds(1.5f);
        bossText.GetComponent<Text>().text = "";
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(ShootMachineGun(6,1.0f,0));
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(ShootMachineGun(12,0.0f,1));
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(ShootMachineGun(12,0.0f,0));
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("playerlaser")){
            health-=player.GetComponent<SHMUPplayercontroller>().playerDamage;
        }
    }

    void FixedUpdate(){
        //Raycast hit a target (player) and then follows the player, uses the raycast to rotate as well. Cant see through astroids
        RaycastHit2D hitTarget;
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        if(tempPlayer!=null){
            hitTarget = Physics2D.Linecast(tempPlayer.transform.position, transform.position, LayerMask.GetMask("playerlaser") | LayerMask.GetMask("Enemy") | LayerMask.GetMask("Player") | LayerMask.GetMask("UI"));
            transform.position = Vector2.MoveTowards(transform.position, tempPlayer.transform.position, enemySpeed);
            if(hitTarget){
                Vector3 hit = hitTarget.point;
                transform.LookAt(hit);
                //fix rotation due to an upward position and 2d fix
                transform.rotation *= (Quaternion.FromToRotation(Vector3.right, Vector3.forward));
                transform.rotation *= Quaternion.Euler(0.0f,0.0f,270.0f);
            }
        }
        //return to original rotation if the raycast does not hit
        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == -90){
            transform.rotation *= Quaternion.Euler(1.0f,0.0f,1.0f);
        }
        //Debug.DrawLine(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
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
                changePat = 4.5f;
            }
            firedLaser.GetComponent<Rigidbody2D>().velocity = 
            new Vector2(transform.position.x+Mathf.Sin((angle*Mathf.PI)/180.0f)*bossBulletSpeed/changePat,
            transform.position.x+Mathf.Cos((angle*Mathf.PI)/180.0f)*bossBulletSpeed/changePat);
            angle+=angleAmount;
        }
        StartCoroutine(ShootMachineGun(amount,newAngle+5.0f,gunType));
    }
}
