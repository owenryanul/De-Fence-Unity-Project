using UnityEngine;
using System.Collections;

public class Enemy_Tank_BarrierDamage : MonoBehaviour {

    private int barrierHealth;
    public GameObject damageEffectEmitter;
    public GameObject barrierDestroyedEffectEmitter;

	// Use this for initialization
	void Start () {
        barrierHealth = 10;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void damageBarrier()
    {
        print("Barrier Damaged");
        barrierHealth--;
        Instantiate(damageEffectEmitter, this.gameObject.transform.position, new Quaternion(0, 180, 180, 0));
        if (barrierHealth <= 0)
        {
            Instantiate(barrierDestroyedEffectEmitter, this.gameObject.transform.position, new Quaternion(0, 180, 180, 0));
            Destroy(this.gameObject);
        }
    }
}
