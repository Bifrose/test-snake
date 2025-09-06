using Raylib_cs;
using System.Collections.Generic;

namespace SnakeGame
{
    public static class SpriteManager
    {
        private static readonly Dictionary<string, Texture2D> textures = new();

       
        public static void LoadTexture(string key, string path)
        {
            if (!textures.ContainsKey(key))
                textures[key] = Raylib.LoadTexture(path);
        }

      
        public static Texture2D GetTexture(string key)
        {
            return textures.TryGetValue(key, out var tex) ? tex : default;
        }


        public static void UnloadAll()
        {
            foreach (var tex in textures.Values)
                Raylib.UnloadTexture(tex);
            textures.Clear();
        }
    }
}

