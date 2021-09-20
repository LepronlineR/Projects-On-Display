using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SHMUPAudioController : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusicplayer;
    public AudioClip[] backgroundMusic;

    // Update is called once per frame
    void Start(){
        backgroundMusicplayer.loop = true;
        //each stage is their stage number, i.e. stage 1 is 1.0f, and the boss for the stages are that with .5f at the end. 
        //Except for the last boss part 2 and final boss
    }

    public void ChangeBGM(float newStage){
        backgroundMusicplayer.clip = BGMClip(newStage);
        backgroundMusicplayer.Play();
    }

    private AudioClip BGMClip(float s){
        //set BGM
        switch(s){
            case 1.0f:
                return backgroundMusic[0];
            case 1.5f:
                return backgroundMusic[1];
            case 2.0f:
                return backgroundMusic[2];
            case 2.5f:
                return backgroundMusic[3];
            case 3.0f:
                return backgroundMusic[4];
            case 3.5f:
                return backgroundMusic[5];
            case 4.0f:
                return backgroundMusic[6];  
            case 5.0f:
                return backgroundMusic[7];
        }
        return null;
    }
}
