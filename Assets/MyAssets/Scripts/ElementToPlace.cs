using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementToPlace : MonoBehaviour {

    [SerializeField] ElementsCorrectPosition correctPosition;

    [SerializeField] Material materialStart;
    [SerializeField] Material materialHint;
    [SerializeField] Material materialSolved;


//TODO: make this serializable?
    float thresholdPosition = 0.05f; //meters?
    float thresholdRotation = 5.0f; //degrees?



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


    // Update is called once per frame
    void Update()
    {
         
        if(grabbable.isGrabbed || Input.GetKeyDown(KeyCode.C)){   //Keycode to DEBUG:
            Debug.Log("check " + gameObject.name);

            if(CheckEndPosition()){
                meshRenderer.material = materialHint;
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

        //vergleichen mit end pos und rot....und bei innerhalb threshold threshold (evtl. setzbar pro pos und pro rot und pro element)

//TODO:
        //if(correct....){
        if(CheckPosition() && CheckRotation()){            
            return true;
        }

        return false;
    }

    private void Solved(){

        if(!atCorrectEndPosition) return;


        isSolved = true;
        Debug.Log("solved ... " + gameObject.name);

        meshRenderer.material = materialSolved;


        GameObject.Destroy(correctPosition.gameObject);
        // GetComponent<OVRGrabbable>().enabled = false;
        Destroy(GetComponent<OVRGrabbable>()); // .enabled = false;
//TODO: rigidbody auch noch destroy?! 


//TODO: IMPORTANT: SNAP/Tween to correct postion.... before it was destroyed^^



//NEXT: hier irgendwas auslösen....entweder auf correct positon oder hier.... evtl. auch dann das richtig diffuse anzeigen

//--> mit echtem Element machen... und dann sieht man schon besser, ob es während "nutzung" evtl. noch etwas transparent oder so sein sollte


//--> die ganze zeit checken, während sie gehalten wird...falls in bereich, dann etwas heller, bzw. mehr "contrast" oder so, dass man echtes material sieht
                                        //aber noch nicht ganz...evtl. auch etwas mit sound spannend werden oder so....
                            //--> und wenn in dem Bereich losgelassen...dann vollends komplettes snap und richtiges material

//wahrscheinlich noch pivor centered, oder alle am gleichen...kommt drauf an...evtl. auch, je nachdem, ob man 2 unabhängig voneinander baut
                            //--> aber an einem Punkt sollte wahrscheinlich trotzdem immer passen

                //"snappen" also tweenen ... und particel system.... und komplette grabbability ausschalten ... bzw. das draggenede dann einfach löschen 
                                                    //--> falls das aber gelöscht wird, dann noch das isSOlved auf das correctPosition stattdessen setzen

    }


    private bool CheckPosition(){
        return ( Vector3.Distance(transform.position, correctPosition.gameObject.transform.position) < thresholdPosition ); 
    }

    private bool CheckRotation(){
//TODO: check if that really works, or else do the adaptment for -5 vs 355 etc...
        //return ( Vector3.SignedAngle(transform.position, correctPosition.gameObject.transform.position) < thresholdPosition ); 
        return (Quaternion.Angle(transform.rotation, correctPosition.gameObject.transform.rotation) < thresholdRotation );

    }


}
