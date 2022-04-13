using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BossChess;

public class TextureManager
{
    private Dictionary<string, Texture2D> textureDict = new Dictionary<string, Texture2D>();
    private SpriteBatch spriteBatch;
    private ContentManager contentManager;

    public TextureManager(SpriteBatch sb, ContentManager cm)
    {
        spriteBatch = sb;
        contentManager = cm;
    }

    public void AddTexture(string name, string path)
    {
        Texture2D newTexture = contentManager.Load<Texture2D>(path);
        textureDict.Add(name,newTexture);
    }

    public void AddTexture(string name, Texture2D tex)
    {
        textureDict.Add(name,tex);
    }

    public Texture2D GetTexture(string name)
    {
        if (!textureDict.ContainsKey(name))
        {
            throw new System.ArgumentException($"No texture with name '{name}' exists.");
        }

        return textureDict[name];
    }
}