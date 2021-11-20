using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunShot : MonoBehaviour {

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] AudioClip shotSound;

    OVRGrabbable grabbable;

    bool waitForNextShot;

    [SerializeField] GameStateMachine.StateCanBeSolvedBy onGrabbedAndShotTag;
    int shotCount;

    void Awake() {
        waitForNextShot = false;
        grabbable = GetComponent<OVRGrabbable>();
        shotCount = 0;
    }


    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){ //to DEBUG:
            Shoot();
        }


        if(grabbable.isGrabbed){
            OVRInput.Controller grabbedBy = grabbable.grabbedBy.Controller;
            if(grabbedBy == OVRInput.Controller.RTouch){  //HINT: could be wrong if player is left-handed and has left controller as primary?! (but should work by using RawButton instead Button)
                if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
                    Shoot();
                }
            }
            else if(grabbedBy == OVRInput.Controller.LTouch){
                if(OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)){
                    Shoot();
                }
            }
        }
        
    }

    public void Shoot(){

        if(waitForNextShot) return;
        waitForNextShot = true;

        GameObject tmpBullet = GameObject.Instantiate(bulletPrefab);
        Transform trans = tmpBullet.transform;
        trans.position = bulletSpawnPoint.position;
        trans.rotation = bulletSpawnPoint.rotation; 

        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = shotSound;
        audioSource.Play();

        shotCount++;

        if(shotCount >= 3){ //don't reset this. If user shot before the state/task started, in this way, it will then still be invoked.... else wait for the 3 shots in general (the statemachine anyhow changes the state then, so that it does not react the next time then)
            GameStateMachine.onStateSolved?.Invoke(onGrabbedAndShotTag);
        }

        StartCoroutine(WaitForNextShot());
    }

    private IEnumerator WaitForNextShot(){
        yield return new WaitForSeconds(0.3f);
        waitForNextShot = false;
    } 


}
