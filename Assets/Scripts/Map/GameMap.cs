using UnityEngine;

public class GameMap : MonoBehaviour{
	public int size = 1;

	[Header("Prefabs")]
	public GameObject quadPreset;

	[Header("Quad Materials")]
	public Material[] materials;

	private Quad[] map;

	void Start(){
		this.map = new Quad[size*size];
	}

	public void SetQuad(int index, Quad q){
		this.map[index] = q;
	}

	public int GetSize(){return this.size;}

	public void BuildQuad(int index){
		this.map[index].Build(CalculateQuadPosition(index), this.quadPreset, this.materials);
	}

	private Vector3 CalculateQuadPosition(int index){
		return new Vector3(index/this.size, 0, index%this.size);
	}
}