using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


//TODO: enum for level state or so.....

//nach und nach nur die nexten bauteile anzeigen.....und oder auch gar nicht und nur, wenn man hilfe braucht oder so....


    void Awake(){
        if( (instance != null) && (instance != this) ){
            Destroy(this.gameObject);
        } else{
            instance = this;
            instance.Init();
        }        
    }

   

    private void Init(){ //Singletons Awake
        //currentState = 1;

        currentState = stateToStartWith;


        Debug.Log("GameStateMachine started");
    }

     void Start(){
        marvis = FindObjectOfType<Marvis>();
        PlayCurrentState();
    }

    public async void PlayCurrentState(){
        if(currentState.nextWithoutCondition != null){
            //await marvis.Say(currentState.audioClipToSay);
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
    
}
