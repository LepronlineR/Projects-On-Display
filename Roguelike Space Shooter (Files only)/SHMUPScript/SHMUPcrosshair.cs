using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPcrosshair : MonoBehaviour
{
    [SerializeField] GameObject player;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start(){
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        Cursor.visible = false;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        this.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
    void FixedUpdate(){
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag.Equals("Enemy")){
            sprite.color = Color.red;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        sprite.color = Color.white;
    }
}
