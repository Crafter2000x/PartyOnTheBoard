using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberRollScript : MonoBehaviour
{
    public int RolledNumber;
    public GamePlayerInformation Player;
    public bool FirstRoll;
    [SerializeField] private TMP_Text PlayerName;
    [SerializeField] private TMP_Text DiceRoll;
    private bool HasBeenRolled;

    private void Start()
    {
        PlayerName.text = Player.PlayerName;
        Player.PlayerInput.OnVariableChangeInputA += StopTheDice;
        if (FirstRoll == true)
        {
            StartCoroutine("RandomDiceRollStart");
        }
        else 
        {
            StartCoroutine("RandomDiceRollLoop");
        }
    }

    private void StopTheDice(bool newVal)
    {
        HasBeenRolled = true;
    }

    IEnumerator RandomDiceRollStart() 
    {
        int RandomInt;
        while (HasBeenRolled == false)
        {
            RandomInt = Random.Range(1, 11);
            if (RandomInt.ToString() != DiceRoll.text)
            {
                DiceRoll.text = RandomInt.ToString();
                yield return new WaitForSeconds(.2F);
            }
        }
        RolledNumber = int.Parse(DiceRoll.text);
        Player.FirstRoll = RolledNumber;
    }

    IEnumerator RandomDiceRollLoop()
    {
        int RandomInt;
        while (HasBeenRolled == false)
        {
            RandomInt = Random.Range(1, 11);
            if (RandomInt.ToString() != DiceRoll.text)
            {
                DiceRoll.text = RandomInt.ToString();
                yield return new WaitForSeconds(.2F);
            }
        }
        RolledNumber = int.Parse(DiceRoll.text);
        Player.CurrentRoll = RolledNumber;
    }

    private void OnDisable()
    {
        Player.PlayerInput.OnVariableChangeInputA -= StopTheDice;
    }

    private void OnDestroy()
    {
        Player.PlayerInput.OnVariableChangeInputA -= StopTheDice;
    }

}
