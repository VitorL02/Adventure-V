using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private GameController _GameController;


	private Rigidbody2D playerRb;
	private Animator playerAnimator;
	public Transform chao;
	public bool isChao;
	public float speed;
	public float jumpforce;
	public bool  olhandoesquerda;
	private bool isAtaque;
	public SpriteRenderer playerSr;

	public Transform mao;
	public GameObject hitBoxPrefab;

	public Color hitColor;
	public Color invColor;


	// Use this for initialization

	void Start () {
		playerRb = GetComponent <Rigidbody2D> ();
		playerAnimator = GetComponent <Animator> ();
		playerSr = GetComponent <SpriteRenderer> ();
		_GameController = FindObjectOfType(typeof(GameController))as GameController;
		_GameController.playerTransform = this.transform;
	

	}

	// Update is called once per frame
	void Update () {

		playerAnimator.SetBool ("isChao", isChao);
		if (_GameController.currentState != gameState.GAMEPLAY) {
			playerRb.velocity = new Vector2 (0, playerRb.velocity.y);
			playerAnimator.SetInteger ("h", 0);
			return;
		}
		float h = Input.GetAxisRaw ("Horizontal");
		if (isAtaque == true && isChao==true) {

			h = 0;



		}
		//CHAMA FUNÇAO DE VIRA PRA ESQUERDA OU DIREITA
		if (h > 0 && olhandoesquerda == true) {

			Vira ();

		} else if (h < 0 && olhandoesquerda == false) {
			Vira ();
		}
		//FIM DA FUNÇÃO

		float speedY = playerRb.velocity.y; 

		if (Input.GetButtonDown ("Jump") && isChao == true ) {
			
			_GameController.playSfx (_GameController.sfxjump, 0.05f);
			playerRb.AddForce (new Vector2 (0, jumpforce));
		}
		if (Input.GetButtonDown("Fire1")&& isAtaque== false ){
			_GameController.playSfx (_GameController.sfxAtaque, 0.05f);
			isAtaque = true;
			playerAnimator.SetTrigger("ataque");

		}

		playerRb.velocity = new Vector2 (h * speed, speedY);
		playerAnimator.SetInteger ("h", (int)h);
		playerAnimator.SetFloat ("speedY", speedY);
		playerAnimator.SetBool ("isAtaque", isAtaque);


	}

	void FixedUpdate(){
		
		isChao = Physics2D.OverlapCircle (chao.position, 0.02f);


	}


	// FUNÇAÕ DE VIRAR ELE

	void Vira(){
		
		olhandoesquerda = !olhandoesquerda; //! = CONTRARIO, retorna a variavavel a falso
		//FLOAT PRA SABER DIFERENÇA  de direção
		float x = transform.localScale.x * -1;
		transform.localScale= new Vector3(x,transform.localScale.y,transform.localScale.z);   // Retorna Pra posição inicial

	}
	void TerminaAtac(){

		isAtaque = false;


	}
	void hitboxAtaque(){
		GameObject hitBoxtemp = Instantiate (hitBoxPrefab, mao.position, transform.localRotation);
		Destroy (hitBoxtemp, 0.2f);
	}
	void passo(){

	_GameController.playSfx(_GameController.sfxpasso[Random.Range(0,_GameController.sfxpasso.Length)],0.1f);
	}


	void OnTriggerEnter2D(Collider2D col){
		
		if (col.gameObject.tag == "Coletavel") {
			_GameController.getCoin ();
			_GameController.playSfx (_GameController.sfxMoeda, 0.05f);
			Destroy (col.gameObject);

		} else if (col.gameObject.tag == "damage") {
			_GameController.getHit ();
			if (_GameController.vida > 0) {
				StartCoroutine ("danocontrol");
			}
		} else if (col.gameObject.tag == "abismo") {

			_GameController.playSfx (_GameController.sfxDano, 0.05f);
			_GameController.vida = 0;
			_GameController.heartController ();
			_GameController.painelGameOver.SetActive (true);
			_GameController.currentState = gameState.GAMEOVER;

		}
		else if(col.gameObject.tag=="flag") {
			_GameController.theEnd();
		}
	}

	IEnumerator danocontrol(){
		_GameController.playSfx (_GameController.sfxDano, 0.05f);
		this.gameObject.layer = LayerMask.NameToLayer ("Invencivel");
		playerSr.color =hitColor;
		yield return new WaitForSeconds (0.2f);
		playerSr.color = invColor;
		for (int i = 0; i < 4; i++) {

		playerSr.enabled = false;
		yield return new WaitForSeconds (0.2f);
		playerSr.enabled=true;
		yield return new WaitForSeconds (0.2f);
		}
		this.gameObject.layer = LayerMask.NameToLayer ("Player");
		playerSr.color = Color.white;
	}

} 
