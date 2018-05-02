using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hitExitPanel : MonoBehaviour {

    // Use this for initialization
    //public Object AssignedScene;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene()
    {
        SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
    }
}
