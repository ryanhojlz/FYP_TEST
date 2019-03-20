using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBase : MonoBehaviour
{
    public float CastleBaseHealthValue;
    public float CastleBaseDefenceValue;
    private EndGameState temp; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CastleBaseHealthValue <= 0)
        {
            //end game here
            temp = new EndGameState();
            temp.EndGame();
        }
    }

    public float GetHealth()
    {
        return CastleBaseHealthValue;
    }

    public void SetHealth(float _castleBaseHealth)
    {
        CastleBaseHealthValue = _castleBaseHealth;
    }
}
