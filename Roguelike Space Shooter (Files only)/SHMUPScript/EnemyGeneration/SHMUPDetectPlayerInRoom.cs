using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPDetectPlayerInRoom : MonoBehaviour
{
    private GameObject asteroidSpawn, enemySpawn;
    private GameObject camera;
    private Transform door;
    private Transform[] children;
    public List<GameObject> childOfDoor = new List<GameObject>();
    private GameObject gameController;
    private bool alreadySpawned;


    //when the player is in the room then the doors will close, until all the enemies are defeated, then the doors will open.

    void Start(){
        alreadySpawned = false;
        gameController = GameObject.Find("GameController");
        asteroidSpawn = GameObject.Find("AsteroidSpawner");
        enemySpawn = GameObject.Find("EnemySpawner");
        camera = GameObject.Find("Main Camera");
        //get the child child of object (door), then get every other child of that door (up,down...)
        door = this.gameObject.transform.GetChild(0);
        children = door.GetComponentsInChildren<Transform>();
        foreach(Transform t in children){
            childOfDoor.Add(t.gameObject);
        }
    }

    void Update(){
        if(gameController.GetComponent<SHMUPGameController>().enemiesOnScreen.Count>0){
            foreach(GameObject g in childOfDoor){
                g.SetActive(true);
            }
        } else {
            foreach(GameObject g in childOfDoor){
                g.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!alreadySpawned&&other.tag.Equals("Player")){
            StartCoroutine("Spawn");
        }
    }

    // when the player enters the room, then the spawner's transform will change
    //then it will trigger the spawn for everything. The script is destoryed so it only happens once
    IEnumerator Spawn(){
        alreadySpawned = true;
        yield return new WaitForSeconds(0.25f);
        asteroidSpawn.transform.position = 
        new Vector2(camera.transform.position.x,camera.transform.position.y);
        enemySpawn.transform.position = 
        new Vector2(camera.transform.position.x,camera.transform.position.y);
        asteroidSpawn.GetComponent<SHMUPSpawnAsteroids>().CreateAsteroids();
        enemySpawn.GetComponent<SHMUPSpawnEnemies>().CreateEnemies();
        //destroy only after the room is cleared
        StopCoroutine("Spawn");
    }
}
