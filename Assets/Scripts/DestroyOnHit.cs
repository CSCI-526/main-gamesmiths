using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    private int hitByCurrent = 0;  // 0  is false
    private int hitByGhost = 0;
    private int hasChangedColor = 0;

    private SpriteRenderer spriteRenderer;

    private Color initialColor = new Color(104f / 255f, 58f / 255f, 32f / 255f);
    private Color hitOnceColor = new Color(182f / 255f, 135f / 255f, 108f / 255f);

    private int round;

    // Start is called before the first frame update
    void Start()
    {
        round = PlayerPrefs.GetInt("round", 1); // get the current round
        spriteRenderer = GetComponent<SpriteRenderer>();

        Debug.Log("Destroy on hit - enter round: " + round);

        if (round == 1)
        {
            spriteRenderer.color = initialColor;
            SaveColor();
            hitByCurrent = 0;
            hitByGhost = 0;
            hasChangedColor = 0;
            SaveStatus();

        }
        else if (round == 2)
        {
            LoadColor(); // ������ɫ
            LoadStatus();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHit(string bulletType)
    {
        if(bulletType == "Current")
        {
            hitByCurrent = 1;
        }
        else if(bulletType == "Ghost")
        {
            hitByGhost = 1; 
        }

        Debug.Log("OnHit -- Current: " + hitByCurrent + " Ghost�� " + hitByGhost);

        if (hasChangedColor == 0)  // hitted by current or ghost for the first time
        {
            ChangeColor();
            SaveColor();
            hasChangedColor = 1;
            SaveStatus();
        }

        if(hitByCurrent == 1 && hitByGhost == 1)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeColor()
    {
        if (spriteRenderer != null)
        {
            if (!IsColorEqual(spriteRenderer.color, hitOnceColor))
            {
                spriteRenderer.color = hitOnceColor;
            }
        }
    }

    private void SaveColor()
    {
        Color currentColor = spriteRenderer.color;

        // ������ɫ�� PlayerPrefs
        PlayerPrefs.SetFloat(gameObject.name + "_R", currentColor.r);
        PlayerPrefs.SetFloat(gameObject.name + "_G", currentColor.g);
        PlayerPrefs.SetFloat(gameObject.name + "_B", currentColor.b);
        PlayerPrefs.SetFloat(gameObject.name + "_A", currentColor.a);
        PlayerPrefs.Save();
    }

    private void SaveStatus()
    {
        PlayerPrefs.SetInt(gameObject.name + "_hitByCurrent", hitByCurrent);
        PlayerPrefs.SetInt(gameObject.name + "_hitByGhost", hitByGhost);
        PlayerPrefs.SetInt(gameObject.name + "_hasChangedColor", hasChangedColor);
    }

    private bool IsColorEqual(Color currentColor, Color targetColor, float tolerance = 0.01f)
    {
        return Mathf.Abs(currentColor.r - targetColor.r) < tolerance &&
               Mathf.Abs(currentColor.g - targetColor.g) < tolerance &&
               Mathf.Abs(currentColor.b - targetColor.b) < tolerance &&
               Mathf.Abs(currentColor.a - targetColor.a) < tolerance;
    }


    void LoadColor()
    {
        Debug.Log("LoadColor -- object name: " + gameObject.name);

        if (PlayerPrefs.HasKey(gameObject.name + "_R")) // ����Ƿ��д洢����ɫ
        {
            float r = PlayerPrefs.GetFloat(gameObject.name + "_R");
            float g = PlayerPrefs.GetFloat(gameObject.name + "_G");
            float b = PlayerPrefs.GetFloat(gameObject.name + "_B");
            float a = PlayerPrefs.GetFloat(gameObject.name + "_A");

            spriteRenderer.color = new Color(r, g, b, a); // �ָ���ɫ
        }
    }

    void LoadStatus()
    {
        hitByCurrent = PlayerPrefs.GetInt(gameObject.name + "_hitByCurrent"); 
        hitByGhost = PlayerPrefs.GetInt(gameObject.name + "hitByGhost"); 
        hasChangedColor = PlayerPrefs.GetInt(gameObject.name + "hasChangedColor"); 
    }

}
