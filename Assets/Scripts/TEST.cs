using UnityEngine;

public class TEST : MonoBehaviour{
	public GameObject prefab;
	public GameMap map;

	void Start(){

		for(int i=0; i<this.map.GetSize()*this.map.GetSize(); i++){
			this.map.SetQuad(i, new Quad(1));
			this.map.BuildQuad(i);
		}

		
	}


}