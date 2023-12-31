using UnityEngine;

public abstract class Quad{
	protected Biome type;
	protected bool built = false;
	protected GameObject obj;
	protected byte ownedBy; //playerId
	protected ushort rotation = 0; 

	public static Quad Create(Biome b){
		switch(b){
			case Biome.PLAINS:
				return new Plains();
			case Biome.BADLANDS:
				return new Badlands();
			case Biome.JUNGLE:
				return new Jungle();
			case Biome.CLAYLANDS:
				return new Claylands();
			case Biome.MOUNTAIN:
				return new Mountain();
			case Biome.VOLCANIC:
				return new Volcanic();
			case Biome.PRAERIE:
				return new Praerie();
			case Biome.BEACH:
				return new Beach();
			case Biome.DESERT:
				return new Desert();
			case Biome.CANYON:
				return new Canyon();
			case Biome.FOREST:
				return new Forest();
			case Biome.WETLANDS:
				return new Wetlands();
			case Biome.NOTHING:
				return new Nothing();
			default:
				return new Nothing();
		}
	} 

	public int GetT(){return (int)this.type;}
	public Biome GetBiome(){return this.type;}
	public void SetRotation(ushort val){this.rotation = val;}

	public void Build(Vector3 pos, GameObject preset, GameObject parent, Material[] mat){
		if(!this.built){
			this.obj = GameObject.Instantiate(preset, pos, Quaternion.Euler(90, this.rotation, 0));
			this.obj.name = this.GetBiome().ToString() + " (" + (int)pos.x + ", " + (int)pos.z + ")";
			this.obj.GetComponent<MeshRenderer>().material = Material.Instantiate(mat[(int)this.type]);
			this.obj.transform.SetParent(parent.transform);

			this.built = true;
		}	
	}

	public void SetOwner(byte b){this.ownedBy = b;}
}