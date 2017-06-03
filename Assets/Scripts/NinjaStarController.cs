using UnityEngine;
using System.Collections;

public class NinjaStarController : MonoBehaviour {

	float starSpeed = 12.0f;
	float rotationSpeed = 20.0f;
    GameController _gameCtroller;
	
	void Start () {
		this.GetComponent<Rigidbody>().velocity = this.transform.up * starSpeed;
		this.GetComponent<Rigidbody>().angularVelocity = this.transform.forward * rotationSpeed;
    }
	
	void OnCollisionEnter (Collision collision) {
		if (collision.collider.tag == "Enemy") {
			collision.collider.GetComponent<EnemyMover>().DieSoon();
			Destroy(gameObject);
		}
        if (collision.collider.tag == "Mini Boss")
        {
            collision.collider.GetComponent<EnemyMover>().bossDieSoon();
            Destroy(gameObject);
        }
    }
	
}
