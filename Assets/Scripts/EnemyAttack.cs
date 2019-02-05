using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public GameObject fireBall;
    public float shotSpeed;
    public float shotTorque;
    public float span = 3f;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(span);
            Vector3 tmp = GameObject.Find("MainPlayer").transform.position;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(tmp - transform.position),
                0.3f);
            Fire();
        }
    }

    private void Fire()
    {
        GameObject fire = (GameObject)Instantiate(
            fireBall,
            Vector3.zero, 
            Quaternion.identity);

        fire.transform.SetParent(transform, false);

        Rigidbody fireBallRB = fire.GetComponent<Rigidbody>();
        fireBallRB.AddForce(transform.forward * shotSpeed);
        fireBallRB.AddTorque(new Vector3(0, shotTorque, 0));
    }
}
