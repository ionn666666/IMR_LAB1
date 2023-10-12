using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class fightScript : MonoBehaviour
{
    public int health = 15;
    private int damage = 3;
    private Animator mAnimator;
    private GameObject[] allObjects;
    private GameObject nearestObject;
    float distance;
    float nearestDistance = 1000000;
    private GameObject parent;
    private int lasthealth;
    fightScript nearestEnemy;
    private int lastTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        lasthealth = health;
        mAnimator = GetComponent<Animator>();
        parent = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        allObjects = GameObject.FindGameObjectsWithTag("imageTarget");
        nearestDistance = 100000000;
        foreach (GameObject obj in allObjects)
        {
            if (!(parent.Equals(obj)))
            {
                distance = Vector3.Distance(parent.transform.position, obj.transform.position);
                //Debug.Log(parent.transform.position + " " + parent.name);
                //Debug.Log(obj.transform.position + " " + obj.name);
                

                if (distance < nearestDistance && distance != 0)
                {
                    nearestDistance = distance;
                    nearestObject = obj;
                }
            }

        }
        //Debug.Log(nearestDistance * 1000000 + " " + distance );
        
        //aici fac animatiile, nu este setata o sisteme de ATACK SPEED 
        if (mAnimator != null && nearestDistance * 1000000 < 150000)
        {
            if (nearestObject != null && ((int)Time.realtimeSinceStartup) % 5 == 0 && ((int)Time.realtimeSinceStartup != lastTime))
            {
                lastTime = (int)Time.realtimeSinceStartup;
                if (health <= 0) { mAnimator.SetTrigger("die"); Destroy(this); }
                nearestEnemy = nearestObject.GetComponentInChildren<fightScript>();
                if (lasthealth > health) { mAnimator.SetTrigger("gotHit");lasthealth = health; } else
                {
                    if (nearestEnemy.health > 0) { mAnimator.SetTrigger("fight"); }
                    if (nearestEnemy != null && health > 0)
                    {
                        nearestEnemy.takeDMG(damage);
                        Debug.Log("HEALTH OF ENEMY " + nearestEnemy.health);
                    }
                }
                
            }
            

        }
        Debug.Log(Time.realtimeSinceStartup);


    }

    public void takeDMG(int amount)
    {
        health = health - amount;
    }

}
