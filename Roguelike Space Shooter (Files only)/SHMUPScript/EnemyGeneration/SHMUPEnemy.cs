using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPEnemy : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    [SerializeField] GameObject laser;
    private GameObject firedWeaponSound;
    private float health;
    private GameObject player;
    private int random;

    public GameObject[] powerUps;
    public string type;
    public string color;

    public float enemyShootingSpeed;
    public float enemyDamage;

    private GameObject powerupPool;
    private float enemylaserSpeed;
    private Vector2 playerPos;
    private float enemySpeed;
    private Vector2 lookVector;
    private bool onlyOnce;
    private bool restartCoroutine;
    private List<GameObject> laserObjects = new List<GameObject>();
    private GameObject gameController;

    void Awake(){
        restartCoroutine = false;
        enemyDamage = 1.0f;
        enemyRb = GetComponent<Rigidbody2D>();
        onlyOnce = false;
        powerupPool = GameObject.Find("PowerUpPool");
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.Find("GameController");
        firedWeaponSound = GameObject.Find("AudioController/WeaponFire");

        health = gameController.GetComponent<SHMUPGameController>().stage;
        if(type.Equals("Chaser")){
            if(color.Equals("Green")){
                health *= 1;
                enemylaserSpeed = 5.0f; 
                enemyShootingSpeed = 2.0f;
                enemySpeed = 0.05f;
            } else if(color.Equals("Orange")){
                health *= 5;
                enemylaserSpeed = 5.0f; 
                enemyShootingSpeed = 1.5f;
                enemySpeed = 0.01f;
            } else if(color.Equals("Red")){
                health *= 10;
                enemylaserSpeed = 5.0f; 
                enemyShootingSpeed = 1.0f;
                enemySpeed = 0.05f;
            }
        }
    }
    void Start(){
        StartCoroutine("ShootAtPlayer");
        transform.rotation = Quaternion.Euler(0.0f,0.0f,0.0f);
    }
    void Update(){
        if(!onlyOnce&&health<=0){
            StartCoroutine("OnDeath");
            onlyOnce = true;
        }
    }
    void FixedUpdate(){
        if(restartCoroutine&&type.Equals("Chaser")){
            StartCoroutine("ShootAtPlayer");
            restartCoroutine = false;
        }
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
        } else {
            // if the raycast does not detect anything remove all gameobjects from the field and then
            // restart the shooting coroutines.
            if(type.Equals("Chaser")){
                restartCoroutine = true;
                StopCoroutine("ShootAtPlayer");
                laserObjects.Clear();
            }
        }
        //return to original rotation if the raycast does not hit
        if (transform.rotation.eulerAngles.y == 90 || transform.rotation.eulerAngles.y == -90){
            transform.rotation *= Quaternion.Euler(1.0f,0.0f,1.0f);
        }
        //Debug.DrawLine(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
    }

    IEnumerator ShootAtPlayer(){
        yield return new WaitForSeconds(enemyShootingSpeed);
        firedWeaponSound.GetComponent<AudioSource>().Play();
        GameObject firedLaser = (GameObject) Instantiate(laser,transform.position,transform.rotation);
        //change velocity based on where the player is
        firedLaser.GetComponent<Rigidbody2D>().velocity = 
        new Vector2((GameObject.FindGameObjectWithTag("Player").transform.position.x-transform.position.x)*enemylaserSpeed,
        (GameObject.FindGameObjectWithTag("Player").transform.position.y-transform.position.y)*enemylaserSpeed);
        laserObjects.Add(firedLaser);
        StartCoroutine("ShootAtPlayer");
        yield return new WaitForSeconds(0.5f);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Equals("playerlaser")){
            health-=player.GetComponent<SHMUPplayercontroller>().playerDamage;
        }
    }

    IEnumerator OnDeath(){
        //explosions happen
        random = Random.Range(0,2);
        //spawn in a powerup (get both prefab then instantiate it)
        switch(random){
            case 0:
                GameObject pu0 = (GameObject) Instantiate(powerUps[0],transform.position,Quaternion.identity);
                pu0.transform.parent = powerupPool.transform;
                break;
            case 1:
                GameObject pu1 = (GameObject) Instantiate(powerUps[1],transform.position,Quaternion.identity);
                pu1.transform.parent = powerupPool.transform;
                break;
        }
        GameObject death = GameObject.Find("DeathController");
        GameObject enemy = this.gameObject;
        gameController.GetComponent<SHMUPGameController>().enemiesOnScreen.Remove(this.gameObject);
        death.GetComponent<SHMUPDeathController>().StartCoroutine(death.GetComponent<SHMUPDeathController>().OnEnemyPlaneDeath(enemy));
        yield return null;
    }
}
