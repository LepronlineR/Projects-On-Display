using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SHMUPDeathController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject explosionSound;
    public GameObject[] explosions;
    public GameObject[] playerShipPieces;

    private GameObject gameController;
    private Animator animation;
    private Color color;
    private Renderer render;

    void Awake(){
        gameController = GameObject.Find("GameController");
    }

    // once the player or enemy dies, it will explode, set thre objects in a random three directions based on their trajectory, then everything is destroyed
    // if it is the player, then they will return with invicibility for a short time
    IEnumerator OnPlayerDeath(){
        //return time back to normal after the player dies
        Time.timeScale = 1.0f;
        player.GetComponent<SHMUPplayercontroller>().playerLife--;
        player.SetActive(false);
        int random = Random.Range(0,explosions.Length);
        animation = explosions[random].GetComponent<Animator>();
        //set parent to audio controller and then play
        GameObject explodeSound = (GameObject) Instantiate(explosionSound,transform.position,Quaternion.identity);
        GameObject audioController = GameObject.Find("AudioController");
        explodeSound.transform.parent = audioController.transform;
        explodeSound.GetComponent<AudioSource>().Play();

        GameObject explosion = (GameObject) Instantiate(explosions[random],player.GetComponent<Transform>().position,transform.rotation);
        explosion.GetComponent<Animator>().SetBool("ExplosionTrue",true);
        GameObject topPiece = (GameObject) Instantiate(playerShipPieces[0],player.GetComponent<Transform>().position,transform.rotation);
        GameObject rightPiece = (GameObject) Instantiate(playerShipPieces[1],player.GetComponent<Transform>().position,transform.rotation);
        GameObject leftPiece = (GameObject) Instantiate(playerShipPieces[2],player.GetComponent<Transform>().position,transform.rotation);
        topPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1,1),Random.Range(1,3));
        rightPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1,3),Random.Range(-1,1));
        leftPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,-1),Random.Range(-1,1));
        yield return new WaitForSeconds(3.0f);
        animation = null;
        Destroy(explosion);
        Destroy(topPiece);
        Destroy(rightPiece);
        Destroy(leftPiece);
        //if the player has negative life remaining then they will return back to the gameover screen
        if(player.GetComponent<SHMUPplayercontroller>().playerLife<0){
            SceneManager.LoadScene(9);
        }
        player.SetActive(true);
        render = player.GetComponent<Renderer>();
        color = render.material.color;
        //10 is player collision, 14 is enemy projectile collision, 18 is asteroids
        //set to ignore collision and then change colors to transparent
        //after a few seconds 
        Physics2D.IgnoreLayerCollision(10,14,true);
        Physics2D.IgnoreLayerCollision(10,18,true);
        color.a = 0.5f;
        render.material.color = color;
        yield return new WaitForSeconds(3.0f);
        Physics2D.IgnoreLayerCollision(10,14,false);
        Physics2D.IgnoreLayerCollision(10,18,false);
        color.a = 1.0f;
        render.material.color = color;
        
    }
    public IEnumerator OnEnemyPlaneDeath(GameObject g){
        g.SetActive(false);
        int random = Random.Range(0,explosions.Length);
        animation = explosions[random].GetComponent<Animator>();
        //set parent to audio controller and then play
        GameObject explodeSound = (GameObject) Instantiate(explosionSound,transform.position,Quaternion.identity);
        GameObject audioController = GameObject.Find("AudioController");
        explodeSound.transform.parent = audioController.transform;
        explodeSound.GetComponent<AudioSource>().Play();

        GameObject explosion = (GameObject) Instantiate(explosions[random],g.GetComponent<Transform>().position,transform.rotation);
        explosion.GetComponent<Animator>().SetBool("ExplosionTrue",true);
        GameObject topPiece = (GameObject) Instantiate(playerShipPieces[0],g.GetComponent<Transform>().position,transform.rotation);
        GameObject rightPiece = (GameObject) Instantiate(playerShipPieces[1],g.GetComponent<Transform>().position,transform.rotation);
        GameObject leftPiece = (GameObject) Instantiate(playerShipPieces[2],g.GetComponent<Transform>().position,transform.rotation);
        topPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1,1),Random.Range(1,3));
        rightPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1,3),Random.Range(-1,1));
        leftPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,-1),Random.Range(-1,1));
        yield return new WaitForSeconds(3.0f);
        animation = null;
        Destroy(explosion);
        Destroy(topPiece);
        Destroy(rightPiece);
        Destroy(leftPiece);
        Destroy(g);
    }
    public IEnumerator OnBossDeath(GameObject boss){
        boss.SetActive(false);
        //set parent to audio controller and then play
        GameObject audioController = GameObject.Find("AudioController");

        GameObject topPiece = (GameObject) Instantiate(playerShipPieces[0],boss.GetComponent<Transform>().position,transform.rotation);
        GameObject rightPiece = (GameObject) Instantiate(playerShipPieces[1],boss.GetComponent<Transform>().position,transform.rotation);
        GameObject leftPiece = (GameObject) Instantiate(playerShipPieces[2],boss.GetComponent<Transform>().position,transform.rotation);
        GameObject topPiece2 = (GameObject) Instantiate(playerShipPieces[0],boss.GetComponent<Transform>().position,transform.rotation);
        GameObject rightPiece2 = (GameObject) Instantiate(playerShipPieces[1],boss.GetComponent<Transform>().position,transform.rotation);
        GameObject leftPiece2 = (GameObject) Instantiate(playerShipPieces[2],boss.GetComponent<Transform>().position,transform.rotation);

        topPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1,1),Random.Range(1,3));
        rightPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1,3),Random.Range(-1,1));
        leftPiece.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,-1),Random.Range(-1,1));
        topPiece2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1,1),Random.Range(1,3));
        rightPiece2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1,3),Random.Range(-1,1));
        leftPiece2.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3,-1),Random.Range(-1,1));

        for(int x=0 ; x<4; x++){
            //want more explosions to happen
            Vector3 changePos = new Vector3(Random.Range(0,2),Random.Range(0,2),0.0f);

            int random = Random.Range(0,explosions.Length);
            animation = explosions[random].GetComponent<Animator>();
            GameObject explodeSound = (GameObject) Instantiate(explosionSound,transform.position,Quaternion.identity);
            explodeSound.transform.parent = audioController.transform;
            explodeSound.GetComponent<AudioSource>().Play();
            GameObject explosion = (GameObject) Instantiate(explosions[random],boss.GetComponent<Transform>().position+changePos,transform.rotation);
            explosion.GetComponent<Animator>().SetBool("ExplosionTrue",true);
            yield return new WaitForSeconds(1.5f);
            animation = null;
            Destroy(explosion);
        }

        yield return new WaitForSeconds(6.0f);
        Destroy(topPiece);
        Destroy(rightPiece);
        Destroy(leftPiece);
        Destroy(topPiece2);
        Destroy(rightPiece2);
        Destroy(leftPiece2);
        gameController.GetComponent<SHMUPGameController>().stage+=0.5f;
        gameController.GetComponent<SHMUPGameController>().getStage = false;
        Destroy(boss);
    }
}
