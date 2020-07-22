using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIA : MonoBehaviour {

	private GameController _GameController;
	private bool isseguir;
	public float speed;
	public bool olhandoesquerda;
	public GameObject hitboxMr;
	private Rigidbody2D morcegoRB;
	private Animator MorcegoAnimator;
	void Start () {
		
		_GameController = FindObjectOfType (typeof(GameController))as GameController;
		morcegoRB = GetComponent<Rigidbody2D> ();
		MorcegoAnimator = GetComponent<Animator>();

	}


	void Update () {
		if (_GameController.currentState != gameState.GAMEPLAY) {
			return;
		}
		if (isseguir == true) {
			transform.position = Vector3.MoveTowards(transform.position,_GameController.playerTransform.position,speed*Time.deltaTime); 
		}
		if (transform.position.x < _GameController.playerTransform.position.x && olhandoesquerda == true) {
			Vira ();
		} else if (transform.position.x > _GameController.playerTransform.position.x && olhandoesquerda == false)
			Vira ();
		
	}
	void OnBecameVisible(){
		isseguir = true;

	}
	void Vira(){

		olhandoesquerda = !olhandoesquerda; //! = CONTRARIO, retorna a variavavel a falso
		//FLOAT PRA SABER DIFERENÇA  de direção
		float x = transform.localScale.x * -1;
		transform.localScale= new Vector3(x,transform.localScale.y,transform.localScale.z);   // Retorna Pra posição inicial

	}
	void  OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "HitBox") {
		Destroy (hitboxMr);
		_GameController.playSfx (_GameController.sfxMortInim, 0.03f);
		MorcegoAnimator.SetTrigger ("Morte");
	}
	} 

void OnDead (){
			Destroy(this.gameObject);
		}


}
