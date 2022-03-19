using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GoatAI : MonoBehaviour
{
    public float speed;
    public Image healthBar;
    public float health;
    public GameObject beam;

    public Transform[] moveSpots;
    private int randomSpot;

    public float waitTime;
    public float StartWaitTime;
    
  
    // Start is called before the first frame update
    void Start()
    {
        beam = GameObject.Find("Beam");
        waitTime = StartWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        moveSpots = new[]
        {
            GameObject.Find("Move_1").transform, GameObject.Find("Move_2").transform,
            GameObject.Find("Move_3").transform, GameObject.Find("Move_4").transform
        };
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health;
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                StartWaitTime = Random.Range(3, 10);
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = StartWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
                Debug.Log(waitTime);
            }
        }
    }

    private void OnEnable()
    {
        health = 1;
    }
}
