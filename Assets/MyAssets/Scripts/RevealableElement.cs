using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealableElement : MonoBehaviour {

    public enum RevealableElementTag{
        LEVEL1_Table, LEVEL1_Materializer, LEVEL1_GrabableGunElement1, LEVEL1_GunHintElement1,
        LEVEL2_GrabableGunElement2, LEVEL2_GunHintElement2,
        LEVEL3_GrabableGunElement3, LEVEL3_GunHintElement3,
        LEVEL4_GrabableGunElement4, LEVEL4_GunHintElement4,
        LEVEL5_GrabableGunElement5, LEVEL5_GunHintElement5,
        LEVEL2_xyzTODO,
        LEVEL5FINAL____xyzTODO_enemy //something like this can you also assign to multiple gameObjects
    };

    [SerializeField] public RevealableElementTag revealableElementTag;
    private bool alreadyRevealed;



    void Awake() {
        gameObject.SetActive(false);        //ensure they are hidden by default
        alreadyRevealed = false;
    }

    public void Reveal(){

        if(alreadyRevealed) return; //if you say "repeat", just repeat the told text, but don't change the elements
        alreadyRevealed = true;


        //TODO: maybe animate/tween that ... but for now just:
        gameObject.SetActive(true);
    }

}
