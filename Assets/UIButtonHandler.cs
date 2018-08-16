using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour {
	
	public void replayFight(){
		PlayerDataManager.playerHealth.ResetHealth();
		EnemyBehavior.enemyHealth.ResetHealth();
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

}
