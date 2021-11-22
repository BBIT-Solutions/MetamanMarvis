using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStateSO : ScriptableObject {

    public AudioClip audioClipToSay;

    public GameStateSO onRepeat;
    public GameStateSO onNoInteraction;
    public GameStateSO onYes;
    public GameStateSO onNo;
    public GameStateSO onHint;
    public GameStateSO onOrder;
    public GameStateSO onReady;
    public GameStateSO onOpenDoor;

    public RevealableElement.RevealableElementTag[] elementsToReveal;

    public bool canBeSolvedByAnAction;
    public GameStateMachine.StateCanBeSolvedBy canBeSolvedByTag;
 
    public bool isAFinalState;

    public bool canReactOnIntentHint;
    public bool canReactOnIntentOrder;
    public bool canReactOnIntentReady;
    public bool canReactOnIntentOpenDoor;


    [Tooltip("next state, which can be reached by solving an action or completely without any voice interaction")]
    public GameStateSO nextState;
}

