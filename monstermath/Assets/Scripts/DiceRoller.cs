using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            transform.Rotate(0, 90, 0);
            //if(checkIfOkMove()) {
                //transform.Rotate(0, -90, 0);
                //Debug.Log(transform.eulerAngles);
            //} else {
                //Debug.Log("Move is not allowed!");
            //}
        }
        if (Input.GetKeyDown("d"))
        {
            transform.Rotate(0, -90, 0);
            //Debug.Log(transform.eulerAngles);
        }
        if (Input.GetKeyDown("w"))
        {
            transform.Rotate(90, 0, 0);
            //Debug.Log(transform.eulerAngles);
        }
        if (Input.GetKeyDown("s"))
        {
            transform.Rotate(-90, 0, 0);
            //Debug.Log(transform.eulerAngles);
        }
        
    }
}
