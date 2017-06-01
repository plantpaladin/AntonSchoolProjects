using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHandler : MonoBehaviour {
    
    public class Spells//
    { }//will be implemented later

    public int health = 10;
    public float attackSpeed = 1;
    public float attackMoveSpeed = 1;
    public float endOfLine = -5;
    public int attackDamage = 1;
    public GameObject healthBar;
    public GameObject attackPrefab;
    private HealthBarText healthBarText;
    
	// Use this for initialization
	void Awake () {
        InvokeRepeating("Attack",1,attackSpeed);
        healthBarText = healthBar.GetComponent<HealthBarText>();
        healthBarText.setVariables("EnemyHealth =", health);
	}
   
    void Attack()
    {
        GameObject newAttack = Instantiate(attackPrefab);
        newAttack.GetComponent<EnemyAttackScript>().setVariables(attackMoveSpeed, attackDamage, endOfLine,false,GetComponent<ComboHandler>());
    }
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            int a = SceneManager.GetActiveScene().buildIndex;
            a = (a + 1) %2;
            SceneManager.LoadScene(a);
        }
        else
        {
            healthBarText.setSliderValue(health);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

}
