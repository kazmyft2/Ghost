using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SwordAttack : MonoBehaviour {

    AudioSource ghostCry;
    public GameObject effectPrefab;
    public Vector3 effectRotation;
    public Collider swordcol;

    void Start()
    {
        ghostCry = GetComponent<AudioSource>();
        swordcol.GetComponent<CapsuleCollider>().enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            other.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(GhostDestroy(other));
        }
    }

    private IEnumerator GhostDestroy(Collider other)
    {
        if (effectPrefab != null)
        {
            Instantiate(
                effectPrefab,
                other.transform.position,
                Quaternion.Euler(effectRotation));
            yield return new WaitForSeconds(0.2f);
            Destroy(other.gameObject);
            ghostCry.Play();
        }
    }

    public void AttackStart()
    {
        swordcol.enabled = true;
    }
    public void AttackEnd()
    {
        swordcol.enabled = false;
    }

}
