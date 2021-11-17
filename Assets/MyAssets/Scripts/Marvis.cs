using System.Collections;
using System.Collections.Generic;
using Facebook.WitAi.Lib;
using UnityEngine;

public class Marvis : MonoBehaviour {

    
    //level handler.... get current level each time...from a state machine or so

    //TODO: maybe.... skip request, go-back request, next request?! --> but can be also handled by the level state machine or so....


//TODO: maybe handle if currently saying or listening and avoid it then? ... or make a queue or so?!

//maybe TODO: let a timer run or so, after the say() ... and if after xyz no voice was recognized...and/or after xyz was nothing appeared...make an active deactivate and say it again or so


    AudioSource audioSource;
    Oculus.Voice.AppVoiceExperience voiceListener;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        voiceListener = FindObjectOfType<Oculus.Voice.AppVoiceExperience>();

//JUST TO DEBUG:
SayQuestion();

    }


    public void SayQuestion(){

//return;

//JUST TO DEBUG:
StartCoroutine(SaySomethingAndActivateListeningAfterwards(audioSource.clip));

    }

    public void SayQuestionRepeating(){
        //(random?) from a list with similar ones...
    }

    public void SayNotUnderstanding(){
        //randomly from different... each time when OutOfScope

            //evtl. noch unterscheiden, wenn nach einer weile nix kommt.... und wenn etwas out of scope kommt
                        //evtl. auch, did you say something.... please turn on your microphone....

    }

    public void SayHint(){
        //randomly....use your voice...just tell me ... usw ...
                        //i will only record your voice after i have asked you something....
    }

    public void HearConfirmation(){
        FindObjectOfType<DebugText>().Show("Conformation");
//JUST TO DEBUG:
SayQuestion();
    }

    public void HearNegotiation(){
        FindObjectOfType<DebugText>().Show("Negotiation");
//JUST TO DEBUG:
SayQuestion();
    }

    public void HearRepeatRequest(){
        FindObjectOfType<DebugText>().Show("RepeatRequest");
//JUST TO DEBUG:
SayQuestion();
    }
    

    private void HearOutOfScope(){
        FindObjectOfType<DebugText>().Show("OutOfScope ... did not understand");
//JUST TO DEBUG:
SayQuestion();
    }

    public void HearNothing(){

    }


    public void HandleGeneralResponse(WitResponseNode node){
        if(node["intents"] == null || node["intents"].Count == 0){
            HearOutOfScope();
        }
    }




    private IEnumerator SaySomethingAndActivateListeningAfterwards(AudioClip clip){
        audioSource.clip = clip;
        audioSource.Play();

        while(audioSource.isPlaying){
            yield return null;
        }

        voiceListener.Activate();
    }



}
