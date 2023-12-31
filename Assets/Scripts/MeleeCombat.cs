using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeleeCombat : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public AudioSource AudioSource;
    public AudioClip punchclip;
    public AudioClip killclip;
    private TextMeshProUGUI killCounter;
    private int kills = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Melee();
        }
    }
    void Start() {
        killCounter = GameObject.Find("Killcounter").GetComponent<TextMeshProUGUI>();
    }
    void Melee()
    {

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            AudioSource.PlayClipAtPoint(punchclip, enemy.gameObject.transform.position);
            AudioSource.PlayClipAtPoint(killclip, enemy.gameObject.transform.position);
            kills ++;
            killCounter.text = "Enemies killed: "+kills;
            Destroy(enemy.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}


