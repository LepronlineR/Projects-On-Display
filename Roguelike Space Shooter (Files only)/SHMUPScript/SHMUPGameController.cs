using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SHMUPGameController : MonoBehaviour
{
    public List<GameObject> enemiesOnScreen = new List<GameObject>();
    public GameObject[] bosses;
    public GameObject[] backgrounds;
    public GameObject bossRoom;
    public float stage;
    public bool getStage;

    [SerializeField] GameObject mainRoom;
    [SerializeField] GameObject menu;

    private GameObject template;
    private List<GameObject> allRooms;

    private GameObject player;
    private GameObject audioController;
    private GameObject roomController;

    private Transform originalPos;
    private GameObject mainR1;
    private GameObject mainR2;
    private GameObject mainR3;

    private GameObject asteroidPool;
    private List<Transform> asteroidChildren;
    private GameObject powerupPool;
    private List<Transform> powerupChildren;

    private GameObject bossTeleporter;

    void Awake(){
        //get all children from asteroid pool and powerup pool and then destroy them after get into the boss level.
        asteroidPool = GameObject.Find("AsteroidPool");
        powerupPool = GameObject.Find("PowerUpPool");

        roomController = GameObject.Find("RoomController");
        audioController = GameObject.Find("AudioController");
        originalPos = GameObject.Find("OriginalPosition").transform;

        getStage = false;
        template = GameObject.Find("Template");
        player = GameObject.Find("Player");
        foreach(GameObject g in backgrounds){
            g.SetActive(false);
        }
        /* setup a stage:
            - set the right background
            - set the right music
            - load in the template
            - return player to original position (0,0)
            - instantiate a main room
            - Unload a stage first and then load the stage

        */
    }

    void Start(){
        stage = 1.0f;
    }

    public void CloseMenu(){
        menu.SetActive(false);
    }

    public void Menu(){
        SceneManager.LoadScene(6);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            //enter the menu
            menu.SetActive(true);
        }
        switch(stage){
            case 1.0f:
                if(!getStage){
                    getStage = true;
                    UnLoadStage(stage);
                    LoadStage(stage);
                }
                break;
            case 1.5f:
                if(!getStage){
                    getStage = true;
                    UnLoadStage(stage);
                    LoadStage(stage);
                }
                break;
            case 2.0f:
                if(!getStage){
                    getStage = true;
                    UnLoadStage(stage);
                    LoadStage(stage);
                }
                break;
            case 2.5f:
                if(!getStage){
                    getStage = true;
                    UnLoadStage(stage);
                    LoadStage(stage);
                }
                break;
            case 3.0f:
                if(!getStage){
                    getStage = true;
                    UnLoadStage(stage);
                    LoadStage(stage);
                }
                break;
            case 3.5f:
                if(!getStage){
                    getStage = true;
                    UnLoadStage(stage);
                    LoadStage(stage);
                }
                break;
            case 4.0f:
                if(!getStage){
                    getStage = true;
                    UnLoadStage(stage);
                    LoadStage(stage);
                }
                break;
            case 4.5f: //win
                player.GetComponent<SHMUPplayercontroller>().SaveHighScore();
                SceneManager.LoadScene(8);
                break;
        }
    }

    void EliminateRooms(){
        template.GetComponent<SHMUPTemplate>().DestroyAllRooms();
    }

    void GetAsteroidChildren(){
        int numChild = asteroidPool.transform.childCount;
        asteroidChildren = new List<Transform>();
        for(int x = 0; x<numChild; x++){
            asteroidChildren.Add(asteroidPool.gameObject.transform.GetChild(x));
        }
    }

    void EliminateAsteroids(){
        foreach(Transform t in asteroidChildren){
            DestroyImmediate(t.gameObject,true);
        }
    }

    void GetPowerUpChildren(){
        int numChild = powerupPool.transform.childCount;
        powerupChildren = new List<Transform>();
        for(int x = 0; x<numChild; x++){
            powerupChildren.Add(powerupPool.gameObject.transform.GetChild(x));
        }
    }

    void EliminatePowerup(){
        foreach(Transform t in powerupChildren){
            DestroyImmediate(t.gameObject,true);
        }
    }

    void LoadStage(float stage){
        switch(stage){
            case 1.0f: //stage 1
                template.GetComponent<SHMUPTemplate>().RestartTemplate();
                mainR1 = (GameObject) Instantiate(mainRoom,originalPos.transform.position+roomController.transform.position,Quaternion.identity);
                mainR1.transform.parent = roomController.transform;
                audioController.GetComponent<SHMUPAudioController>().ChangeBGM(stage);
                backgrounds[0].SetActive(true);
                player.transform.position = originalPos.position;
                break;
            case 1.5f: //stage 1 boss
                bossRoom.SetActive(true);
                template.GetComponent<SHMUPTemplate>().DestroyBossTele();
                audioController.GetComponent<SHMUPAudioController>().ChangeBGM(stage);
                backgrounds[0].SetActive(true);
                player.transform.position = originalPos.position;
                StartCoroutine(SpawnBoss(bosses[0],new Vector3(0.0f,3.0f,0.0f)));
                break;
            case 2.0f: //stage 2
                template.GetComponent<SHMUPTemplate>().RestartTemplate();
                mainR2 = (GameObject) Instantiate(mainRoom,originalPos.transform.position+roomController.transform.position,Quaternion.identity);
                mainR2.transform.parent = roomController.transform;
                audioController.GetComponent<SHMUPAudioController>().ChangeBGM(stage);
                backgrounds[1].SetActive(true);
                player.transform.position = originalPos.position;
                break;
            case 2.5f: //stage 2 boss
                bossRoom.SetActive(true);
                template.GetComponent<SHMUPTemplate>().DestroyBossTele();
                audioController.GetComponent<SHMUPAudioController>().ChangeBGM(stage);
                backgrounds[1].SetActive(true);
                player.transform.position = originalPos.position;
                StartCoroutine(SpawnBoss(bosses[1],new Vector3(0.0f,3.0f,0.0f)));
                break;
            case 3.0f: //stage 3
                template.GetComponent<SHMUPTemplate>().RestartTemplate();
                mainR3 = (GameObject) Instantiate(mainRoom,originalPos.transform.position+roomController.transform.position,Quaternion.identity);
                mainR3.transform.parent = roomController.transform;
                audioController.GetComponent<SHMUPAudioController>().ChangeBGM(stage);
                backgrounds[2].SetActive(true);
                player.transform.position = originalPos.position;
                break;
            case 3.5f: //stage 3 boss
                bossRoom.SetActive(true);
                template.GetComponent<SHMUPTemplate>().DestroyBossTele();
                audioController.GetComponent<SHMUPAudioController>().ChangeBGM(stage);
                backgrounds[2].SetActive(true);
                player.transform.position = originalPos.position;
                StartCoroutine(SpawnBoss(bosses[2],new Vector3(0.0f,3.0f,0.0f)));
                break;
            case 4.0f: //stage 3 boss part 2
                bossRoom.SetActive(true);
                audioController.GetComponent<SHMUPAudioController>().ChangeBGM(stage);
                backgrounds[2].SetActive(true);
                StartCoroutine(SpawnBoss(bosses[3],new Vector3(0.0f,3.0f,0.0f)));
                break;
        }
    }
    void UnLoadStage(float stage){
        switch(stage){
            case 1.0f:
                bossRoom.SetActive(false);
                backgrounds[0].SetActive(false);
                break;
            case 1.5f:
                GetAsteroidChildren();
                EliminateAsteroids();
                GetPowerUpChildren();
                EliminatePowerup();
                EliminateRooms();
                backgrounds[0].SetActive(false);
                DestroyImmediate(mainR1,true);
                break;
            case 2.0f:
                bossRoom.SetActive(false);
                backgrounds[1].SetActive(false);
                break;
            case 2.5f:
                GetAsteroidChildren();
                EliminateAsteroids();
                GetPowerUpChildren();
                EliminatePowerup();
                EliminateRooms();
                backgrounds[1].SetActive(false);
                DestroyImmediate(mainR2,true);
                break;
            case 3.0f:
                bossRoom.SetActive(false);
                backgrounds[2].SetActive(false);
                break;
            case 3.5f:
                GetAsteroidChildren();
                EliminateAsteroids();
                GetPowerUpChildren();
                EliminatePowerup();
                EliminateRooms();
                backgrounds[2].SetActive(false);
                DestroyImmediate(mainR3,true);
                break;
            case 4.0f:
                break;
        }
    }

    IEnumerator SpawnBoss(GameObject boss,Vector3 designatedPos){
        yield return new WaitForSeconds(0.5f);
        Instantiate(boss,designatedPos,Quaternion.identity);
    }
}