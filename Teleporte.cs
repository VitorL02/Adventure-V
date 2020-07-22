using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporte : MonoBehaviour {

	private GameController _GameController;
	public Transform PontoSaida;
	public Transform posCamera;
	public Transform limitCamDir,limitCamEsq,limitCamSup,limitCamBaix;
	public musicaFase novaMusica;
	void Start () {
		
		_GameController = FindObjectOfType (typeof(GameController))as GameController;

	}
	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag == "Player") {
			_GameController.trocarMusica (musicaFase.CAVERNA);
			col.transform.position = PontoSaida.position;
			Camera.main.transform.position = posCamera.position;

			_GameController.LimitCamBaix = limitCamBaix;
			_GameController.LimitCamDir = limitCamDir;
			_GameController.LimitCamEsq = limitCamEsq;
			_GameController.LimitCamSup = limitCamSup;
		}
	}
	}

