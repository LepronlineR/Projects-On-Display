using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPSpawnAsteroids : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] asteroids;

    private GameObject asteroidPool;

    void Start(){
        asteroidPool = GameObject.Find("AsteroidPool");
    }
    
    public void CreateAsteroids(){
        /* asteroids will spawn randomly for every spawn point
        */
        foreach(Transform t in spawnPoints){
            //choose if this transform should create an asteroid
            int random = Random.Range(0,2);
            if(random==1){
                int randAsteroid = Random.Range(0,asteroids.Length);
                GameObject g = (GameObject) Instantiate(asteroids[randAsteroid],t.position,Quaternion.identity);
                g.transform.parent = asteroidPool.transform;
            }
        }
    }
}
