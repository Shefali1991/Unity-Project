using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitReceiverScenepanel : MonoBehaviour {

    // Use this for initialization
    
    //public object AssignedScene;

    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene()
    {
        SceneManager.LoadScene(gameObject.name , LoadSceneMode.Single);
    }
}
