using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

    // [SerializeField] GameObject[] questParts; 
//TODO: maybe name it "Subquest" or so
    [SerializeField] ElementToPlace[] questParts; 


    // Update is called once per frame
    void Update()
    {
        
//TODO: has this to be done in Update necessarily?!

        bool questSolved = true;
        for(int i=0; i<questParts.Length; i++){
            if(!questParts[i].IsSolved){
                questSolved = false;
                break;
            }
        }

        if(questSolved){
            Debug.Log("full quest solved: " + gameObject.name);
        }

    }
}
