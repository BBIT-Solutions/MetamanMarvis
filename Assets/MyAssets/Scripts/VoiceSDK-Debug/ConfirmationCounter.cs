using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationCounter : MonoBehaviour {

    int counter;

    void Awake() {
        counter = 0;
        Show();
    }



    public void Increment(){
        counter++;
        Show();
    }

    private void Show(){
        GetComponent<TMPro.TextMeshProUGUI>().text = counter.ToString();
    }



}
