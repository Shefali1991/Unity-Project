using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCaster : MonoBehaviour {

    private GameObject prevHitObj = null;
    private GameObject currentObj = null;
    private GameObject selectedObj = null;

    private bool ObjSelected = false;

    private float start_time;
    private float running_time;
    private float select_time;

    public Transform LoadingBar;
    public float duration;

    

    // Use this for initialization
    void Start () {
        start_time = Time.time;
        running_time = start_time;
    }
	
	// Update is called once per frame
	void Update () {
        CastRay();
        CheckTimer_v2();
    }

    void CastRay()
    {
        RaycastHit hit;
        Vector3 forwardVect = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forwardVect, out hit))
        {
            if (hit.collider.gameObject.tag == "picture" || 
                hit.collider.gameObject.tag == "video" || 
                hit.collider.gameObject.tag == "scene" ||
                hit.collider.gameObject.tag == "exit"
            )
            {
                handlePictureCollision(hit.collider.gameObject);
            }
            else
            {
                handlePictureCollision(null);
            }
        }
        else
        {
            handlePictureCollision(null);
        }
    }

    void handlePictureCollision(GameObject curHitObj)
    {
        currentObj = curHitObj;

        if (ObjSelected == false)
        {
            // Possibility 1: prevHitObj is null and ray has hit something
            if (prevHitObj == null && curHitObj != null)
            {
                prevHitObj = curHitObj;
                // start timer here ---------------------
                start_time = Time.time + Time.deltaTime;
                running_time = start_time;
            }

            // Possibility 2: prevHitObj is not null and ray has hit something now too
            else if (prevHitObj != null && curHitObj != null)
            {
                if (prevHitObj == curHitObj)
                {
                    // continue running the timer
                    running_time = Time.time;
                }
                else //  prevHitObj != curHitObj
                {
                    prevHitObj = curHitObj;
                    // Reset the timer to 0 and start the timer again
                    start_time = Time.time + Time.deltaTime;
                    running_time = start_time;
                }
            }

            // Possibility 3: prevHitObj is not null and ray has hit nothing now
            else if (prevHitObj != null && curHitObj == null)
            {
                // Reset the timer;
                start_time = Time.time;
                running_time = start_time;
            }
        }
        else // ObjSelected == true
        {
            if (prevHitObj == curHitObj)
            {
                start_time = Time.time + Time.deltaTime;
                running_time = start_time;
            }
            else // prevHitObj != curHitObj
            {
                running_time = Time.time;
            }
        }
    }

    void CheckTimer_v2()
    {
        float difInTime = running_time - start_time;
        updateProgressBar_v2(difInTime);
        ////Debug.Log(" difInTime and duration :: " + difInTime + " and " + duration);
        if (difInTime >= duration)
        {
            //Debug.Log(" difInTime > = duration " + difInTime + " > = " + duration);
            if (ObjSelected == false)
            {
                ObjSelected = true;
                selectedObj = currentObj;
                start_time = Time.time;
                running_time = start_time;
                //UpdateMaterial_v2();
                if(selectedObj.tag == "picture")
                {
                    var hitReciver = selectedObj.GetComponent<hitReceiverPictures>();
                    hitReciver.LerpToOrigin();
                }
                else if (selectedObj.tag == "scene")
                {
                    var hitReciver = selectedObj.GetComponent<hitReceiverScenepanel>();
                    hitReciver.LoadScene();
                }
                else if (selectedObj.tag == "exit")
                {
                    var hitReciver = selectedObj.GetComponent<hitExitPanel>();
                    hitReciver.LoadScene();
                }
                else if( selectedObj.tag == "video")
                {
                    var hitReciver = selectedObj.GetComponent<hitReceiverVideo>();
                    hitReciver.PlayVideo();
                }

                return;
            }
            else // ObjSelected == true
            {
                ObjSelected = false;
                start_time = Time.time;
                running_time = start_time;
                //RevertMaterial_v2();
                if (selectedObj.tag == "picture")
                {
                    var hitReciver = selectedObj.GetComponent<hitReceiverPictures>();
                    hitReciver.RevertPosition();
                }
                else if (selectedObj.tag == "video")
                {
                    var hitReciver = selectedObj.GetComponent<hitReceiverVideo>();
                    hitReciver.StopVideo();
                }
                return;
            }
        }
    }

    void updateProgressBar_v2(float difInTime)
    {
        LoadingBar.GetComponent<Image>().fillAmount = (difInTime / duration);
    }
}
