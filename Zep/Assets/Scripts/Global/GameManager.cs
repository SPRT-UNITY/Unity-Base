using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerController player { get; private set; }

    [SerializeField] private CharacterSelectPanel characterSelectPanel;
    [SerializeField] private NameInputPanel nameInputPanel;

    private void Awake()
    {
        Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        PauseGame();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterSelectPanel.OnCharacterSelectPanelClosedEvent.AddListener(OpenNameInputPanel);
        characterSelectPanel.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenNameInputPanel() 
    {
        characterSelectPanel.OnCharacterSelectPanelClosedEvent.RemoveListener(OpenNameInputPanel);
        nameInputPanel.gameObject.SetActive(true);
    }

    public void PauseGame() 
    {
        Time.timeScale = 0;
    }

    public void StartGame() 
    {
        Time.timeScale = 1;
    }
}
