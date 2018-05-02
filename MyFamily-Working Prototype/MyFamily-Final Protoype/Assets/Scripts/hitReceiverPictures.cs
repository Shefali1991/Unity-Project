using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class hitReceiverPictures : MonoBehaviour {

    private bool flag = false;

    private Transform homeParent;
    private Transform DestinationParent;
    private Transform curParent;

    private Vector3 homePosition;
    private Vector3 homeAngles;
    public Vector3 homeLocalScale ;


    private Vector3 destinationPosition;
    private Vector3 destinationhomeAngles;

    private float theta_x;
    private float theta_y;

    private AudioSource audSrc;

    // Use this for initialization
    void Start () {
        
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LerpToOrigin()
    {
        homeParent = gameObject.transform.parent;
        DestinationParent = homeParent.parent;
        homePosition = gameObject.transform.localPosition;
        homeAngles = gameObject.transform.localEulerAngles;
        homeLocalScale = gameObject.transform.localScale;
        Debug.Log(homeParent.name + " == " + gameObject.transform.localScale + " --- " + homeLocalScale);

        audSrc = gameObject.GetComponent<AudioSource>();


        gameObject.transform.parent = DestinationParent;

        curParent = gameObject.transform.parent;

        //float radius_small = 1f;

        float radius_big = Mathf.Sqrt(Mathf.Pow(gameObject.transform.localPosition.x, 2) +
                                       Mathf.Pow(gameObject.transform.localPosition.y, 2) +
                                       Mathf.Pow(gameObject.transform.localPosition.z, 2)
                                     );


        float x_new = gameObject.transform.localPosition.x / radius_big;
        float y_new = gameObject.transform.localPosition.y / radius_big;
        float z_new = gameObject.transform.localPosition.z / radius_big;

        Vector3 container_size = new Vector3(1,1,1) ;
        double container_width = container_size.x ;
        double container_height = container_size.y ;

        double ScalingFactor;

        double Tex_width = gameObject.transform.localScale.x;
        double Tex_height = gameObject.transform.localScale.y;

        double Background_ratio = GetRatio(container_width, container_height);
        double Texture_ratio = GetRatio(Tex_width, Tex_height);

        if (1 < Texture_ratio)
        {
            // Fit texture to Background by width
            ScalingFactor = container_width / (double)Tex_width;
        }
        else
        {
            // Fit texture to Background by height
            ScalingFactor = container_height / (double)Tex_height;
        }

        gameObject.transform.localScale = new Vector3((float)Tex_width * (float)ScalingFactor, (float)Tex_height * (float)ScalingFactor, 1);
        gameObject.transform.localPosition = new Vector3(x_new, 0, z_new);
        gameObject.transform.rotation = Quaternion.LookRotation(transform.position - new Vector3(0, 0, 0));
        audSrc.Play();

    }

    double GetRatio(double width, double height)
    {
        double ratio = (double)width / (double)height;
        return ratio;
    }

    public void RevertPosition()
    {
        
        gameObject.transform.parent = homeParent;
        curParent = gameObject.transform.parent;
        Debug.Log(homeLocalScale);
        gameObject.transform.localPosition = homePosition;
        gameObject.transform.localEulerAngles = homeAngles;
        gameObject.transform.localScale = homeLocalScale;
        audSrc.Stop();
        
    }
}
