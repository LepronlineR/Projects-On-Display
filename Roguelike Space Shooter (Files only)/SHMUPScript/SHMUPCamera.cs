using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPCamera : MonoBehaviour
{
    [SerializeField] Transform playerLocation;
    [SerializeField] float speed;
    private Vector2 size;

    // Start is called before the first frame update
    void Start(){
        int height = Mathf.RoundToInt(2*Camera.main.orthographicSize);
        int width = Mathf.RoundToInt(height*Camera.main.aspect);
        size = new Vector2(width,height);
        transform.position = new Vector3(playerLocation.position.x,playerLocation.position.y,transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 pos = new Vector3(Mathf.RoundToInt(playerLocation.position.x/size.x)*size.x,
        Mathf.RoundToInt(playerLocation.position.y/size.y)*size.y,transform.position.z);
        //Linearly interpolates the camera from the given position to a new position at the player. 
        transform.position = Vector3.Lerp(transform.position, pos, speed);
    }
}
