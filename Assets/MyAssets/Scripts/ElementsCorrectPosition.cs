using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsCorrectPosition : MonoBehaviour {

    MeshRenderer meshRenderer;

    void Awake() {
	    meshRenderer = GetComponent<MeshRenderer>();	
        Show();
    }

    public void Show(){
        meshRenderer.enabled = true;
    }
    
    public void Hide(){
        meshRenderer.enabled = false;
    }

}
