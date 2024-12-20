using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.VersionControl;

public class NumpadController : MonoBehaviour
{
    public GameObject numpadCanvas; // Referencia al Canvas del Numpad
    public TextMeshProUGUI displayText; // Referencia al texto que mostrará los asteriscos
    public string correctPassword = "1234"; // Contraseña correcta
    private string inputPassword = ""; // Contraseña ingresada
    public DoorController doorController; // Referencia al DoorController
    public PlayerMovement playerMovement; // Referencia al PlayerMovement para checar items recolectados
    public bool skipCanvasIfItemCollected = false; // Nueva variable para controlar el comportamiento
    public ItemController ic;

    void Start()
    {
        numpadCanvas.SetActive(false); // Desactiva el canvas al inicio
        if (ic.name == "Paper" || ic.name == "Poster" || ic.name == "Sheet")
        {
        }
    }

    void Update()
    {
        if (numpadCanvas.activeInHierarchy)
        {
            if (ic.name == "Paper" || ic.name == "Poster" || ic.name == "Sheet")
            {
                correctPassword = ic.password;
                foreach (char key in Input.inputString)
                {
                    if (char.IsDigit(key))
                    {
                        AddDigit(key.ToString());
                    }
                    else if (key == '\b') // Backspace
                    {
                        RemoveLastDigit();
                    }
                    else if (key == '\r' || key == '\n') // Enter
                    {
                        EnterPassword();
                    }
                }
            } else if(ic.name == "Key" || ic.name == "Id")
            {
                correctPassword = ic.name;
                if (Input.GetKey(KeyCode.KeypadEnter))
                {
                    if(playerMovement.itemsCollected.Contains(ic.name))
                    {
                        doorController.RemoveDoor();
                    }
                }
            }

        }
    }

    public void AddDigit(string digit)
    {
        if (inputPassword.Length < 8)
        {
            inputPassword += digit;
            UpdateDisplay();
        }
    }

    public void RemoveLastDigit()
    {
        if (inputPassword.Length > 0)
        {
            inputPassword = inputPassword.Substring(0, inputPassword.Length - 1);
            UpdateDisplay();
        }
    }

    public void ClearInput()
    {
        inputPassword = "";
        UpdateDisplay();
    }

    public void EnterPassword()
    {
        if (inputPassword == correctPassword)
        {
            Debug.Log("Contraseña correcta!");
            doorController.RemoveDoor(); // Llamar al método RemoveDoor
        }
        else
        {
            Debug.Log("Contraseña incorrecta");
        }
        ClearInput();
    }

    private void UpdateDisplay()
    {
        displayText.text = new string('*', inputPassword.Length);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorController.isOpen)
            {
                numpadCanvas.SetActive(false);
            } else if (skipCanvasIfItemCollected && playerMovement.itemsCollected.Contains("ItemID")) // Cambia "ItemID" por el ID del item específico
            {
                Debug.Log("Item ya recolectado, abriendo puerta directamente.");
                doorController.RemoveDoor();
            }
            else
            {
                numpadCanvas.SetActive(true);
                ClearInput();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            numpadCanvas.SetActive(false);
            ClearInput();
        }
    }
}
