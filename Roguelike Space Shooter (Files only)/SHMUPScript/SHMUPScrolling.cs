using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPScrolling : MonoBehaviour
{
    private float rotation;
    void Start (){
        //rotation and change to make a scrolling effect
        rotation = gameObject.GetComponentInParent<Transform>().eulerAngles.x;
    }
    void Update(){
        Vector3 currentPos = gameObject.transform.position;
        Vector3 changePos = 
        new Vector3(currentPos.x ,currentPos.y + .004f,currentPos.z + .004f);
        gameObject.transform.position = changePos;
    }
}
