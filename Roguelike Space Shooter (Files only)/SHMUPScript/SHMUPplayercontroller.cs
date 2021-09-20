using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SHMUPplayercontroller : MonoBehaviour
{
    public float shootingSpeed;
    public float playerDamage;
    public float playerLife;

    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Transform crosshairLocation;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject firedWeaponSound;

    [SerializeField] GameObject damageText;
    [SerializeField] GameObject shootingText;
    [SerializeField] GameObject lifeText;
    [SerializeField] GameObject timeText;
    [SerializeField] GameObject panel;

    private float laserSpeed;
    private Vector2 movement;
    private Vector3 fixedRotation;
    private float angle;
    private float defaultAngle;
    private float playerSpeed;
    private float savedTime;
    private float speed;

    // Start is called before the first frame update
    void Awake(){
        speed = 1.0f;
        playerLife = 5.0f;
        playerDamage = 1.0f; //stronger upgrade
        laserSpeed = 27.5f; 
        shootingSpeed = 0.5f; //faster upgrade
        defaultAngle = 90.0f;
        playerSpeed = 5.0f; 
    }

    public void SaveHighScore(){
        //save the high score, or if it is the first time then have the first score as the high score
        float high = PlayerPrefs.GetFloat("High Score SHMUP");
        if(high>savedTime || high==0.0f){
            PlayerPrefs.SetFloat("High Score SHMUP",savedTime);
        }
    }

    void ShowScore(){
        string minute = Mathf.Floor((savedTime%3600)/60).ToString("00");
        string second = (savedTime%60).ToString("00");
        //Debug.Log("Time: "+minute+":"+second);
        timeText.GetComponent<Text>().text = ("Time: "+minute+":"+second);
    }

    // Update is called once per frame
    void Update(){
        savedTime += Time.deltaTime * speed;
        ShowScore();
        //Debug.Log(savedTime);
        //set UI on
        if(Input.GetKeyDown(KeyCode.Tab)){
            panel.SetActive(true);
        } else if(Input.GetKeyUp(KeyCode.Tab)){
            panel.SetActive(false);
        }
        damageText.GetComponent<Text>().text = "Damage: "+ playerDamage;
        shootingText.GetComponent<Text>().text = "Shooting: "+ shootingSpeed;
        lifeText.GetComponent<Text>().text = "Life: "+ playerLife;

        //when player dies it returns to the start screen

        // Used for player movement/rotation + shooting 
        UpdateMovementandLook();
        if(Input.GetMouseButtonDown(0)){
            StartCoroutine("ShootLaser");
        } else if(Input.GetMouseButtonUp(0)){
            StopCoroutine("ShootLaser");
        }

        //slow mode, if you hold shift you can enter slow mode, makes it easier to dodge bullets
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            Time.timeScale = 0.5f;
        } else if(Input.GetKeyUp(KeyCode.LeftShift)){
            Time.timeScale = 1.0f;
        }
        /* test out death
        if(Input.GetKeyDown(KeyCode.T)){
            GameObject death = GameObject.Find("DeathController");
            death.GetComponent<SHMUPDeathController>().StartCoroutine("OnPlayerDeath");
        }*/

    }
    void FixedUpdate(){
        playerRB.MovePosition(playerRB.position + movement * playerSpeed * Time.fixedDeltaTime);
    }
    void UpdateMovementandLook(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //angle due to the character looking at a positive value for the 4th quadrant, therefore it has to be multipied by 3, then added through the angle it looks for the crosshair
        fixedRotation = crosshairLocation.position - transform.position;
        angle = Mathf.Atan2(fixedRotation.y, fixedRotation.x);
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, 3*defaultAngle+(angle * Mathf.Rad2Deg));

    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Asteroid")){
            GameObject death = GameObject.Find("DeathController");
            death.GetComponent<SHMUPDeathController>().StartCoroutine("OnPlayerDeath");
        }
    }

    IEnumerator ShootLaser(){
        yield return new WaitForSeconds(shootingSpeed);
        firedWeaponSound.GetComponent<AudioSource>().Play();
        GameObject firedLaser = (GameObject) Instantiate(laser,transform.position,transform.rotation);
        //to make the vector constant, the rotation is divided by magnitude and then multipled by speed;
        firedLaser.GetComponent<Rigidbody2D>().velocity = 
        new Vector2((fixedRotation.x/fixedRotation.magnitude)*laserSpeed,(fixedRotation.y/fixedRotation.magnitude)*laserSpeed);
        StartCoroutine("ShootLaser");
    }
}
