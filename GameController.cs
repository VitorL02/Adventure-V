using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum musicaFase{
	FLORESTA,CAVERNA 

}
public enum gameState
{
	TITULO,GAMEPLAY,END,GAMEOVER
}

public class GameController : MonoBehaviour {
	
	public gameState currentState;
	public GameObject painelTitulo, painelGameOver, painelEnd;

	public float speedCam;
	public Transform LimitCamDir, LimitCamEsq, LimitCamBaix, LimitCamSup;
	public Transform playerTransform;
	private Camera cam;

	[Header("Audio")]
	public AudioSource SFX;
	public AudioSource MUSICA;

	public AudioClip sfxjump;
	public AudioClip sfxMoeda;
	public AudioClip sfxAtaque;
	public AudioClip sfxMortInim;
	public AudioClip[] sfxpasso;
	public AudioClip sfxDano;
	public GameObject[] fase;
	public musicaFase   musicaAtual;
	public AudioClip musicaFloresta, musicaCaverna;

	public int  moedas;
	public Text  moedasTxt;
	public Image[] cora;
	public int   vida;

	void Start () {

		cam = Camera.main;

		heartController ();


	}
	
	// Update is called once per frame
	void Update () {
		if (currentState == gameState.TITULO && Input.GetKeyDown (KeyCode.Space)) {

			currentState = gameState.GAMEPLAY;
			painelTitulo.SetActive (false);
		} 
		else if (currentState == gameState.GAMEOVER && Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
		else if (currentState == gameState.GAMEOVER && Input.GetKeyDown (KeyCode.Space)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	void LateUpdate(){

		float posCamX = playerTransform.position.x;
		float posCamY = playerTransform.position.y;

		if (cam.transform.position.x < LimitCamEsq.position.x && playerTransform.position.x < LimitCamEsq.position.x)
		
		{

			posCamX = LimitCamEsq.position.x; 
		}
		else if (cam.transform.position.x > LimitCamDir.position.x && playerTransform.position.x > LimitCamDir.position.x)
		{

			posCamX = LimitCamDir.position.x; 
		}
	
		if (cam.transform.position.y < LimitCamBaix.position.y && playerTransform.position.y < LimitCamBaix.position.y)

		{

			posCamY =LimitCamBaix.position.y; 
		}
		else if (cam.transform.position.y > LimitCamSup.position.y && playerTransform.position.y > LimitCamSup.position.y)

		{

			posCamY =LimitCamSup.position.y; 
		}

		

		Vector3 posCam= new Vector3(posCamX,posCamY,cam.transform.position.z);
		cam.transform.position =  Vector3.Lerp(cam.transform.position,posCam,speedCam * Time.deltaTime);
	}
	public void  playSfx(AudioClip sfxclip,float volume){

		SFX.PlayOneShot (sfxclip, volume);

	} 
	public void getCoin(){
		moedas += 1;
		moedasTxt.text = moedas.ToString ();
	}
	public void getHit(){

		vida -= 1;
		heartController ();
		if (vida <= 0) {

			playerTransform.gameObject.SetActive (false);
			painelGameOver.SetActive (true);
			currentState = gameState.GAMEOVER;

		}

	}


	public void trocarMusica(musicaFase novaMusica){

		AudioClip clip = null;
		switch (novaMusica) {

		case musicaFase.CAVERNA:
			clip = musicaCaverna;
			break;
		case musicaFase.FLORESTA:
			clip = musicaFloresta;
			break;
		}
		StartCoroutine ("controleMusica", clip);
	}
	IEnumerator controleMusica(AudioClip musica)
	{
		float volumeMaximo = MUSICA.volume;
		for (float volume = volumeMaximo; volume > 0; volume -= 0.01f) {
			MUSICA.volume = volume;

			yield return new WaitForEndOfFrame ();
		}
		MUSICA.clip = musica;
		MUSICA.Play ();
		for (float volume = 0; volume < volumeMaximo ; volume += 0.01f) {
			MUSICA.volume = volume;

			yield return new WaitForEndOfFrame ();
		}
		 

	}
	public void heartController (){
		foreach (Image H in cora) {
			H.enabled = false;
		}
		for (int v = 0; v<vida; v++)
		{
			cora [v].enabled = true;
	}

	}
	public void theEnd (){
		
		currentState = gameState.END;
		painelEnd.SetActive (true);
	}

}



