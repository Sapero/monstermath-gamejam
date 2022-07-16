using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            transform.Rotate(0, 90, 0);
        }
        if (Input.GetKeyDown("a"))
        {
            transform.Rotate(0, -90, 0);
        }
        if (Input.GetKeyDown("w"))
        {
            transform.Rotate(90, 0, 0);
        }
        if (Input.GetKeyDown("s"))
        {
            transform.Rotate(-90, 0, 0);
        }
    }
}
