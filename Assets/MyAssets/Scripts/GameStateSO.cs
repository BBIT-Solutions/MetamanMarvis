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

    //TODO: asset/s to spawn/reveal   (string?!) ... or have it in scene and link the go/prefab there
                //special intent (all as one, or one for each?)
                //alreadyInvoked ... or just compare them....

    public GameStateSO nextWithoutCondition;
}

/*
[System.Serializable]
public struct Country {
    public string id_iso3;
    public Sprite flag;
}
*/