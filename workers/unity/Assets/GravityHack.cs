using UnityEngine;

public class GravityHack : MonoBehaviour
{
    public float Force;

	// Update is called once per frame
	void FixedUpdate () {
	    GetComponent<Rigidbody>().AddForce(new Vector3(0, -Force, 0), ForceMode.Acceleration);
	}
}
