using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private TMP_InputField amountText;
    private int amount;

    private void Start()
    {
        amount = 1;
    }

    public void menu_play()
    {
        SceneManager.LoadScene("Game");
    }

    public void menu_quit()
    {
        Application.Quit();
    }

    public void menu_main()
    {
        SceneManager.LoadScene("Menu");
    }

    public void menu_donate()
    {
        SceneManager.LoadScene("Donate");
    }

    //DONATE

    public void donate_paypal()
    {
        Application.OpenURL("https://paypal.me/bitheral/" + amount);
    }
    
   public void donate_patreon()
   {
       Application.OpenURL("https://patreon.com/bitheral");
   }

    public void playSnound()
    {
        GameObject.Find("Menu_Sound").GetComponent<AudioSource>().Play();
    } 

   public void changeAmount()
   {
      amountText = GameObject.Find("TextMeshPro - InputField").GetComponent<TMP_InputField>();
      Int32.TryParse(amountText.text, out amount);
      Debug.Log(amount);
    }
}
