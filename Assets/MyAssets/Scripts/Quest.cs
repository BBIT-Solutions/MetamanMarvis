using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {

    // [SerializeField] GameObject[] questParts; 
//TODO: maybe name it "Subquest" or so
    [SerializeField] ElementToPlace[] questParts; 

    [SerializeField] GameObject reward;

    bool questSolved;

    void Awake() {
        reward.SetActive(false);
        questSolved = false;
    }

    void Update() {

        if(questSolved) return;
        
//TODO: has this to be done in Update necessarily?!

        bool tmpSolved = true;
        for(int i=0; i<questParts.Length; i++){
            if(!questParts[i].IsSolved){
                tmpSolved = false;
                break;
            }
        }

        if(tmpSolved){
            questSolved = true;
            Debug.Log("full quest solved: " + gameObject.name);
            ShowReward();
            DestroyAllQuestParts(); 
        }

    }

    void DestroyAllQuestParts(){
        for(int i=0; i<questParts.Length; i++){
            Destroy(questParts[i].gameObject);
        }
    }

    void ShowReward(){
        reward.SetActive(true);
    }
}
