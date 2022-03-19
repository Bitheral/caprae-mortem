using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine.U2D;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class PlayerScript : MonoBehaviour
{

    public float speed;
	public float startWaitTime;
	public float waitTime;
	private int amount;
	public GameObject goat;
	public Canvas pause;
	public TextMeshPro textGoats;
	private AudioSource beamAudio;

	private Vector3 pos;
	
	// Use this for initialization
	void Start()
	{
		beamAudio = Camera.main.GetComponent<AudioSource>();
		amount = 0;
		startWaitTime = 10f;
		waitTime = startWaitTime;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		pause.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		textGoats.text = "Goats sacrificed: " + amount;
		goat = GameObject.Find("Goat");
		controls();
		limitPos();

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Time.timeScale = 0;
			pause.enabled = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			beamAudio.Pause();
		}
	}
	
	public void continueGame()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		pause.enabled = false;
		Time.timeScale = 1;
		beamAudio.UnPause();
		
	}
	
	public void exit()
	{
		SceneManager.LoadScene("Menu");
	}
	
	void limitPos()
	{
		pos = this.transform.position;

		if (pos.x < -7.8)
		{
			pos.x = -7.8f;
		} else if (pos.x > 7.7)
		{
			pos.x = 7.7f;
		}

		this.transform.position = pos;
	}


	void controls()
	{
		float lftrgt = Input.GetAxis("Horizontal") * speed;
		lftrgt *= Time.deltaTime;
		
		//Debug.LogFormat("Horizontal: {0}, Vertical: {1}", Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			
		transform.Translate(lftrgt, 0, 0);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		beamAudio.Play();
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		Debug.Log("Goat health: " + goat.GetComponent<GoatAI>().health);
		goat.GetComponent<GoatAI>().health = waitTime / 10;
		if (other.gameObject.name == goat.name)
		{
			if (waitTime <= 0)
			{
				beamAudio.Stop();
				waitTime = startWaitTime;
				Destroy(other.gameObject);
				amount += 1;
				newGoat();
				Debug.Log("Wait Time: " + waitTime);
			}
			else
			{
				waitTime -= Time.deltaTime;
			}
		}
	}


	private void OnTriggerExit2D(Collider2D other)
	{
		beamAudio.Stop();
	}

	void newGoat()
	{
		GameObject goatNew =  Instantiate(goat);
		goatNew.name = "Goat";
		goatNew.transform.position = new Vector3(0.27f, 2f, 2.01f);
		goatNew.GetComponent<GoatAI>().health = waitTime;
		goatNew.GetComponent<BoxCollider2D>().enabled = true;
		goatNew.GetComponent<GoatAI>().enabled = true;
		goatNew.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
		goatNew.transform.GetChild(0).GetComponent<CanvasScaler>().enabled = true;
		goatNew.transform.GetChild(0).GetComponent<GraphicRaycaster>().enabled = true;
	}
}
