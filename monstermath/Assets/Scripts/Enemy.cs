using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject playerDice;
    private Vector3 startPos;
    private Vector3 dicePos;

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

        if(level <= 5){
            transform.Rotate(0, 0, 90*howMuchZ);
            lerpTime *= 0.9f;
            if(level == 5) {
                lerpTime = 10.0f;
            }
        } else if (level <= 10){
            transform.Rotate(90*howMuchX, 0, 90*howMuchZ);
            lerpTime *= 0.9f;
            if(level == 10) {
                lerpTime = 10.0f;
            }
        } else if (level > 11) {
            transform.Rotate(90*howMuchX, 90*howMuchY, 90*howMuchZ);
            lerpTime *= 0.9f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetKeyDown (KeyCode.Space)) {
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
            } else {
                Debug.Log(transform.position.x);
                Debug.Log(dicePos.x);
            }    
        }
    }
}
