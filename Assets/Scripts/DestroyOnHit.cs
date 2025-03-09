using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    private int hitByCurrent = 0;
    private int hitByGhost = 0;

    private SpriteRenderer spriteRenderer;
    private int round;

    void Start()
    {
        round = PlayerPrefs.GetInt("round", 1);
        spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("DestroyOnHit - Enter Round: " + round);

        if (round == 1)
        {
            // Initialize hit statuses for the first round
            hitByCurrent = 0;
            hitByGhost = 0;
            SaveStatus();
        }
        else if (round == 2)
        {
            LoadStatus(); // Load the saved status for replay
        }
    }

    public void OnHit(string bulletType)
    {
        if (bulletType == "Current")
        {
            hitByCurrent = 1;
        }
        else if (bulletType == "Ghost")
        {
            hitByGhost = 1;
        }

        Debug.Log("OnHit -- Current: " + hitByCurrent + ", Ghost: " + hitByGhost);

        SaveStatus();

        // Destroy the block if hit by both bullets
        if (hitByCurrent == 1 && hitByGhost == 1)
        {
            Debug.Log("Block Destroyed!");
            Destroy(gameObject);
        }
    }

    private void SaveStatus()
    {
        PlayerPrefs.SetInt(gameObject.name + "_hitByCurrent", hitByCurrent);
        PlayerPrefs.SetInt(gameObject.name + "_hitByGhost", hitByGhost);
        PlayerPrefs.Save();
    }

    private void LoadStatus()
    {
        hitByCurrent = PlayerPrefs.GetInt(gameObject.name + "_hitByCurrent", 0);
        hitByGhost = PlayerPrefs.GetInt(gameObject.name + "_hitByGhost", 0);
    }




}