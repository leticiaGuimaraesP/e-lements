using UnityEngine;
using System.Collections;
using FThLib;
using UnityEngine.UI;

/// <summary>
/// It is the controller of the construction points
/// </summary>

public class CP_Controller : MonoBehaviour {
	private bool mouseover=false;
	public bool clicked =false;

	 [SerializeField] public GameObject buttonPrefab;

	 private GameObject buttonInst;

	 private bool buttonClicked = false;

[SerializeField] private Sprite tower, btTower;
    
    //Show hand
	// void OnMouseOver(){ 
	// 	//if(!GameObject.Find("hand")){master.showHand (true);}
	// 	mouseover=true;
	// }
	// //Hid hand
	// void OnMouseExit(){
	// 	//if(GameObject.Find("hand")){master.showHand (false);}
	// 	mouseover=false;
	// }
	// /// <summary>
    // /// Normalize the Z position relative to the Y position, and set the layer and name
    // /// </summary>
	// void Start () {
	// 	//this.transform.position = master.setThisZ(this.transform.position,0.02f);
	// 	this.gameObject.name="CP";
	// 	master.setLayer("interface",this.gameObject);
	// }
	
	// /// <summary>
    // /// It shows the Construction Point Interface on the Construction point clicked position
    // /// </summary>
	// void Update () {
	// 	if(!master.isFinish()){                                                                                                             //Game is not finished
	// 		if(master.getChildFrom("Interface",this.gameObject)==null&&clicked==false){GetComponent<CircleCollider2D>().enabled=true;}
	// 		if (Input.GetMouseButtonDown(0)&&mouseover==true){
    //             GameObject.Find("UI").GetComponent<AudioSource>().Play();
    //             master.showInterface(this.gameObject.name,this.gameObject,this.gameObject.transform);                                       //Show CP interface
	// 			GetComponent<CircleCollider2D>().enabled=false;
	// 		}
	// 	}
	// }

	private void OnMouseDown() {

		if(!buttonClicked){
			buttonInst = Instantiate(buttonPrefab, new Vector2(0f,0f), Quaternion.identity);
        Button bt = buttonInst.GetComponentInChildren<Button>();
		bt.onClick.AddListener(() => ButtonClick());
		Image buttonImage = bt.GetComponent<Image>();
		buttonImage.sprite = btTower;

		Vector2 buttonPosition = this.gameObject.transform.position+ new Vector3(0f, 2f, 0f);// Ajuste a posição vertical conforme necessário

		Vector3 screenPosition = Camera.main.WorldToScreenPoint(buttonPosition);

        // Obtenha o RectTransform do botão e defina sua posição
        bt.transform.position = screenPosition;

		buttonClicked = true;
		}
	}

	 public void SpawnTower()
    {
		gameObject.GetComponent<SpriteRenderer>().sprite = tower;
		gameObject.GetComponent<SpriteRenderer>().sortingOrder =10;
		Destroy(buttonInst);
	}

	void ButtonClick()
    {
		SpawnTower();
    }
}
