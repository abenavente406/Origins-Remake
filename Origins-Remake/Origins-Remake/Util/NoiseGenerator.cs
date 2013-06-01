using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PerlinNoise;
using PerlinNoise.Filters;
using PerlinNoise.Transformers;
using Microsoft.Xna.Framework;
using System.IO;

namespace Origins_Remake.Util
{
    public static class NoiseGenerator
    {
        static MainGame gameRef;

        public static void Initialize(Game game)
        {
            gameRef = (MainGame)game;
        }

        /// <summary>
        /// Generates a perlin noise cloud
        /// </summary>
        /// <param name="size">Must be a square map</param>
        /// <returns></returns>
        public static Texture2D GenerateTextureData(int size)
        {
            PerlinNoiseGenerator generator = new PerlinNoiseGenerator();
            generator.OctaveCount = 7;
            generator.Persistence = .55f;
            generator.Interpolation = InterpolationAlgorithms.CosineInterpolation;

            NoiseField<float> perlinNoise = generator.GeneratePerlinNoise(size, size);

            CustomGradientColorFilter filter = new CustomGradientColorFilter();
            Texture2DTransformer tranformer = new Texture2DTransformer(gameRef.GraphicsDevice);

            filter.AddColorPoint(0.0f, 1f, Color.White);
            filter.AddColorPoint(0.7f, 1, Color.Black);

            var noiseTexture = tranformer.Transform(filter.Filter(perlinNoise));

            FileStream stream = File.Create(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.png");
            noiseTexture.SaveAsPng(stream, size, size);
            stream.Close();

            return noiseTexture;
        }
    }
}
