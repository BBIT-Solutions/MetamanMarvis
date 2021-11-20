using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] GameStateMachine.StateCanBeSolvedBy onShotAllEnemies;

    // Start is called before the first frame update
    void Start() {
        //move up and down slightly
        LeanTween.moveY(gameObject, gameObject.transform.position.y + 1f, Random.Range(1.5f, 2.5f)).setLoopPingPong();


    }

   

    void OnDestroy() {
        if(transform.parent.childCount <= 1){ //should be still 1 ON/during destroy...but to be sure, check for 1 and 0 (to ensure continuing... in worst case the next state could be already triggered one enemy too early)
            GameStateMachine.onStateSolved?.Invoke(onShotAllEnemies);
        }
    }
}
