using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharMenu : MonoBehaviour
{
    public Text lvlTxt;
    public Text hpTxt;
    public Text goldTxt;
    public Text upgradeCostTxt;
    public Text expText;

    public int currCharSelect = 0;
    public Image charSelectSprite;
    public Image weaponSprite;
    public RectTransform expBar;

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Char Selection
    public void SetChar(int index)
    {
        currCharSelect = index;
        OnSelectChanged();
    }

    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currCharSelect++;

            if (currCharSelect == GameManager.instance.playerSprites.Count)
                currCharSelect = 0;

            OnSelectChanged();
        }
        else
        {
            currCharSelect--;

            if (currCharSelect < 0)
                currCharSelect = GameManager.instance.playerSprites.Count - 1;

            OnSelectChanged();
        }
    }

    private void OnSelectChanged()
    {
        charSelectSprite.sprite = GameManager.instance.playerSprites[currCharSelect];
        GameManager.instance.player.SwapSprite(currCharSelect);
    }
    //end char selection
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void OnUpgradeClick()
    {
        if (GameManager.instance.tryUpgrade())
            UpdateMenu();
    }

    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLvl];
        if (GameManager.instance.weapon.weaponLvl == GameManager.instance.weaponPrices.Count)
            upgradeCostTxt.text = "MAX";
        else
            upgradeCostTxt.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLvl].ToString();


        lvlTxt.text = GameManager.instance.GetCurrLvl().ToString();
        hpTxt.text = GameManager.instance.player.hp.ToString();
        goldTxt.text = GameManager.instance.gold.ToString();


        int currLevel = GameManager.instance.GetCurrLvl();
        if (currLevel == GameManager.instance.expGuide.Count)
        {
            expText.text = GameManager.instance.experience.ToString() + " total exp"; 
            expBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLvl(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLvl(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)diff;
            expBar.localScale = new Vector3(completionRatio, 1, 1);
            expText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }
}
