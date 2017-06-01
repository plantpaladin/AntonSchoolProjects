using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour {
    public GameObject spellPrefab;
    public float spellMoveSpeed;
    public int spellDamage;
    public float spellCastSpeed;
    public float endOfLine;
	// Use this for initialization
	
    void Start () 
    {
        Invoke("CastSpell", 1);
	}

    void CastSpell()
    {
        GameObject newAttack = Instantiate(spellPrefab);
        newAttack.GetComponent<EnemyAttackScript>().setVariables(spellMoveSpeed, spellDamage, endOfLine,true, GetComponent<ComboHandler>());
        Vector3 newVector = newAttack.transform.position;
        newVector.y += 0.5f;
        newAttack.transform.position = newVector;
        Invoke("CastSpell", spellCastSpeed);
 
    }
}
