using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVoiceActivation : MonoBehaviour {

    [SerializeField] Oculus.Voice.AppVoiceExperience voiceExperience;
    bool isRecording;

    void Awake() {
        isRecording = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            Toggle();
        }
    }


    public void Toggle(){
        if(!isRecording){
            isRecording = true;
            voiceExperience.Activate();
        }
        else{
            isRecording = false;
            voiceExperience.Deactivate();
        }

    }
}
