using UnityEngine;

public class GameMap : MonoBehaviour{
	public int size = 4;

	[Header("Prefabs")]
	public GameObject quadPreset;

	[Header("Quad Materials")]
	public Material[] materials;

	private Quad[] map;

	public void CreateMap(MapGenerator gen){
		this.map = new Quad[size*size];

		for(int i=0; i < size; i++){
			for(int j=0; j < size; j++){
				this.map[i*size+j] = gen.CreateQuad(i, j);
			}
		}
	}

	public void BuildAllQuads(){
		for(int i=0; i < this.size; i++){
			for(int j=0; j < this.size; j++){
				BuildQuad(i, j);
			}
		}
	}

	public void SetSize(int size){this.size = size;}

	public void SetQuad(int x, int y, Quad q){
		this.map[x*this.size + y] = q;
	}

	public Quad GetQuad(int x, int y){
		return this.map[x*this.size+y];
	}

	public int GetSize(){return this.size;}

	public void BuildQuad(int x, int y){
		this.map[x*this.size+y].Build(CalculateQuadPosition(x*this.size+y), this.quadPreset, this.materials);
	}

	private Vector3 CalculateQuadPosition(int index){
		return new Vector3(index/this.size, 0, index%this.size);
	}
}