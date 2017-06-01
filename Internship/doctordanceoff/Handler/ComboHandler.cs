using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ComboHandler : MonoBehaviour {

    public List<Vector3> Combos;//this is the combinations used to acess the specific combo, 
    //Remember to put the combo wanted for a specific combo at the same place as the comboID
    //if the list is smaller than the number of comboID then the higher numbers cant be triggered
    //if the list is larger then the comboID numbers then the numbers would have to be checked
    //the enums are set in the number they appear in the game, this is also why the list is set in the inspector
    public int playerHealth = 10;
    private int maxHealth;
    private EnemyHandler enemy;
    public GameObject healthBar;
    public GameObject blockPrefab;
    private HealthBarText healthText;
    private SpriteRenderer blockRenderer;
    private SpriteRenderer counterRenderer;
    private bool blocking;
    private bool counterSpell;

    public enum comboID
    {
        Heal,
        Attack,
        Block,
        MagicHeal,
        MagicAttack,
        CounterSpell,
        AttackAndHeal
    }

	void Awake () {
        maxHealth = playerHealth;
        healthText = healthBar.GetComponent<HealthBarText>();
        healthText.setVariables("PlayerHealth =", playerHealth);
        enemy = GetComponent<EnemyHandler>();
        GameObject block = Instantiate(blockPrefab);
        GameObject counter = Instantiate(blockPrefab);
        Vector3 newBlockPosition = new Vector3(-6, 4);
        Vector3 newCounterPosition = new Vector3(-6, 4.5f);
        block.transform.position = newBlockPosition;
        blockRenderer = block.GetComponent<SpriteRenderer>();
        blockRenderer.enabled = false;
        blocking = false;
        counter.transform.position = newCounterPosition;
        counterRenderer = counter.GetComponent<SpriteRenderer>();
        counterRenderer.enabled = false;
        counterSpell = false;
        
	}
    public bool comboChecker(Vector3 tiles)
    {
	for (int i = 0; i < Combos.Count;i++)
        {
            if(tiles==Combos[i])
            {
                activateCombo(i);
                return true;
            }
        }
        return false;
    }
    private void activateCombo(int i)
    {
        comboID c = (comboID)i;
        switch (c)
        {
            case (comboID.Heal):
                {
                    heal(1);
                    break;
                }
            case(comboID.Attack):
        {
            enemy.takeDamage(1);
            break;
        }
            case (comboID.Block):
        {
            blocking = true;
            blockRenderer.enabled = true;
            break;
        }
            case (comboID.MagicHeal):
        {
            heal(2);
            break;
        }
            case (comboID.MagicAttack):
        {
            enemy.takeDamage(2);
            break;
        }
            case (comboID.CounterSpell):
        {
            counterSpell = true;
            counterRenderer.enabled = true;
            break;
        }
            case(comboID.AttackAndHeal):
        {
            enemy.takeDamage(1);
            heal(1);
            break;
        }
        }
    }

    public void recieveAttack(int damage)
    {
        if (blocking == true)
        {//the attack is blocked
            blocking = false;
            blockRenderer.enabled = false;
        }
        else
        {
            playerHealth -= damage;
            if (playerHealth <= 0)
            {
                //restarts the level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                healthText.setSliderValue(playerHealth);
            }
        }
    }
    public void recieveSpell(int damage)
    {
        if (counterSpell == true)
        {//the attack is blocked
            counterSpell = false;
            counterRenderer.enabled = false;
        }
        else
        {
            playerHealth -= damage;
            if (playerHealth <= 0)
            {
                //restarts the level
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                healthText.setSliderValue(playerHealth);
            }
        }
    }

    private void heal(int healamount)
    {
        int newHealth = playerHealth + healamount;
        if (newHealth < maxHealth)
        {
            playerHealth = newHealth;
        }
        else
        {
            playerHealth = maxHealth;
        }
            healthText.setSliderValue(playerHealth);
        
    }
    


	// Update is called once per frame
	void Update () {
		
	}
}
