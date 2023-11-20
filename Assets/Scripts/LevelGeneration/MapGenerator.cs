using System;
using UnityEngine;
using Unity.Mathematics;

using Random = System.Random;


public class MapGenerator : MonoBehaviour {
	public AnimationCurve baseSpline;
	public AnimationCurve intensitySpline;

    private BiomeTable biomeTable = new BiomeTable();

	private byte[] baseMap = new byte[257];
	private byte[] intensityMap = new byte[257];
	private byte[] neighborMap = new byte[257];
    private byte[] continentalMap = new byte[257];

	private Random rng;
	private int seed = 0;
    private int mapSize = 10;

	private static float BASE_X_STEP = 0.163f;
	private static float BASE_Y_STEP = 0.151f;

	private static float INT_X_STEP = 0.143f;
	private static float INT_Y_STEP = 0.127f;

	private static float NEI_X_STEP = 0.111f;
	private static float NEI_Y_STEP = 0.097f;

    private static float CON_X_STEP;
    private static float CON_Y_STEP;

    public void SetNoiseMaps(){
    	this.rng = new Random(this.seed);

    	BASE_X_STEP += (float)(this.rng.NextDouble()/10);
    	BASE_Y_STEP += (float)(this.rng.NextDouble()/10);

    	INT_X_STEP += (float)(this.rng.NextDouble()/10);
    	INT_Y_STEP += (float)(this.rng.NextDouble()/10);

    	NEI_X_STEP += (float)(this.rng.NextDouble()/7);
    	NEI_Y_STEP += (float)(this.rng.NextDouble()/7);

        CON_X_STEP = 1/(this.mapSize*2)+(float)(this.rng.NextDouble()/12);
        CON_Y_STEP = 1/(this.mapSize*2)+(float)(this.rng.NextDouble()/12);

        // Base Noise
        for(int i=0; i < 256; i++){
            this.baseMap[i] = (byte)this.rng.Next(0, 256);
        }
        this.baseMap[256] = (byte)this.baseMap[0];

        // Intensity Noise
        this.rng = new Random(((this.seed << 2) + 3));
        for(int i=0; i < 256; i++){
            this.intensityMap[i] = (byte)this.rng.Next(0, 256);
        }
        this.intensityMap[256] = (byte)this.intensityMap[0];

        // Neighbor Noise
        this.rng = new Random(~seed ^ 0x7AAAAAAA);
        for(int i=0; i < 256; i++){
            this.neighborMap[i] = (byte)this.rng.Next(0, 256);
        }
        this.neighborMap[256] = (byte)this.neighborMap[0];

        // Continental Noise
        this.rng = new Random(~seed);
        for(int i=0; i < 256; i++){
            this.continentalMap[i] = (byte)this.rng.Next(0, 256);
        }
        this.continentalMap[256] = (byte)this.continentalMap[0];
    }

    public void SetSeed(int seed){this.seed = seed;}
    public void SetSize(int size){this.mapSize = size;}

    public Quad CreateQuad(int x, int y){
    	float baseResult = baseSpline.Evaluate(NoiseMaker.Noise2D(x*BASE_X_STEP, y*BASE_Y_STEP, this.baseMap));
    	float intensityResult = intensitySpline.Evaluate(NoiseMaker.Noise2D(x*INT_X_STEP, y*INT_Y_STEP, this.intensityMap));
    	float neighborResult = NoiseMaker.Noise2D(x*NEI_X_STEP, y*NEI_Y_STEP, this.neighborMap);
        float continentalResult = NoiseMaker.Noise2D(x*CON_X_STEP, y*NEI_Y_STEP, this.continentalMap);

        float4 noises = new float4(baseResult, intensityResult, neighborResult, continentalResult);

        Biome generatedBiome = this.biomeTable.GetBiome(noises);

        return Quad.Create(generatedBiome);
    }

    public void GenerateBeaches(GameMap map){
        int mapSize = map.GetSize();
        Quad neiQuad;

        for(int i=0; i < mapSize; i++){
            for(int j=0; j < mapSize; j++){
                if(map.GetQuad(i, j).GetBiome() != Biome.NOTHING)
                    continue;

                if(i > 0){
                    neiQuad = map.GetQuad(i-1, j);
                    if(IsContinent(neiQuad)){
                        map.SetQuad(i, j, Quad.Create(Biome.BEACH));
                        continue;
                    }
                }
                if(i < mapSize-1){
                    neiQuad = map.GetQuad(i+1, j);

                    if(IsContinent(neiQuad)){
                        map.SetQuad(i, j, Quad.Create(Biome.BEACH));
                        continue;
                    }
                }
                if(j > 0){
                    neiQuad = map.GetQuad(i, j-1);
                    if(IsContinent(neiQuad)){
                        map.SetQuad(i, j, Quad.Create(Biome.BEACH));
                        continue;
                    }
                }
                if(j < mapSize-1){
                    neiQuad = map.GetQuad(i, j+1);

                    if(IsContinent(neiQuad)){
                        map.SetQuad(i, j, Quad.Create(Biome.BEACH));
                        continue;
                    }
                }
            }
        }
    }

    private bool IsContinent(Quad q){
        return q.GetBiome() != Biome.NOTHING && q.GetBiome() != Biome.BEACH;
    }

}