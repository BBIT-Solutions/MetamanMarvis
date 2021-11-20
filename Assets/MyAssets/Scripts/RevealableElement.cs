using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealableElement : MonoBehaviour { //HINT: if necessary, one GO can contain multiple of this component, which can be tagged differently

    public enum RevealableElementTag{
        LEVEL1_Table, LEVEL1_Materializer, LEVEL1_GrabableGunElement1, LEVEL1_GunHintElement1,
        LEVEL2_GrabableGunElement2, LEVEL2_GunHintElement2,
        LEVEL3_GrabableGunElement3, LEVEL3_GunHintElement3,
        LEVEL4_GrabableGunElement4, LEVEL4_GunHintElement4,
        LEVEL5_GrabableGunElement5, LEVEL5_GunHintElement5,
        HIDE_VIRTUAL_ELEMENTS, SHOW_VIRTUAL_ELEMENTS,
        GLOVES_ON_CONTROLLER,
        FULL_GUN, //is handled different (by quest/puzzle logic), but to have the option here too
        ENEMY_TARGET //something like this can also be assigned to multiple gameObjects
    };

    [SerializeField] public RevealableElementTag revealableElementTag;
    private bool alreadyRevealed;



    void Awake() {
        alreadyRevealed = false;
        if((revealableElementTag == RevealableElementTag.HIDE_VIRTUAL_ELEMENTS) || (revealableElementTag == RevealableElementTag.SHOW_VIRTUAL_ELEMENTS) ){
            return; //don't change anything in these 2 cases
        }        
        gameObject.SetActive(false);        //ensure they are hidden by default
    }

    public void Reveal(){

        if(alreadyRevealed) return; //if you say "repeat", just repeat the told text, but don't change the elements
        alreadyRevealed = true;




        if(revealableElementTag == RevealableElementTag.HIDE_VIRTUAL_ELEMENTS){
            gameObject.SetActive(false); //it's not really "revealing" the gameobject in this case ... it's rather revealing this "Property"
        }
        else{
        //TODO: maybe animate/tween that ... but for now just switch active state:
            gameObject.SetActive(true);

        }
    }

}
