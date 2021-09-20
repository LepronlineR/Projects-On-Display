using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPGpoint : MonoBehaviour
{
    [SerializeField] string requiredConnection;
    [SerializeField] GameObject parentObject;
    private GameObject template;
    private int random;
    private bool started;
    private List<GameObject> tempGameObject;
    private float timeCount;
    void Awake(){
        timeCount = 4.0f;
        template = GameObject.Find("Template");
        tempGameObject = template.GetComponent<SHMUPTemplate>().Rooms;
    }
    void Start(){
        Destroy(gameObject, timeCount);
        started = false;
        Invoke("Create",0.1f);
    }
    // Update is called once per frame
    void Create(){
        /*foreach(GameObject g in tempGameObject){
            Debug.Log(g.name);
        }*/
        if(!started && !requiredConnection.Equals("Main")){
            List<GameObject> listedRooms = new List<GameObject>();
            if(requiredConnection.Equals("Top")){
                foreach(GameObject g in tempGameObject){
                    if(g.gameObject.name.Contains("Bottom"))
                        listedRooms.Add(g);
                }
            } else if(requiredConnection.Equals("Bottom")){
                foreach(GameObject g in tempGameObject){
                    if(g.gameObject.name.Contains("Top"))
                        listedRooms.Add(g);
                }
            } else if(requiredConnection.Equals("Right")){
                foreach(GameObject g in tempGameObject){
                    if(g.gameObject.name.Contains("Left"))
                        listedRooms.Add(g);
                }
            } else if(requiredConnection.Equals("Left")){
                foreach(GameObject g in tempGameObject){
                    if(g.gameObject.name.Contains("Right")){
                        listedRooms.Add(g);
                    }
                }
            }
            /*foreach(GameObject g in listedRooms){
                Debug.Log(g.name);
            }*/
            random = Random.Range(0,listedRooms.Count);
            started = false;
            GameObject roomMade = (GameObject) Instantiate(listedRooms[random],transform.position,listedRooms[random].transform.rotation);
            template.GetComponent<SHMUPTemplate>().totalRooms.Add(roomMade);
            GameObject controllerObject = GameObject.Find("RoomController");
            roomMade.transform.parent = controllerObject.transform;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag.Equals("Gpoint")){
            template.GetComponent<SHMUPTemplate>().totalRooms.Remove(gameObject);
            Destroy(gameObject);
            started = true;
        }
    }
}
