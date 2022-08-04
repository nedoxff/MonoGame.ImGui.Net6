using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.ImGui.Data;

/// <summary>
///     Contains the GUIRenderer's texture data element.
/// </summary>
public class TextureData
{
    public IntPtr? FontTextureId;
    public readonly Dictionary<IntPtr, Texture2D> Loaded;
    public int TextureId;

    public TextureData()
    {
        Loaded = new Dictionary<IntPtr, Texture2D>();
    }

    public int GetTextureId()
    {
        return TextureId++;
    }
}