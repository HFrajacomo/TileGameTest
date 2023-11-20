using System.Collections.Generic;

public class ResourcesStorage {
	private Dictionary<Resources, ushort> store;
	private PlayerStorage linkedPlayer;

	public ResourcesStorage(PlayerStorage master){
		this.store = new Dictionary<Resources, ushort>(){{Resources.FOOD, 0}, {Resources.WOOD, 0}, {Resources.CLAY, 0}, {Resources.STONE, 0}, {Resources.METAL, 0}};
		this.linkedPlayer = master;
	}

	public ushort GetResource(Resources res){return this.store[res];}

	public void LinkStorage(PlayerStorage ps){this.linkedPlayer = ps;}
}