using UnityEngine;

public static class NoiseMaker {

    public static float Noise1D(float x, byte[] noiseMap)
    {
        int X = Mathf.FloorToInt(x) & 0xff;
        x -= Mathf.Floor(x);
        float u = Fade(x);

        return Normalize(Lerp(u, Grad(noiseMap[X], x), Grad(noiseMap[X+1], x-1)) * 2);
    }


    public static float Noise2D(float x, float y, byte[] noiseMap)
    {
        int X = Mathf.FloorToInt(x) & 0xff;
        int Y = Mathf.FloorToInt(y) & 0xff;
        x -= Mathf.Floor(x);
        y -= Mathf.Floor(y);

        float u = Fade(x);
        float v = Fade(y);

        int A = (noiseMap[X  ] + Y) & 0xff;
        int B = (noiseMap[X+1] + Y) & 0xff;
        return Normalize(Lerp(v, Lerp(u, Grad(noiseMap[A  ], x, y  ), Grad(noiseMap[B  ], x-1, y  )),
                       Lerp(u, Grad(noiseMap[A+1], x, y-1), Grad(noiseMap[B+1], x-1, y-1))));
        
    }

    private static float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    private static float Lerp(float t, float a, float b)
    {
        return a + t * (b - a);
    }

    private static float Grad(int hash, float x)
    {
        return (hash & 1) == 0 ? x : -x;
    }

    private static float Grad(int hash, float x, float y)
    {
        return ((hash & 1) == 0 ? x : -x) + ((hash & 2) == 0 ? y : -y);
    }

    private static float Grad(int hash, float x, float y, float z)
    {
        int h = hash & 15;
        float u = h < 8 ? x : y;
        float v = h < 4 ? y : (h == 12 || h == 14 ? x : z);
        return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
    }

    private static float Normalize(float x){
        return (1 + x)/2;
    }
}