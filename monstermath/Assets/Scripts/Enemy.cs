using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject wall;
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance = 30f;

    private float lerpTime = 5;
    private float currentLerpTime = 0;
    private bool keyHit = false;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = wall.transform.position;
        endPos = wall.transform.position + Vector3.right * distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space)) {
            keyHit = true;
        }

        if (keyHit == true) {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime >= lerpTime) {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime/lerpTime;
            wall.transform.position = Vector3.Lerp(startPos, endPos, Perc);

            if(timer <= 5f){
                timer += Time.deltaTime;
            }
            else{
                timer = 0;
                //Spawn Enemy
                wall.transform.position = startPos;
                wall.transform.Rotate(0, 90, 0);
                currentLerpTime = 0;
            }
        }
    }
}
