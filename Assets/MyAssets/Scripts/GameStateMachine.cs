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
        PlayCurrentState();
    }

    public async void PlayCurrentState(){
        Debug.Log("Play current state: " + currentState.name);
        if(currentState.nextWithoutCondition != null){
            await marvis.Say(currentState.audioClipToSay);
            await new WaitForSeconds(1f);
            SetStateForNextWithoutCondition();
        }
        else{ //CAUTION: be sure to leave the nextWithoutCondition field None, when you need a interaction
            Debug.Log("and wait for answer afterwards...");
            await marvis.Say(currentState.audioClipToSay, true);
            //HINT: no need to await here for extra seconds, because after you said something, the wit response anyhow need a quick moment to process, which is enough for a "realistic" conversation
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
    
    private void SetStateForNextWithoutCondition(){
        SetNextState(currentState.nextWithoutCondition);
    }
    
}
