using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        //there can only be one
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(dialogueManager.gameObject);
            Destroy(hud);
            Destroy(menu.gameObject);
            return;
        }

        instance = this;
        //SceneManager.sceneLoaded += SaveState;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        //DontDestroyOnLoad(gameObject);
    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;

    public List<int> weaponPrices;
    public List<int> expGuide;

    //refs
    public Player player;
    public Weapon weapon;
    public DialogueManager dialogueManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public CharMenu menu;

    //stuff
    public int gold;
    public int experience;

    //save game
    public void SaveState()
    {
        //Debug.Log("Save Game");

        string s = "";

        s += menu.currCharSelect + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLvl.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    //load game
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        //Debug.Log("Load Game");

        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //get player skin
        menu.SetChar(int.Parse(data[0]));

        //get gold
        gold = int.Parse(data[1]);

        //get exp
        experience = int.Parse(data[2]);
        if (GetCurrLvl() != 1)
            player.SetLvl(GetCurrLvl());

        //get weapon lvl
        weapon.SetWeaponLvl(int.Parse(data[3]));
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void ShowText(string msg, int size, Color c, Vector3 pos, Vector3 motion, float dur)
    {
        dialogueManager.Show(msg, size, c, pos, motion, dur);
    }

    public bool tryUpgrade()
    {
        //check max lvl
        if (weaponPrices.Count <= weapon.weaponLvl)
            return false;

        if (gold >= weaponPrices[weapon.weaponLvl])
        {
            gold -= weaponPrices[weapon.weaponLvl];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //health bar manager
    public void OnHitpointChange()
    {
        float ratio = (float)player.hp / (float)player.maxHp;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //player lvl manager
    public int GetCurrLvl()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += expGuide[r];
            r++;

            if (r == expGuide.Count) // Max Level
                return r;
        }

        return r;
    }

    //exp till next lvl
    public int GetXpToLvl(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += expGuide[r];
            r++;
        }

        return xp;
    }

    //got exp
    public void GainXp(int xp)
    {
        int currLevel = GetCurrLvl();
        experience += xp;
        if (currLevel < GetCurrLvl())
            OnLevelUp();
    }

    //lvled up
    public void OnLevelUp()
    {
        Debug.Log("Level up!");
        player.OnLvlUp();
        OnHitpointChange();
    }

    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
}
