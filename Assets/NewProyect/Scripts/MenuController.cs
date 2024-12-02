using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class MenuController : MonoBehaviour
{
    public ItemController ic;
    public TextMeshProUGUI message;
    public bool SecondLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (SecondLevel) {

                char[] charArray = ic.password.ToCharArray();
                Array.Reverse(charArray);
                string newPassword = new string(charArray);

                if (ic.name == "Paper" || ic.name == "Sheet")
                {
                    message.text = $"En la nave que al agua caerá, la clave cambiará. Los dígitos de la verdad son {newPassword}, pero al tocar el mar, el orden se invertirá. ~ Juan Pedro";
                }
                else if (ic.name == "Poster")
                {
                    message.text = "No. " + newPassword;
                }

            }
            else{
                if (ic.name == "Paper" || ic.name == "Sheet")
                {
                    message.text = $"Para trabajar necesitarás un: {ic.password}\r\n(Guiño guiño)\r\n¡No le digas a nadie!";
                }
                else if (ic.name == "Poster")
                {
                    message.text = "No. " + ic.password;
                }
            }
        }
    }
}
