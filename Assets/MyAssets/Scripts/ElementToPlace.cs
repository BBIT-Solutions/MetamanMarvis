using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementToPlace : MonoBehaviour {

    [SerializeField] ElementsCorrectPosition correctPosition;

    [SerializeField] Material materialStart;
    [SerializeField] Material materialHint;
    [SerializeField] Material materialSolved;


    [SerializeField] GameStateMachine.StateCanBeSolvedBy onGrabbedTag;
    [SerializeField] GameStateMachine.StateCanBeSolvedBy onCloseToHintTag;
    [SerializeField] GameStateMachine.StateCanBeSolvedBy onSolvedTag;


    //TODO: make them serializable?
    float thresholdPosition = 0.05f; //meters
    float thresholdRotation = 5.0f; //degrees



    MeshRenderer meshRenderer;
    OVRGrabbable grabbable;

    bool atCorrectEndPosition;

    private bool isSolved;
    public bool IsSolved{
        get{
            return isSolved;
        }
    }


    void Awake() {
        isSolved = false;
        atCorrectEndPosition = false;
        meshRenderer = GetComponent<MeshRenderer>();
        grabbable = GetComponent<OVRGrabbable>();
        meshRenderer.material = materialStart;
        grabbable.grabEnded += Solved;
    }


    void Update() {

        if(isSolved) return;
         
        if(grabbable.isGrabbed || Input.GetKeyDown(KeyCode.C)){   //Keycode to DEBUG:
            Debug.Log("check " + gameObject.name);

            if(onGrabbedTag != GameStateMachine.StateCanBeSolvedBy.NONE){
                GameStateMachine.onStateSolved?.Invoke(onGrabbedTag);
                onGrabbedTag = GameStateMachine.StateCanBeSolvedBy.NONE; //to ensure only trigger once //TODO: improve that maybe (if referenced by value?!)...then use bools instead
            }


            if(CheckEndPosition()){
                meshRenderer.material = materialHint;
                
                //TODO: play "exiting/tensioning" sound (if not started yet)
                
                correctPosition.Hide();
                atCorrectEndPosition = true;
                Debug.Log("HINT!!!");


            }
            else{
                meshRenderer.material = materialStart;
                correctPosition.Show();
                atCorrectEndPosition = false;
            }

            
        }

        
        if(Input.GetKeyDown(KeyCode.S)){ //to DEBUG:
            Solved();
        }

    }

    public bool CheckEndPosition(){

        if(!correctPosition.gameObject.activeInHierarchy) return false; //only allow checking, if the correct position is already revealed


        if(CheckPosition() && CheckRotation()){    

            if(onCloseToHintTag != GameStateMachine.StateCanBeSolvedBy.NONE){
                GameStateMachine.onStateSolved?.Invoke(onCloseToHintTag);
                onCloseToHintTag = GameStateMachine.StateCanBeSolvedBy.NONE; //to ensure only trigger once //TODO: improve that maybe (if referenced by value?!)...then use bools instead
            }

            return true;
        }

        return false;
    }

    private void Solved(){

        if(!atCorrectEndPosition) return;
        if(isSolved) return;

        isSolved = true;
        Debug.Log("solved ... " + gameObject.name);

        meshRenderer.material = materialSolved;

        Destroy(GetComponent<OVRGrabbable>()); // .enabled = false;
        //TODO: also destroy rigidbody?! 


        LeanTween.rotate(gameObject, correctPosition.gameObject.transform.rotation.eulerAngles, 0.15f).setEaseOutQuad();
        LeanTween.move(gameObject, correctPosition.gameObject.transform.position, 0.3f).setEaseOutQuad().setOnComplete( () => {
            transform.SetParent(correctPosition.transform.parent);
            Destroy(correctPosition.gameObject);
        });
        //TODO: add sound and particle system for the snap-effect

        if(onSolvedTag != GameStateMachine.StateCanBeSolvedBy.NONE){
            GameStateMachine.onStateSolved?.Invoke(onSolvedTag);
            onSolvedTag = GameStateMachine.StateCanBeSolvedBy.NONE; //to ensure only trigger once //TODO: improve that maybe (if referenced by value?!)...then use bools instead
        }

    }


    private bool CheckPosition(){
        return ( Vector3.Distance(transform.position, correctPosition.gameObject.transform.position) < thresholdPosition ); 
    }

    private bool CheckRotation(){
        return (Quaternion.Angle(transform.rotation, correctPosition.gameObject.transform.rotation) < thresholdRotation );
    }


}
