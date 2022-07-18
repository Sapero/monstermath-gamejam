using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private bool rotating;
    private float speed = 0.2f;
    public GameObject rightObject;
    public GameObject frontObject;
    public int rotateRight;

    // Start is called before the first frame update
    void Start()
    {

    }

     private IEnumerator Rotate( Vector3 angles, float duration )
    {
        rotating = true ;
        Quaternion startRotation = transform.rotation ;
        Quaternion endRotation = Quaternion.Euler( angles ) * startRotation ;
        for( float t = 0 ; t < duration ; t+= Time.deltaTime )
        {
            transform.rotation = Quaternion.Lerp( startRotation, endRotation, t / duration ) ;
            yield return null;
        }
        transform.rotation = endRotation;
        rotating = false;
    }

    public void StartRotation(Vector3 vector)
    {
        if( !rotating )
            StartCoroutine(Rotate(vector, speed));
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {*/
                    //Debug.Log(rotateRight);
                    if(rotateRight > 1) {
                        Vector3 vector = new Vector3(0, 0, 90);
                        StartRotation(vector);
                    } /*else {
                        Vector3 vector = new Vector3(0, 0, -90);
                        StartRotation(vector);
                    }*/
                /*}
            }
        }*/

        if (Input.GetKeyDown("s")  || Input.GetKeyDown("down"))
        {
            Vector3 vector = new Vector3(0, 0, 90);
            StartRotation(vector);
        }
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            Vector3 vector = new Vector3(0, 0, -90);
            StartRotation(vector);
        }
        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            Vector3 vector = new Vector3(-90, 0, 0);
            StartRotation(vector);
        }
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            Vector3 vector = new Vector3(90, 0, 0);
            StartRotation(vector);
        }
    }
}
