using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Asyncoroutine;

public class GameStateMachine : MonoBehaviour {

    private static GameStateMachine instance;
    public static GameStateMachine Instance{ get{ return instance; }}

    [SerializeField] GameStateSO stateToStartWith; //HINT: to Debug a later state immediately, change that tmp
    
    Marvis marvis;


    private GameStateSO currentState;
    public GameStateSO CurrentState{
        get{
            return currentState;
        }
    }

    void Awake(){
        if( (instance != null) && (instance != this) ){
            Destroy(this.gameObject);
        } else{
            instance = this;
            instance.Init();
        }        
    }

    private void Init(){ //Singletons Awake
        currentState = stateToStartWith;        
        Debug.Log("GameStateMachine started");
    }

     void Start(){
        marvis = FindObjectOfType<Marvis>();
        StartCoroutine(StartDelayed());
    }

    IEnumerator StartDelayed(){
        yield return new WaitForSeconds(3f); //To avoid user hears the voice immediately on game-start
        PlayCurrentState();
    }

    public async void PlayCurrentState(){
        Debug.Log("Play current state: " + currentState.name);
        if(currentState.nextWithoutCondition != null){
            await marvis.Say(currentState.audioClipToSay);
Debug.Log("told");
            await new WaitForSeconds(1f); //wait before contuniung with the next state
Debug.Log("waited one second");


            if(currentState.elementsToReveal != null && currentState.elementsToReveal.Length > 0){
Debug.Log("elements to Reveal is NOT null and not empty");
                await marvis.RevealElements(currentState.elementsToReveal);
                await new WaitForSeconds(5f);
//TODO: make this Waiting time adpatable?!.... or actually it anyhow waits internally?!
            }



Debug.Log("set the next state");
            SetStateForNextWithoutCondition();
Debug.Log("next state setted");
        }
        else{ //CAUTION: be sure to leave the nextWithoutCondition field None, when you need a interaction
            Debug.Log("and wait for answer afterwards...");
            await marvis.Say(currentState.audioClipToSay, !currentState.isAFinalState);
            //HINT: no need to await here for extra seconds, because after you said something, the wit response anyhow need a quick moment to process, which is enough for a "realistic" conversation
       
            //TO DEBUG: and maybe later TODO:    handle this case and quit the app or so... 
            if(currentState.isAFinalState){       
                Debug.Log("---------------- Final State reached ----------------");
            }
       
        }
    }

    private void SetNextState(GameStateSO nextState){
        currentState = nextState;
        PlayCurrentState();
    }
   
    public void SetStateForRepeat(){
        SetNextState(currentState.onRepeat);
    }
    
    public void SetStateForNoInteraction(){
        SetNextState(currentState.onNoInteraction);
    }
    
    public void SetStateForYes(){
        SetNextState(currentState.onYes);
    }
    
    public void SetStateForNo(){
        SetNextState(currentState.onNo);
    }
    
    public async void HandleOutOfScope(){

        await marvis.SayNotUnderstandPhrase();
        //PlayCurrentState();//should work ... if it somewhere should play too much, then just call this instead await marvis.Say(currentState.audioClipToSay, true);
                                        //--> or if you want to repeat automatically.... but user should ask by hisself for the repeat, if he wants to hear it again... 
        marvis.WaitForAnswer();
    }
    
    private void SetStateForNextWithoutCondition(){
        SetNextState(currentState.nextWithoutCondition);
    }
    
}