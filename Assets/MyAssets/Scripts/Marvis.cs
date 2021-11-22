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

    [SerializeField] AudioClip[] notUnderstandPhrases;
    private int currentNotUnderstandPhrase;
    
    private bool isInitializing;
    public bool IsInitializing{
        get{
            return isInitializing;
        }
    }


    void Awake() {
        audioSource = GetComponent<AudioSource>();
        currentNotUnderstandPhrase = 0;
        isInitializing = true;
    }

    void Start() {
        voiceListener = FindObjectOfType<Oculus.Voice.AppVoiceExperience>();

        StartCoroutine(Init());
    }

    IEnumerator Init(){
        //because the very first listening always is not working correctly, we do one "fake" listening to init the system

        //TODO: show a loading screen/bar or the logo or something similar in the meantime
                //--> e.g. a sign which explains what M.A.R.V.I.S. means

        voiceListener.ActivateImmediately();

        yield return new WaitForSeconds(1f);

        voiceListener.Deactivate();
    }


    public IEnumerator Say(AudioClip toSay, bool listenAfterwards = false){

        audioSource.clip = toSay;
        audioSource.Play();
        
        while(audioSource.isPlaying){
            yield return null;
        }

        if(listenAfterwards){
            WaitForAnswer();
        }

    }

    public IEnumerator RevealElements(RevealableElement.RevealableElementTag[] elementTags){ //TODO: maybe make a flag, if (when multiple) they should be revealed together or step by step
        for(int i=0; i<elementTags.Length; i++){         
            

            if(elementTags.Length > 1){   
                yield return new WaitForSeconds(2f);
            }
            
            RevealableElement[] revEls = FindObjectsOfType<RevealableElement>(true);
            for(int j=0; j<revEls.Length; j++){
                if(revEls[j].revealableElementTag == elementTags[i]){
                    //if(revEls.Length > 1){   //HINT: this would allow, to reveal also elements of the same tag step by step (but probably not needed)
                    //    yield return new WaitForSeconds(2f);
                    //}
                    revEls[j].Reveal();
                }
            }

        }
    }

    public IEnumerator SayNotUnderstandPhrase(){

        audioSource.clip = notUnderstandPhrases[currentNotUnderstandPhrase];
        audioSource.Play();

        while(audioSource.isPlaying){
            yield return null;
        }

        currentNotUnderstandPhrase = (currentNotUnderstandPhrase + 1) % notUnderstandPhrases.Length;
    }

    public void WaitForAnswer(){
        voiceListener.Activate();
        //TODO: let a timeout run or so, if nothing happens
    }


    public void HearConfirmation(){
        if(isInitializing){
            isInitializing = false;
            return;
        }

        FindObjectOfType<DebugText>().Show("Conformation");
        GameStateMachine.Instance.SetStateForYes();
        FindObjectOfType<ShowSoundProblemWarning>().SoundProblemFixed();
    }

    public void HearNegotiation(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        FindObjectOfType<DebugText>().Show("Negotiation");
        GameStateMachine.Instance.SetStateForNo();
        FindObjectOfType<ShowSoundProblemWarning>().SoundProblemFixed();
    }

    public void HearRepeatRequest(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        FindObjectOfType<DebugText>().Show("RepeatRequest");
        GameStateMachine.Instance.SetStateForRepeat();
        FindObjectOfType<ShowSoundProblemWarning>().SoundProblemFixed();
    }
    

    private void HearOutOfScope(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        FindObjectOfType<DebugText>().Show("OutOfScope ... did not understand");
        GameStateMachine.Instance.HandleOutOfScope();
        FindObjectOfType<ShowSoundProblemWarning>().SoundProblemFixed();
    }

    public void HearNothing(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        //TODO: implement this case
        GameStateMachine.Instance.SetStateForNoInteraction();

    }


    public void HandleGeneralResponse(WitResponseNode node){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        if(node["intents"] == null || node["intents"].Count == 0){
            HearOutOfScope();
        }
    }



    public void HearHint(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        FindObjectOfType<DebugText>().Show("Hint");
        if(GameStateMachine.Instance.CurrentState.canReactOnIntentHint){
            GameStateMachine.Instance.SetStateForHint();
        }
        else{
            HearOutOfScope();
        }
    }

    public void HearOrder(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        FindObjectOfType<DebugText>().Show("Order");
        if(GameStateMachine.Instance.CurrentState.canReactOnIntentOrder){
            GameStateMachine.Instance.SetStateForOrder();
        }
        else{
            HearOutOfScope();
        }
    }

    public void HearReady(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        FindObjectOfType<DebugText>().Show("Ready");
        if(GameStateMachine.Instance.CurrentState.canReactOnIntentReady){
            GameStateMachine.Instance.SetStateForReady();
        }
        else{
            HearOutOfScope();
        }
    }

    public void HearOpenDoor(){
        if(isInitializing){
            isInitializing = false;
            return;
        }
        
        FindObjectOfType<DebugText>().Show("OpenDoor");
        if(GameStateMachine.Instance.CurrentState.canReactOnIntentOpenDoor){
            GameStateMachine.Instance.SetStateForOpenDoor();
        }
        else{
            HearOutOfScope();
        }
    }


}
