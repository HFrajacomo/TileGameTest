using UnityEngine;
using Unity.Mathematics;

public class BiomeTable {
    private Biome[,] lowBaseNeighborTable;
    private Biome[,] midBaseNeighborTable;
    private Biome[,] highBaseNeighborTable;
    private BiomeType[] intensityArray;
    private BiomeType[] continentalArray;


    public BiomeTable(){
    	SetupIntensityArray();
    	SetupContinentalArray();
    	SetupBaseNeighborTable();
    }

    private void SetupIntensityArray(){
        this.intensityArray = new BiomeType[]{BiomeType.LOW, BiomeType.LOW, BiomeType.MID, BiomeType.MID, BiomeType.MID, BiomeType.MID, BiomeType.MID, BiomeType.HIGH, BiomeType.HIGH, BiomeType.HIGH};
	}

	private void SetupContinentalArray(){
		this.continentalArray = new BiomeType[]{BiomeType.NOTHING, BiomeType.NOTHING, BiomeType.NOTHING, BiomeType.SOMETHING, BiomeType.SOMETHING, BiomeType.SOMETHING, BiomeType.SOMETHING, BiomeType.SOMETHING, BiomeType.SOMETHING, BiomeType.SOMETHING};
	}

	private void SetupBaseNeighborTable(){
        this.lowBaseNeighborTable = new Biome[,]{
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS},
            {Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.PLAINS, Biome.PLAINS},
            {Biome.PLAINS, Biome.PLAINS, Biome.BADLANDS, Biome.BADLANDS, Biome.BADLANDS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS, Biome.PLAINS},
        };

        this.midBaseNeighborTable = new Biome[,]{
            {Biome.JUNGLE, Biome.JUNGLE, Biome.JUNGLE, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.JUNGLE, Biome.JUNGLE, Biome.JUNGLE},
            {Biome.CLAYLANDS, Biome.JUNGLE, Biome.JUNGLE, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.VOLCANIC, Biome.VOLCANIC, Biome.VOLCANIC, Biome.VOLCANIC, Biome.VOLCANIC},
            {Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.VOLCANIC, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE},
            {Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.JUNGLE, Biome.JUNGLE, Biome.PRAERIE, Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.CLAYLANDS},
            {Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.VOLCANIC, Biome.JUNGLE, Biome.JUNGLE, Biome.JUNGLE, Biome.PRAERIE, Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.CLAYLANDS},
            {Biome.VOLCANIC, Biome.VOLCANIC, Biome.VOLCANIC, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE,},
            {Biome.JUNGLE, Biome.JUNGLE, Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.PRAERIE, Biome.VOLCANIC, Biome.PRAERIE, Biome.JUNGLE, Biome.JUNGLE, Biome.JUNGLE},
            {Biome.JUNGLE, Biome.JUNGLE, Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.PRAERIE, Biome.VOLCANIC, Biome.VOLCANIC, Biome.PRAERIE, Biome.PRAERIE, Biome.MOUNTAIN},
            {Biome.VOLCANIC, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.VOLCANIC, Biome.PRAERIE, Biome.PRAERIE, Biome.VOLCANIC, Biome.VOLCANIC, Biome.VOLCANIC, Biome.VOLCANIC},
            {Biome.VOLCANIC, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.CLAYLANDS, Biome.MOUNTAIN, Biome.MOUNTAIN, Biome.MOUNTAIN},
        };

        this.highBaseNeighborTable = new Biome[,]{
            {Biome.FOREST, Biome.FOREST, Biome.FOREST, Biome.CANYON, Biome.CANYON, Biome.CANYON, Biome.CANYON, Biome.FOREST, Biome.FOREST, Biome.FOREST},
            {Biome.WETLANDS, Biome.FOREST, Biome.FOREST, Biome.CANYON, Biome.CANYON, Biome.DESERT, Biome.DESERT, Biome.DESERT, Biome.DESERT, Biome.DESERT},
            {Biome.WETLANDS, Biome.WETLANDS, Biome.DESERT, Biome.CANYON, Biome.CANYON, Biome.CANYON, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE},
            {Biome.WETLANDS, Biome.WETLANDS, Biome.CANYON, Biome.CANYON, Biome.FOREST, Biome.FOREST, Biome.PRAERIE, Biome.WETLANDS, Biome.WETLANDS, Biome.WETLANDS},
            {Biome.WETLANDS, Biome.WETLANDS, Biome.DESERT, Biome.FOREST, Biome.FOREST, Biome.FOREST, Biome.PRAERIE, Biome.WETLANDS, Biome.WETLANDS, Biome.WETLANDS},
            {Biome.DESERT, Biome.DESERT, Biome.DESERT, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE, Biome.PRAERIE,},
            {Biome.FOREST, Biome.FOREST, Biome.WETLANDS, Biome.WETLANDS, Biome.PRAERIE, Biome.DESERT, Biome.PRAERIE, Biome.FOREST, Biome.FOREST, Biome.FOREST},
            {Biome.FOREST, Biome.FOREST, Biome.WETLANDS, Biome.WETLANDS, Biome.PRAERIE, Biome.DESERT, Biome.DESERT, Biome.PRAERIE, Biome.PRAERIE, Biome.CANYON},
            {Biome.DESERT, Biome.CANYON, Biome.CANYON, Biome.DESERT, Biome.PRAERIE, Biome.PRAERIE, Biome.DESERT, Biome.DESERT, Biome.DESERT, Biome.DESERT},
            {Biome.DESERT, Biome.CANYON, Biome.CANYON, Biome.CANYON, Biome.WETLANDS, Biome.WETLANDS, Biome.WETLANDS, Biome.CANYON, Biome.CANYON, Biome.CANYON},
        };
	}

    public Biome GetBiome(float4 data){
    	BiomeType continentalism = this.continentalArray[GetInteger(data.w)];
        BiomeType type = this.intensityArray[GetInteger(data.y)];

        if(continentalism == BiomeType.NOTHING)
        	return Biome.NOTHING;

        if(type == BiomeType.LOW)
            return this.lowBaseNeighborTable[GetInteger(data.x), GetInteger(data.z)];
        else if(type == BiomeType.MID)
            return this.midBaseNeighborTable[GetInteger(data.x), GetInteger(data.z)];
        else if(type == BiomeType.HIGH)
            return this.highBaseNeighborTable[GetInteger(data.x), GetInteger(data.z)];
        else
            return Biome.NOTHING;
    }

    private int GetInteger(float a){
        if(a >= 1)
            return 9;
        return (int)(a*10);
    }
}