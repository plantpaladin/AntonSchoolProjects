using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour {
    private float speed = 1;
    private int damage = 1;
    private float endOfLine = -1;
    private ComboHandler target;
    private bool spell = false;
    //end of line is the x value which marks that the object hastravelled far enough to hit the player
	public void setVariables (float newSpeed, int newDamage, float newEndOfLine,bool ifSpell, ComboHandler newTarget) 
    {
        speed = newSpeed;
        damage = newDamage;
        endOfLine = newEndOfLine;
        target = newTarget;
        spell = ifSpell;
	}
    void Update()
    {

        Vector3 newposition = transform.position;
        newposition.x -= speed*Time.deltaTime;
        if (newposition.x <= endOfLine)
        {
            if (spell == false)
            {
                target.recieveAttack(damage);
            }
            else
            {
                target.recieveSpell(damage);
            }
            Object.Destroy(this.gameObject);
        }
        else
        {
            transform.position = newposition;
        }

    }
	
}
