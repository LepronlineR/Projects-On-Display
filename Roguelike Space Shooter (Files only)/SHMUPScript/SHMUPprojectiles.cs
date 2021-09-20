using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPprojectiles : MonoBehaviour
{
    [SerializeField] string projectileAllegiance;
    [SerializeField] Animator laserAnimation;
    private Rigidbody2D rb;

    private bool collided;
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        collided = false;
    }

    // Update is called once per frame
    void Update(){
        if(projectileAllegiance.Equals("player")){}
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(projectileAllegiance.Equals("enemy")&&other.gameObject.tag.Equals("Player")){
            GameObject death = GameObject.Find("DeathController");
            death.GetComponent<SHMUPDeathController>().StartCoroutine("OnPlayerDeath");
        }
        /*if(projectileAllegiance.Equals("player")&&other.gameObject.tag.Equals("Enemy")){

        }*/
        collided = true;
        StartCoroutine("LaserCollider");
    }


    private void OnBecameInvisible() {
        Destroy(this.gameObject);
    }

    IEnumerator LaserCollider(){
        laserAnimation.SetBool("CollisionTrue",collided);
        rb.velocity = new Vector3(0.0f,0.0f,0.0f);
        yield return new WaitForSeconds(0.05f);
        Destroy(this.gameObject);
        yield return null;
    }
}
