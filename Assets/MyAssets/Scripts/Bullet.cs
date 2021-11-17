using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour {

    [SerializeField] AudioClip explosionSound;

    void Start() {
        GiveVelocity();
        StartCoroutine(FadeOut());
    }

    private void GiveVelocity(){
        GetComponent<Rigidbody>().velocity = transform.up * 20f;
    }

    private IEnumerator FadeOut(){
        yield return new WaitForSeconds(3f);
        
        //maybe TODO: fade out slowly, but not really necessary. It's far away and nearly invisible already when destroying 
        StartCoroutine(DestroyDelayed());
    }
    

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Enemy")){
            Destroy(other.gameObject);
            Explode();
        }
    }

    public void Explode(){
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = explosionSound;
        audioSource.Play();

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        StartCoroutine(DestroyDelayed());
    }

    private IEnumerator DestroyDelayed(){
        yield return new WaitForSeconds(2f); //wait for the explosion sound etc.
        Destroy(gameObject);
    }

}
