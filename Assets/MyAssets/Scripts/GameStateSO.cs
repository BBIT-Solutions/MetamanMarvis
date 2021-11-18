using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStateSO : ScriptableObject {
    //public List<Country> countries;

    public AudioClip audioClipToSay;

    public GameStateSO onRepeat;
    public GameStateSO onNoInteraction;
    public GameStateSO onYes;
    public GameStateSO onNo;

    // public GameObject[] elementsToReveal;
    public RevealableElement.RevealableElementTag[] elementsToReveal;
 
    public bool isAFinalState;


    
//--> //diese hier verlinken.... und auf GO jeweils extra component machen RevealableElement... und da get, bzw. einfach auch public das...
            //dann findallObject of type.... und für das was gleich ist revealen.....
//            -->//und da beim Awake, already revealed auf false setzen...und beim ersten revealen dann auf true...nur einmal zeigen


    //TODO: asset/s to spawn/reveal   (string?!) ... or have it in scene and link the go/prefab there
                //special intent (all as one, or one for each?)
                //alreadyInvoked ... or just compare them....
                        //--> serializable like the County, to be able to connect it?!

    public GameStateSO nextWithoutCondition;
}

/*
[System.Serializable]
public struct Country {
    public string id_iso3;
    public Sprite flag;
}
*/
/*
[System.Serializable]
public struct GameObjectToReveal {
    public GameObject element;
//TODO: e.g. time/delay or so
    //public Sprite flag;
}

*/
