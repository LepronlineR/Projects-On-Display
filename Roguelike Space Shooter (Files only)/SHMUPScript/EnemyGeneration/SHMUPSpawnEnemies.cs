using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPSpawnEnemies : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] chaser;

    private GameObject gameController;
    private int rand; 

    void Start(){
        gameController = GameObject.Find("GameController");
    }
    public void CreateEnemies(){
        /*for the chaser, each three has a different spawn method, the first chaser
        will be based on a 50/50 for all spawn points. The second will choose two random spawn points and spawn
        and the final will spawn in a random spawn point

        Finally after creating the enemy it is added to the enemy count
        */
        rand = Random.Range(0,3);
        //Debug.Log(rand);
        switch(rand){
            case 0:
                //Debug.Log("case0");
                for(int x = 0; x<spawnPoints.Length; x++){
                    int randomC = Random.Range(0,2);
                    //Debug.Log(randomC);
                    if(randomC==1){
                        GameObject enemyCreated = (GameObject) Instantiate(chaser[rand],spawnPoints[x].position,Quaternion.identity);
                        gameController.GetComponent<SHMUPGameController>().enemiesOnScreen.Add(enemyCreated);
                    }
                }
                break;
            case 1:
                //Debug.Log("case1");
                int random1 = Random.Range(0,spawnPoints.Length);
                int random2 = Random.Range(0,spawnPoints.Length);
                while(random1==random2){
                    random2 = Random.Range(0,spawnPoints.Length);
                }
                GameObject enemyCreated1 = (GameObject) Instantiate(chaser[rand], spawnPoints[random1].position, Quaternion.identity);
                GameObject enemyCreated2 = (GameObject) Instantiate(chaser[rand], spawnPoints[random2].position, Quaternion.identity);
                gameController.GetComponent<SHMUPGameController>().enemiesOnScreen.Add(enemyCreated1);
                gameController.GetComponent<SHMUPGameController>().enemiesOnScreen.Add(enemyCreated2);
                break;

            case 2:
                //Debug.Log("case2");
                int random = Random.Range(0,spawnPoints.Length);
                GameObject enemyCreated3 = (GameObject)Instantiate(chaser[rand], spawnPoints[random].position, Quaternion.identity);
                gameController.GetComponent<SHMUPGameController>().enemiesOnScreen.Add(enemyCreated3);
                break;
        }
    }

}
