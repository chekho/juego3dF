using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using TMPro; 

public class CanvasController : MonoBehaviour
{
    public GameObject OxigenLevel;
    public GameObject FurnitureMenu;
    public float totalOxygenTime = 600f;
    public GameObject ImagesPanel;
    public List<Image> imagesMenu;
    public TextMeshProUGUI oxygenText; 
    public TextMeshProUGUI scoreText;  
    public TextMeshProUGUI alertMenu; 

    private RectTransform rtOxigenLevel;
    private float sizeOxigenLevel;
    private float remainingOxygenTime;
    private PlayerMovement pm;
    private int score = 0;
    private float scoreUpdateTime = 1f;
    private float scoreUpdateTimer = 0f;

    private void Start()
    {
        rtOxigenLevel = OxigenLevel.GetComponent<RectTransform>();
        pm = FindObjectOfType<PlayerMovement>();
        imagesMenu = gameObject.GetComponentsInChildren<Image>().Where(i => i.name.EndsWith("Menu")).ToList();
        sizeOxigenLevel = rtOxigenLevel.sizeDelta.x;
        remainingOxygenTime = totalOxygenTime;
        HideMenus();
    }

    public void HideMenus()
    {
        imagesMenu.ForEach(im => im.gameObject.SetActive(false));
    }

    private void Update()
    {
        remainingOxygenTime -= Time.deltaTime;

        float newWidth = Mathf.Max(0, sizeOxigenLevel * (remainingOxygenTime / totalOxygenTime));
        rtOxigenLevel.sizeDelta = new Vector2(newWidth, rtOxigenLevel.sizeDelta.y);

        if (oxygenText != null)
        {
            oxygenText.text = "Oxígeno: " + Mathf.CeilToInt(remainingOxygenTime / 60f) + " mins";
        }

        scoreUpdateTimer += Time.deltaTime;
        if (scoreUpdateTimer >= scoreUpdateTime)
        {
            IncreaseScore(5);
            scoreUpdateTimer = 0f;
        }

        if (scoreText != null)
        {
            scoreText.text = "Puntaje: " + score;
        }

        if (remainingOxygenTime <= 0)
        {
            // guardar escena antes de morir para reintentar
            PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GameOver");
        }

        var ListItems = pm.itemsToCollect;
        List<string> ItemsCurrent = pm.itemsCollected;
        foreach (var item in ListItems)
        {
            //obtener imagen
            var imageObject = ImagesPanel.transform.Find(item);
            if (imageObject != null)
            {
                GameObject f = imageObject.gameObject;
                Image img = f.GetComponent<Image>();
                Color newColor = img.color;
                if (!ItemsCurrent.Contains(item)) newColor.a = 0.5f;
                else newColor.a = 1f;
                img.color = newColor;
            }
            else { Debug.LogWarning("No se encontró ningún GameObject con el nombre especificado."); }
        }
    }

    public void ShowFurnitureMenu()
    {
        FurnitureMenu.SetActive(!FurnitureMenu.activeInHierarchy);
    }

    public void DecreaseOxygenTime(float seconds)
    {
        remainingOxygenTime = Mathf.Max(0, remainingOxygenTime - seconds);
    }


    public void IncreaseOxygenTime(float seconds)
    {
        remainingOxygenTime = Mathf.Max(0, remainingOxygenTime + (seconds));
    }

    public void IncreaseScore(int points)
    {
        score += points;
    }

    public void ShowAlertItemCollectedWithChangeInText(string Message)
    {
        alertMenu.text = Message;
        StartCoroutine(ShowAlert());
        return;
    }

    private IEnumerator ShowAlert()
    {
        alertMenu.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f); 
        alertMenu.gameObject.SetActive(false);
    }
}
