using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Create crossfade effect when loading Menu scene
/// </summary>
public class Go_Menu : MonoBehaviour {
	void OnMouseDown() {
		Invoke("CrossfadeDelayed",0.5f);
	}
		
	private void CrossfadeDelayed(){
		GameObject.Find("Crossfade").GetComponent<Animator>().SetBool("out",true);
		Invoke("ExitDelayed",2);
	}

	private void ExitDelayed(){
		
        SceneManager.LoadScene("Menu");
		
	}

}
