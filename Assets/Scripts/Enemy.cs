using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject playerDice;
    private Vector3 startPos;
    private Vector3 dicePos;

    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;

    public float lerpTime = 10.0f;
    private float currentLerpTime = 0;
    private bool keyHit = false;

    private float points = 0;
    private float highScore = 0;
    public int level = 0;

    bool checkIfSameRotation() {
        Vector3 playerAngles = playerDice.transform.eulerAngles;
        Vector3 enemyAngles = transform.eulerAngles;
        Quaternion a = Quaternion.Euler(playerAngles.x, playerAngles.y, playerAngles.z);
        Quaternion b = Quaternion.Euler(enemyAngles.x, enemyAngles.y, enemyAngles.z);
        float angle = Quaternion.Angle(a, b);
        bool hasSameAngle = Mathf.Abs (angle) < 1e-3f;
        return hasSameAngle;
    }

    void rotateEnemy() {
        int howMuchX = Random.Range(0, 3);
        int howMuchY = Random.Range(0, 3);
        int howMuchZ = Random.Range(0, 3);

        /*if(level == 2)
            playerDice.transform.LookAt(new Vector3(90.0f, -90.0f, 0.0f));
        if(level == 3)
            playerDice.transform.LookAt(new Vector3(0, 0, 0));
        if(level == 4)
            playerDice.transform.LookAt(new Vector3(-90.0f, -90.0f, -90.0f));*/
        
        if(level <= 5){
            transform.Rotate(0, 0, 90*howMuchZ);
            lerpTime *= 0.9f;
            if(level == 5) {
                lerpTime = 10.0f;
                GetComponent<Renderer>().material = material2;
            }
        } else if (level <= 10){
            transform.Rotate(90*howMuchX, 0, 90*howMuchZ);
            lerpTime *= 0.9f;
            if(level == 10) {
                lerpTime = 10.0f;
                GetComponent<Renderer>().material = material3;
            }
        } else if (level > 11) {
            transform.Rotate(90*howMuchX, 90*howMuchY, 90*howMuchZ);
            lerpTime *= 0.9f;
            if(level == 15) {
                GetComponent<Renderer>().material = material4;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = material1;
        startPos = transform.position;
        dicePos = playerDice.transform.position;
        bool sameRotation = checkIfSameRotation();
        if(sameRotation){
            rotateEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown (KeyCode.Space) || Input.touchCount > 0) && !keyHit) {
            keyHit = true;
            rotateEnemy();
        }

        if (keyHit == true) {
            currentLerpTime += Time.deltaTime;
            if(currentLerpTime >= lerpTime) {
                currentLerpTime = lerpTime;
            }

            float Perc = currentLerpTime/lerpTime;
            transform.position = Vector3.Lerp(startPos, dicePos, Perc);

            if (Input.GetKeyDown(KeyCode.Space) && currentLerpTime > 0.5f) {
                currentLerpTime = lerpTime-0.25f;
            }

            bool hasReached = Mathf.Round(transform.position.x) == Mathf.Round(dicePos.x);

            if(hasReached){
                currentLerpTime = 0;

                bool sameRotation = checkIfSameRotation();

                if(sameRotation) {
                    points += 1;
                    
                    if(points > highScore) {
                        highScore = points;
                        Debug.Log(points + " points (high score!)");
                    } else {
                        Debug.Log(points + " point(s)");
                    }

                    level += 1;
                    //lerpTime *= 0.75f;
                    //Debug.Log("Level: " + level);
                    //Debug.Log("LerpTime: " + lerpTime);
                } else {
                    GetComponent<Renderer>().material = material1;

                    //Debug.Log(playerDice.transform.eulerAngles);
                    //Debug.Log(transform.eulerAngles);

                    if(points <= highScore) {
                        Debug.Log("Score: " + points + ". High score: " + highScore + ". Try again? [Press SPACE]");
                     } else if(points > 0) {
                         Debug.Log("Congrats, " + highScore + " is a new high score! Try again? [Press SPACE]");
                     };

                    //Debug.Log(playerDice.transform.eulerAngles);
                    
                    points = 0;
                    keyHit = false;

                    level = 0;
                    lerpTime = 10.0f;
                    //Debug.Log("Level: " + level);
                    //Debug.Log("LerpTime: " + lerpTime);
                }

                Debug.Log("startPos: " + startPos);
                Debug.Log("positionBefore: " + transform.position);
                transform.position = startPos;
                Debug.Log("positionAfter: " + transform.position);

                rotateEnemy();
                bool sameRot = checkIfSameRotation();
                if(sameRot){
                    rotateEnemy();
                }
            }
        }
    }
}
