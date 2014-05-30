using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float life = 0;
	// Update is called once per frame
	void Update () {
        life -= Time.deltaTime;
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }



}
