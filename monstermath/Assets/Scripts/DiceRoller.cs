using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    private bool rotating;
    private float speed = 0.2f;

    public Transform player; // Drag your player here
    private Vector2 fp; // first finger position
    private Vector2 lp; // last finger position
    private float angle;
    private float swipeDistanceX;
    private float swipeDistanceY;

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
        //https://sushanta1991.blogspot.com/2014/05/simple-swipe-in-unity-using-angle.html
        foreach(Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved )
            {
                lp = touch.position;
                swipeDistanceX = Mathf.Abs((lp.x-fp.x));
                swipeDistanceY = Mathf.Abs((lp.y-fp.y));
            }
            if(touch.phase == TouchPhase.Ended)
            {
                angle = Mathf.Atan2((lp.x-fp.x),(lp.y-fp.y))*57.2957795f;
                
                if(angle > 60 && angle < 120 && swipeDistanceX > 40    )
                {
                    print ("right");
                    Vector3 vector = new Vector3(0, -90, 0);
                    StartRotation(vector);
                }
                if(angle > 150 || angle < -150 && swipeDistanceY > 40)
                {
                    print ("down");
                    Vector3 vector = new Vector3(0, 0, 90);
                    StartRotation(vector);
                }
                if(angle < -60 && angle > -120 && swipeDistanceX > 40)
                {
                    print ("left");
                    Vector3 vector = new Vector3(0, 90, 0);
                    StartRotation(vector);
                }
                if(angle > -30 && angle < 30 && swipeDistanceY > 40)
                {
                    print ("up");
                    Vector3 vector = new Vector3(0, 0, -90);
                    StartRotation(vector);
                }
                if(angle > 120 && angle < 150 && swipeDistanceY > 40)
                {
                    print ("down-left->right");
                    Vector3 vector = new Vector3(-90, 0, 0);
                    StartRotation(vector);
                }
                if(angle > -60 && angle < -30 && swipeDistanceY > 40)
                {
                    print ("up->down-left");
                    Vector3 vector = new Vector3(90, 0, 0);
                    StartRotation(vector);
                }
            }
        }

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
        if (Input.GetKeyDown("q") || Input.GetKeyDown("z"))
        {
            Vector3 vector = new Vector3(0, 90, 0);
            StartRotation(vector);
        }
        if (Input.GetKeyDown("e") || Input.GetKeyDown("x"))
        {
            Vector3 vector = new Vector3(0, -90, 0);
            StartRotation(vector);
        }
    }
}
