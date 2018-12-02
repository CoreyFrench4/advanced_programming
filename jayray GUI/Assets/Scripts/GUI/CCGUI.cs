using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCGUI : MonoBehaviour
{

    float scrW = Screen.width / 16;
    float scrH = Screen.height / 9;
    public string currentClass;
    public bool isBarbarian = true;
    public GameObject barbHat, rogueHat;
    public int baseStr, baseDex, baseInt, baseCha, currentStr, currentDex, currentIntel, currentCha;
    public int strMod, dexMod, intelMod, chaMod;
    public int freePoints = 10;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isBarbarian)
        {
            currentClass = "Barbarian";
            barbHat.SetActive(true);
            rogueHat.SetActive(false);
            baseStr = 8;
            baseDex = 4;
            baseInt = 3;
            baseCha = 5;
        }
        else
        {
            currentClass = "Rogue";
            rogueHat.SetActive(true);
            barbHat.SetActive(false);
            baseStr = 3;
            baseDex = 7;
            baseInt = 5;
            baseCha = 6;
        }
        currentStr = baseStr + strMod;
        currentDex = baseDex + dexMod;
        currentIntel = baseInt + intelMod;
        currentCha = baseCha + chaMod;
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(2.5f * scrW, 2f * scrH, 3 * scrW, scrH), "Change Class"))
        {
            isBarbarian = !isBarbarian;
        }
        GUI.Box(new Rect(2.5f * scrW, 1f * scrH, 3 * scrW, scrH), currentClass);
        GUI.Box(new Rect(10 * scrW, 1f * scrH, 0.5f * scrW, 0.5f * scrH), currentStr.ToString());
        GUI.Box(new Rect(10 * scrW, 1.5f * scrH, 0.5f * scrW, 0.5f * scrH), currentDex.ToString());
        GUI.Box(new Rect(10 * scrW, 2f * scrH, 0.5f * scrW, 0.5f * scrH), currentIntel.ToString());
        GUI.Box(new Rect(10 * scrW, 2.5f * scrH, 0.5f * scrW, 0.5f * scrH), currentCha.ToString());
        GUI.Box(new Rect(10 * scrW, 3f * scrH, 0.5f * scrW, 0.5f * scrH), freePoints.ToString());
        if (GUI.Button(new Rect(10 * scrW, 4f * scrH, 2 * scrW, 1 * scrH), "Save to file"))
        {

        }
        if (GUI.Button(new Rect(10 * scrW, 5f * scrH, 2 * scrW, 1 * scrH), "Save to Database"))
        {

        }
        if (GUI.Button(new Rect(10.5f * scrW, 1f * scrH, 0.5f * scrW, 0.5f * scrH), "+"))
        {
            if (freePoints > 0)
            {
                strMod++;
                freePoints--;
            }

        }
        if (GUI.Button(new Rect(10.5f * scrW, 1.5f * scrH, 0.5f * scrW, 0.5f * scrH), "+"))
        {
            if (freePoints >0)
            {
                dexMod++;
                
                freePoints--;
            }

        }
        if (GUI.Button(new Rect(10.5f * scrW, 2f * scrH, 0.5f * scrW, 0.5f * scrH), "+"))
        {
            if (freePoints >0)
            {
                intelMod++;
                freePoints--;
            }

        }
        if (GUI.Button(new Rect(10.5f * scrW, 2.5f * scrH, 0.5f * scrW, 0.5f * scrH), "+"))
        {
            if (freePoints >0)
            {
                chaMod++;
                freePoints--;
            }

        }


        if (strMod >= 1)
        {
            if (GUI.Button(new Rect(9.5f * scrW, 1f * scrH, 0.5f * scrW, 0.5f * scrH), "-"))
            {
                if (freePoints < 10)
                {
                    strMod--;
                    freePoints++;
                }

            }

        }
        if (dexMod >= 1)
        {
            if(GUI.Button(new Rect(9.5f * scrW, 1.5f * scrH, 0.5f * scrW, 0.5f * scrH), "-"))
            {
                if (freePoints < 10)
                {
                    dexMod--;
                    freePoints++;
                }
            }

        }
        if (intelMod >= 1)
        {
            if (GUI.Button(new Rect(9.5f * scrW, 2f * scrH, 0.5f * scrW, 0.5f * scrH), "-"))
            {
                if (freePoints < 10)
                {
                    intelMod--;
                    freePoints++;
                }
            }

        }
        if (chaMod >= 1)
        {
            if (GUI.Button(new Rect(9.5f * scrW, 2.5f * scrH, 0.5f * scrW, 0.5f * scrH), "-"))
            {
                if (freePoints < 10)
                {
                    chaMod--;
                    freePoints++;
                }
            }

        }


    }
}
