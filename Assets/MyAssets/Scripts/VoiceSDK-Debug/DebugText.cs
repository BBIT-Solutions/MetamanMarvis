using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugText : MonoBehaviour {

    TMPro.TextMeshProUGUI textField;

    void Awake() {
        textField = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void MicLevelChanged(float single){
        // string toShow = "MicLevelChanged: " + single.ToString("D8");
        string toShow = "MicLevelChanged: " + single.ToString("0.0000000000");
        Show(toShow);
    }

    public void StartListening(){
        string toShow = "Start Listening";
        Show(toShow);        
    }
    
    public void StoppedListening(){
        string toShow = "Stopped Listening";
        Show(toShow);
    }

    public void StoppedListeningDueToInactivity(){
        string toShow = "Stopped Listening Due To Inactivity";
        Show(toShow);
    }
    
    public void StoppedListeningDueToTimeout(){
        string toShow = "Stopped Listening Due To Timeout";
        Show(toShow);
    }

    public void StoppedListeningDueToDeactivation(){
        string toShow = "Stopped Listening Due To Deactivation";
        Show(toShow);
    }
    
    public void MicDataSent(){
        string toShow = "Mic Data Sent";
        Show(toShow);
    }
    
    public void MinimumWakeThreshold(){
        string toShow = "Minimum Wake Threshold";
        Show(toShow);
    }


    public void PartialTranscription(string trans){
        string toShow = "PartialTranscription: " + trans;
        Show(toShow);
    }

    public void FullTranscription(string trans){
        string toShow = "FullTranscription: " + trans;
        Show(toShow);
    }


//TODO: die threshold hit und mic data sent auch noch alle... evtl. und an anderen Stellen zeigen...




    private void Show(string textToShow){
        Debug.Log(textToShow);        
        textField.text = textToShow;
    }

}
