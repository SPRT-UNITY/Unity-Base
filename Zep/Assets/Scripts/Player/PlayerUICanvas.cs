using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUICanvas : MonoBehaviour
{
    [SerializeField]
    TMP_Text _playerName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName() 
    {
        _playerName.text = GameManager.Instance.player.playerName;
    }
}
