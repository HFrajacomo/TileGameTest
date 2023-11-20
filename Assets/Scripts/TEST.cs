using System;
using UnityEngine;

using Random = System.Random;

public class TEST : MonoBehaviour{
	public GameMap map;
	public MapGenerator generator;
	public int mapSize = 10;
	private Random seeder;

	void Awake(){
		int seed = (int)DateTime.Now.Ticks;
		this.seeder = new Random(seed);

		this.generator.SetSize(this.mapSize);
		this.generator.SetSeed(this.seeder.Next());
		this.generator.SetNoiseMaps();
		this.map.SetSize(this.mapSize);
		this.map.CreateMap(this.generator);
		this.generator.GenerateBeaches(this.map);

		this.map.BuildAllQuads();
	}
}