using System.Collections.Generic;

public class PlayerStorage {
	private List<ResourcesStorage> storages;
	private Dictionary<Resources, byte> resources;
	private byte playerId;

	public PlayerStorage(byte id){
		this.playerId = id;

		this.storages = new List<ResourcesStorage>();
		this.resources = new Dictionary<Resources, byte>(){{Resources.FOOD, 0}, {Resources.WOOD, 0}, {Resources.CLAY, 0}, {Resources.STONE, 0}, {Resources.METAL, 0}};
	}

	public void RegisterStorage(ResourcesStorage store){
		this.storages.Add(store);
		store.LinkStorage(this);
	}

	public void UnregisterStorage(ResourcesStorage store){
		int i = 0;
		bool found = false;

		for(; i < this.storages.Count; i++){
			if(store == this.storages[i]){
				found = true;
				break;
			}
		}

		if(found)
			this.storages.RemoveAt(i);
	}

	public void RefreshResources(){
		foreach(ResourcesStorage rs in this.storages){

		}
	}
}