using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitChecker : MonoBehaviour {
    AudioSource hitSound;
    //PlayerHealth player;

    void Start()
    {
        hitSound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Girl")
        {
            hitSound.Play();
            GameObject player = GameObject.Find("MainPlayer");
            player.GetComponent<PlayerHealth>().TakeDamage();
        }
    }
}