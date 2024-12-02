using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public ItemController ic;
    public TextMeshProUGUI message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (ic.name == "Paper" || ic.name == "Sheet")
            {
                message.text = $"Para trabajar necesitarás un: {ic.password}\r\n(Guiño guiño)\r\n¡No le digas a nadie!";
            } else if (ic.name == "Poster")
            {
                message.text = "No. " + ic.password;
            }
        }
    }
}
