using UnityEngine;

public class GhostMoveing : MonoBehaviour {
    public float amplitube;
    public float speed;

    Vector3 startPosition;

    // Use this for initialization
    void Start () 
    {
        startPosition = transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () 
    {
        float y = amplitube * Mathf.Sin(Time.time * speed);
        transform.localPosition = startPosition + new Vector3(0, y, 0);
    }
}
