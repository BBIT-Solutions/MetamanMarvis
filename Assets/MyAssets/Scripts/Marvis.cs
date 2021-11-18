using System.Collections;
using System.Collections.Generic;
using Facebook.WitAi.Lib;
using UnityEngine;
using Asyncoroutine;

public class Marvis : MonoBehaviour {

    

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
    }

    public IEnumerator Say(AudioClip toSay, bool listenAfterwards = false){

//TODO: maybe somehow check if not playing currently?! ... or handle that by the caller?!

        audioSource.clip = toSay;
        audioSource.Play();
        
        while(audioSource.isPlaying){
            yield return null;
        }

        if(listenAfterwards){
            voiceListener.Activate();
//TODO: let a timeout run or so, if nothing happens
        }

    }

//TODO: ensure, they are all not visible at begin.... maybe make a component for them (for each one, or one with string or so for all)
    public IEnumerator RevealElements(RevealableElement.RevealableElementTag[] elementTags){
        for(int i=0; i<elementTags.Length; i++){            
            yield return new WaitForSeconds(2f);
            
            RevealableElement[] revEls = FindObjectsOfType<RevealableElement>(true);
            for(int j=0; j<revEls.Length; j++){
                if(revEls[j].revealableElementTag == elementTags[i]){
                    revEls[j].Reveal();
                }
            }

        }
    }


    public void HearConfirmation(){
        FindObjectOfType<DebugText>().Show("Conformation");
        GameStateMachine.Instance.SetStateForYes();
    }

    public void HearNegotiation(){
        FindObjectOfType<DebugText>().Show("Negotiation");
        GameStateMachine.Instance.SetStateForNo();
    }

    public void HearRepeatRequest(){
        FindObjectOfType<DebugText>().Show("RepeatRequest");
        GameStateMachine.Instance.SetStateForRepeat();
    }
    

    private void HearOutOfScope(){
        FindObjectOfType<DebugText>().Show("OutOfScope ... did not understand");
        GameStateMachine.Instance.HandleOutOfScope();
    }

    public void HearNothing(){
//TODO: implement this case
        GameStateMachine.Instance.SetStateForNoInteraction();

    }


    public void HandleGeneralResponse(WitResponseNode node){
        if(node["intents"] == null || node["intents"].Count == 0){
            HearOutOfScope();
        }
    }



}
