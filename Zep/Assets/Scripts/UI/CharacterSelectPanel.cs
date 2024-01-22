using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterSelectPanel : MonoBehaviour
{
    [SerializeField]
    HorizontalLayoutGroup horizontalLayoutGroup;

    [SerializeField]
    GameObject selectPrefab;

    List<string> characterNameList = new List<string>();

    public UnityEvent OnCharacterSelectPanelClosedEvent;

    private void Awake()
    {
        characterNameList = AssetDatabase.GetAssetPathsFromAssetBundle("playersprite").ToList<string>();

        foreach(string name in characterNameList) 
        {
            string fileName = name.Substring(17, name.Length - 24);
            Debug.Log(fileName);
            GameObject character = Resources.Load<GameObject>(fileName);

            GameObject selectButton = GameObject.Instantiate<GameObject>(selectPrefab, horizontalLayoutGroup.transform);

            selectButton.GetComponent<Image>().sprite = character.GetComponent<SpriteRenderer>().sprite;
            selectButton.GetComponent<Button>().onClick.AddListener(() => {
                GameManager.Instance.player.ChangeCharacter(character.GetComponent<SpriteRenderer>(), character.GetComponent<Animator>());
                this.gameObject.SetActive(false);
            });
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameManager.Instance.PauseGame();
    }

    private void OnDisable()
    {
        GameManager.Instance.StartGame();
        OnCharacterSelectPanelClosedEvent?.Invoke();
    }
}
