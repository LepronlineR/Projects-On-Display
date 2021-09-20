using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SHMUPBossOne : MonoBehaviour
{
    public GameObject[] machineGuns;

    private GameObject player;
    public float health;
    [SerializeField] GameObject plasma;

    private GameObject bossText;
    private float bossBulletSpeed;
    private List<GameObject> laserObjects = new List<GameObject>();
    

    void Start(){
        health = 250.0f;
        bossBulletSpeed = 5.5f;
        bossText = GameObject.Find("bossText");
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("StartBoss");
    }

    void Update(){
        foreach(GameObject g in machineGuns){
            g.transform.Rotate(0.0f,0.0f,0.5f);
        }
        if(health<=0){
            GameObject death = GameObject.Find("DeathController");
            GameObject boss = this.gameObject;
            death.GetComponent<SHMUPDeathController>().StartCoroutine(death.GetComponent<SHMUPDeathController>().OnBossDeath(boss));
        }
    }

    IEnumerator StartBoss(){
        bossText.GetComponent<Text>().text = "Mission 1 Boss";
        yield return new WaitForSeconds(2.0f);
        bossText.GetComponent<Text>().text = "Shoot The Cockpit";
        yield return new WaitForSeconds(1.5f);
        bossText.GetComponent<Text>().text = "";
        foreach(GameObject g in machineGuns){
            StartCoroutine(ShootMachineGun(g));
        }
    }



    // the guns really dont work as "intended" but it somehow created something better
    //I have no idea how it did that but I'm much better off with this
    IEnumerator ShootMachineGun(GameObject g){
        yield return new WaitForSeconds(0.1f);
        GameObject firedLaser = (GameObject) Instantiate(plasma,g.transform.position,g.transform.rotation);
        //use sin/cos to get the final vector 
        float angle = Quaternion.Angle(transform.rotation, g.transform.rotation);
        //Debug.Log(angle);
        firedLaser.GetComponent<Rigidbody2D>().velocity = 
        new Vector2(bossBulletSpeed*(Mathf.Cos(angle)),bossBulletSpeed*(Mathf.Sin(angle)));
        StartCoroutine(ShootMachineGun(g));
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("playerlaser")){
            health-=player.GetComponent<SHMUPplayercontroller>().playerDamage;
        }
    }
}
