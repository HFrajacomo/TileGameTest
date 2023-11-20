using System;
using UnityEngine;

using Random = System.Random;


public class MapGenerator : MonoBehaviour {
	public AnimationCurve baseSpline;
	public AnimationCurve intensitySpline;

	private byte[] baseMap = new byte[257];
	private byte[] intensityMap = new byte[257];
	private byte[] neighborMap = new byte[257];

	private Random rng;
	private int seed = 0;

	private static float BASE_X_STEP = 0.063f;
	private static float BASE_Y_STEP = 0.051f;

	private static float INT_X_STEP = 0.103f;
	private static float INT_Y_STEP = 0.097f;

	private static float NEI_X_STEP = 0.051f;
	private static float NEI_Y_STEP = 0.037f;

    void Start(){
    	this.rng = new Random(this.seed);

    	BASE_X_STEP += (float)(this.rng.NextDouble()/10);
    	BASE_Y_STEP += (float)(this.rng.NextDouble()/10);

    	INT_X_STEP += (float)(this.rng.NextDouble()/10);
    	INT_Y_STEP += (float)(this.rng.NextDouble()/10);

    	NEI_X_STEP += (float)(this.rng.NextDouble()/7);
    	NEI_Y_STEP += (float)(this.rng.NextDouble()/7);

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
    }

    /*
    public void CreateQuad(int x, int y){
    	float baseResult = baseSpline.Evaluate(NoiseMaker.Noise2D(x*BASE_X_STEP, y*BASE_Y_STEP, this.baseMap));
    	float intensityResult = intensitySpline.Evaluate(NoiseMaker.Noise2D(x*INT_X_STEP, y*BASE_Y_STEP, this.intensityMap));
    	float neighborMap = NoiseMaker.Noise2D(x*NEI_X_STEP, y*NEI_Y_STEP, this.neighborMap);
    }
    */
}