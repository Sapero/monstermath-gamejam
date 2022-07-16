using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public GameObject objectToRotate;
    public Transform enemy;
    private Quaternion lookRotation;
    
    public float RotationSpeed;
    private bool rotating;
    private float speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private bool checkIfOkMove() {
        if (transform.eulerAngles.z == 270)
        {
            return false;
        } else {
            return true;
        }
    }

     private IEnumerator Rotate( Vector3 angles, float duration )
    {
        rotating = true ;
        Quaternion startRotation = objectToRotate.transform.rotation ;
        Quaternion endRotation = Quaternion.Euler( angles ) * startRotation ;
        for( float t = 0 ; t < duration ; t+= Time.deltaTime )
        {
            objectToRotate.transform.rotation = Quaternion.Lerp( startRotation, endRotation, t / duration ) ;
            yield return null;
        }
        objectToRotate.transform.rotation = endRotation  ;
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector3 enemyDirection = (enemy.position - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(enemyDirection);
            //transform.rotation = lookRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        }

        //TODO: implement so that up/down changes behaviour depending on direction towards the enemy

        if (Input.GetKeyDown("a"))
        {
            Vector3 vector = new Vector3(0, 90, 0);
            StartRotation(vector);
        }
        if (Input.GetKeyDown("d"))
        {
            Vector3 vector = new Vector3(0, -90, 0);
            StartRotation(vector);
        }
        if (Input.GetKeyDown("w"))
        {
            Vector3 vector = new Vector3(0, 0, 90);
            StartRotation(vector);
        }
        if (Input.GetKeyDown("s"))
        {
            Vector3 vector = new Vector3(0, 0, -90);
            StartRotation(vector);
        }
    }
}
