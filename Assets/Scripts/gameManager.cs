using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI newHighScoreText;
    public TextMeshProUGUI abilityDmgText;
    public TextMeshProUGUI abilityCooldownText;
    public TextMeshProUGUI abilitySpeedText;
    public TextMeshProUGUI abilitySizeText;

    public GameObject gameUi;
    public GameObject gameOverScreen;
    public GameObject levelUpScreen;
    public Button restartButton;

    public GameObject player;
    public PlayerLogic playerLogic;
    public SpawnManager spawnManager;
    public Ability ability;

    public bool isGameActive;

    public float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
            // TODO: Change this to look nicer, with minutes and seconds and stuff
            timerText.text = "" + Mathf.Round(timer);
        }
    }

    // Start the game, remove title screen, reset score, and adjust spawnRate based on difficulty button clicked
    public void StartGame()
    {
        isGameActive = true;
        gameUi.gameObject.SetActive(true);
        spawnManager.StartSpawning();
        ability.setDefaults();

        ResetEnemyStats();
        UpdateAllText();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void AddExperienceToPlayer(float experience)
    {
        playerLogic.GainExperience(experience);
    }

    public void UpdateAllText()
    {
        UpdateLevelText();
        UpdateExperienceText();
        UpdateHealthText();
        UpdateAbilityText();
    }

    public void UpdateLevelText()
    {
        levelText.text = "Level: " + playerLogic.level;
    }

    public void UpdateExperienceText()
    {
        experienceText.text = "Exp: " + playerLogic.xp + "/" + playerLogic.xpNeeded;
    }    

    public void UpdateHealthText()
    {
        healthText.text = "Health: " + playerLogic.hp + "/" + playerLogic.maxHp;
    }

    public void UpdateAbilityText()
    {
        abilityDmgText.text = "Weapon Damage: " + ability.attackPower;
        abilityCooldownText.text = "Weapon Cooldown: " + ability.coolDown;
        abilitySpeedText.text = "Weapon Speed: " + ability.projectileSpeed;
        abilitySizeText.text = "Weapon Size: " + ability.sizeModifier;
    }

    public void PlayerLeveledUp()
    {
        isGameActive = false;

        if (spawnManager.enemySpawnTime >= 1f)
        {
            spawnManager.enemySpawnTime -= 0.2f;
        }
        RaiseEnemyStats();
        spawnManager.enemiesToSpawn += 2;


        levelUpScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        UpdateExperienceText();
        UpdateLevelText();
    }

    public void ResetEnemyStats()
    {
        foreach (GameObject enemy in spawnManager.enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            enemyScript.SetDefaults();
        }
    }

    public void RaiseEnemyStats()
    {
        foreach(GameObject enemy in spawnManager.enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();

            enemyScript.hp *= 1.12f;
            enemyScript.attack *= 1.08f;
        }
    }

    public void LevelUpButtonClicked(string option)
    {
        if (option == "health")
        {
            float percentIncrease = playerLogic.maxHp * 0.10f;
            playerLogic.maxHp += percentIncrease;
        }
        else if (option == "damage")
        {
            float percentIncrease = ability.attackPower * 0.50f;
            ability.attackPower += percentIncrease;
        }
        else if (option == "cooldown")
        {
            float percentDecrease = ability.coolDown * 0.20f;
            ability.coolDown -= percentDecrease;
        }
        else if (option == "speed")
        {
            float percentIncrease = ability.projectileSpeed * 0.20f;
            ability.projectileSpeed += percentIncrease;
        }
        else if (option == "size")
        {
            float percentIncrease = ability.sizeModifier * 0.50f;
            ability.sizeModifier += percentIncrease;
        }

        levelUpScreen.gameObject.SetActive(false);
        UpdateAbilityText();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isGameActive = true;
    }

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameOverScreen.gameObject.SetActive(true);
        gameUi.gameObject.SetActive(false);
        levelUpScreen.gameObject.SetActive(false);
        newHighScoreText.gameObject.SetActive(false);
        isGameActive = false;
        if (timer > MainManager.Instance.HighScore)
        {
            newHighScoreText.gameObject.SetActive(true);
            MainManager.Instance.UpdateHighScore(timer);
        }
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Back to main menu by switching scene
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
