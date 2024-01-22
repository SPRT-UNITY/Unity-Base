using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameInputPanel : MonoBehaviour
{
    [SerializeField]
    TMP_InputField field;

    const int minInputLimit = 2;
    const int maxInputLimit = 12;

    [SerializeField]
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void InputLimit() 
    {
        if(field.text.Length >= minInputLimit && field.text.Length <= maxInputLimit) 
        {
            button.interactable = true;
        }
        else 
        {
            button.interactable = false;
        }
    }

    public void InputName() 
    {
        GameManager.Instance.player.ChangePlayerName(field.text);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.PauseGame();
    }

    private void OnDisable()
    {
        GameManager.Instance.StartGame();   
    }
}
