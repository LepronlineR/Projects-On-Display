using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPTemplate : MonoBehaviour
{
    public List<GameObject> Rooms;
    public List<GameObject> totalRooms = new List<GameObject>();
    [SerializeField] GameObject boss;

    private GameObject bossTelelocation;
    private float waitTime;
    private bool createdBoss;

    void Awake(){
        waitTime = 1.0f;
    }

    public void DestroyAllRooms(){
        foreach(GameObject g in totalRooms){
            Destroy(g);
        }
        totalRooms.Clear();
    }

    public void RestartTemplate(){
        waitTime = 1.0f;
        createdBoss = false;
    }

    public void DestroyBossTele(){
        Destroy(bossTelelocation);
    }


    void Update(){

        if(!createdBoss&&waitTime<=0){
            GameObject bossTele = (GameObject) Instantiate(boss, totalRooms[totalRooms.Count-1].
            transform.position, Quaternion.identity);
            bossTelelocation = bossTele;
            createdBoss = true;
        } else {
            waitTime -= Time.deltaTime;
        }
    }
}
