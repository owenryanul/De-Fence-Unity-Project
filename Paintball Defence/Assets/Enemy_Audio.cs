using UnityEngine;
using System.Collections;

public class Enemy_Audio : MonoBehaviour {

    private float soundCooldown;
    private AudioSource soundSource;
    public AudioClip grrSound;

	// Use this for initialization
	void Start () {
        soundCooldown = 5;
        soundSource = this.gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (soundCooldown <= 0)
        {
            soundSource.PlayOneShot(grrSound);
            soundCooldown = 5;
        }
        else
        {
            soundCooldown -= Time.deltaTime;
        }
    }
}
