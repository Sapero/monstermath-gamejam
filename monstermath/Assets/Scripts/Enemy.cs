using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject playerDice;
    private Vector3 startPos;
    private Vector3 endPos;
    //private float distance = 5f;

    public float lerpTime = 10;
    private float currentLerpTime = 0;
    private bool keyHit = false;

    private float points = 0;
    private float timer = 0f;
    private float highScore = 0;
    public int level = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPos = enemy.transform.position;
        endPos = playerDice.transform.position;
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
            enemy.transform.position = Vector3.Lerp(startPos, endPos, Perc);

            if(enemy.transform.position != endPos){
                //timer += Time.deltaTime;
            }
            else{
                //timer = 0;
                //Spawn Enemy
                enemy.transform.position = startPos;
                currentLerpTime = 0;
                /*Debug.Log("enemy");
                Debug.Log(enemy.transform.position);
                Debug.Log(enemy.transform.eulerAngles.x);
                Debug.Log(enemy.transform.eulerAngles.y);
                Debug.Log(enemy.transform.eulerAngles.z);*/

                if(enemy.transform.eulerAngles == playerDice.transform.eulerAngles) {
                    points += 1;
                    
                    if(points > highScore) {
                        highScore = points;
                        Debug.Log(points + " points (high score!)");
                    } else {
                        Debug.Log(points + " point(s)");
                    }

                    //level += 1;
                    //lerpTime -= 1;
                } else {
                    if(points < highScore) {
                        Debug.Log("Try again? [Press SPACE]");
                     } else if(points > 0) {
                         Debug.Log("Congrats, " + highScore + " is a new high score! Try again? [Press SPACE]");
                     };

                    //Debug.Log(playerDice.transform.eulerAngles);
                    
                    points = 0;
                    keyHit = false;

                    //level = 0;
                    //lerpTime = 10;
                }

                int howMuchX = Random.Range(0, 3);
                int howMuchY = Random.Range(0, 3);
                int howMuchZ = Random.Range(0, 3);
                enemy.transform.Rotate(90*howMuchX, 90*howMuchY, 90*howMuchZ);
            }
        }
    }
}
