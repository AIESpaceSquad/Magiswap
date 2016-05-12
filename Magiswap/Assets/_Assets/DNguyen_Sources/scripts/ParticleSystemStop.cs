using UnityEngine;
using System.Collections;

public class ParticleSystemStop : MonoBehaviour {
	public ParticleSystem psystem;

	// Use this for initialization
	void Start () {
		psystem = this.gameObject.GetComponent<ParticleSystem>();
		psystem.Pause ();

	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
