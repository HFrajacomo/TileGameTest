using UnityEngine;

public class Quad{
	private int type;
	private bool built = false;
	private GameObject obj;

	public Quad(int type){
		this.type = type;
	}

	public int GetT(){return this.type;}

	public void Build(Vector3 pos, GameObject preset, Material[] mat){
		if(!this.built){
			this.obj = GameObject.Instantiate(preset, pos, Quaternion.Euler(90, 0, 0));
			this.obj.GetComponent<MeshRenderer>().material = mat[this.type];
			this.built = true;
		}
			
	}
}