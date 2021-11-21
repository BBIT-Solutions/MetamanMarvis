using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSoundProblemWarning : MonoBehaviour {
    
    [SerializeField] GameObject soundProblemWarning;
    
    private bool thereIsASoundProblem;

    void Start() {
        soundProblemWarning.SetActive(false);
        thereIsASoundProblem = true;
        StartCoroutine(ShowDelayed());
    }

    IEnumerator ShowDelayed(){
        yield return new WaitForSeconds(27f);

        if(thereIsASoundProblem){
            soundProblemWarning.SetActive(true);
        }

    }

    public void SoundProblemFixed(){ // //as soon as we get anything, we can consider it as "fixed" for this experience
        thereIsASoundProblem = false;
        soundProblemWarning.SetActive(false);
    }


}
