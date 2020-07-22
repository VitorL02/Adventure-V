using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeIa : MonoBehaviour {
	private GameController _GameController;
	private Rigidbody2D slimeRB;
	private Animator slimeAnimator;
	public float   speed;
	public float timeToWalk;
	public GameObject hitboxSl;
	public bool olhandoesquerda;
	private int h;


	void Start () {

		_GameController = FindObjectOfType(typeof(GameController))as GameController;

		slimeRB = GetComponent<Rigidbody2D> ();
		slimeAnimator = GetComponent<Animator> (); 
		StartCoroutine ("slimeAnda");

		
	}
	

	void Update () {
		if (_GameController.currentState != gameState.GAMEPLAY) { return; }

		if (h > 0 && olhandoesquerda == true) {

			Vira ();

		} else if (h < 0 && olhandoesquerda == false) {
			Vira ();
		}

		slimeRB.velocity = new Vector3 (h * speed, slimeRB.velocity.y);
		if (h != 0) {
			slimeAnimator.SetBool ("slimeAnda", true);
		}
		else 
		{
			slimeAnimator.SetBool ("slimeAnda", false);
		} 

		
	}


	//coROTINA PODE SER PARADA E CONTINUADA
	IEnumerator slimeAnda(){
		int rand = Random.Range (0, 100);
		if (rand < 33) {
			h = -1;

		} else if (rand < 66) {
			h = 0;
		} 
		else if (rand <100) 
		{

			h = 1;
		}

		yield return new WaitForSeconds (timeToWalk);
		StartCoroutine ("slimeAnda");
	}

	void  OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.tag == "HitBox") {

			h = 0;
			StopCoroutine ("slimeAnda");
			Destroy (hitboxSl);
			_GameController.playSfx (_GameController.sfxMortInim, 0.03f);
			slimeAnimator.SetTrigger ("Dead");
		}
	}

	void OnDead (){
		Destroy(this.gameObject);
	}
	void Vira(){

		olhandoesquerda = !olhandoesquerda; //! = CONTRARIO, retorna a variavavel a falso
		//FLOAT PRA SABER DIFERENÇA  de direção
		float x = transform.localScale.x * -1;
		transform.localScale= new Vector3(x,transform.localScale.y,transform.localScale.z);   // Retorna Pra posição inicial

	}
}
