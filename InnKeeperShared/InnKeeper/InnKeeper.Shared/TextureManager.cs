
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace InnKeeper.Shared
{
    /// <summary>
    /// TextureManager caches textures loaded by the content manager
    /// </summary>
    public class TextureManager
    {

        Dictionary<string, Texture2D> textures;

        /// <summary>
        /// Constructor - initializes textures dictionary
        /// </summary>
        public TextureManager()
        {
            textures = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Add a texture to the dictionary if its key does not exist
        /// </summary>
        /// <param name="name">Key</param>
        /// <param name="tex">Value</param>
        public void AddTexture(string name, Texture2D tex)
        {
            // Add texture to dictionary
            if(!textures.ContainsKey(name))
            {
                textures.Add(name, tex);
            }

            
        }
        /// <summary>
        /// Retrieves a texture from the dictionary
        /// </summary>
        /// <param name="name">Texture key</param>
        /// <returns></returns>
        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }
        /// <summary>
        /// Clear references
        /// </summary>
        public void Clear()
        {
            textures.Clear();
        }

    }
}
