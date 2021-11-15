using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    private static LevelController instance;
    public static LevelController Instance{ get{ return instance; }}

    private int currentLevel;
    public int CurrentLevel{
        get{
            return currentLevel;
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
        currentLevel = 1;

        Debug.Log("LevelController started");
    }
    
}
