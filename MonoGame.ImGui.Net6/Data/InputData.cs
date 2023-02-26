using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Vector2 = System.Numerics.Vector2;

namespace MonoGame.ImGui.Data;

/// <summary>
///     Contains the GUIRenderer's input data elements.
/// </summary>
public class InputData
{
    private readonly List<int> _keyMap;
    private int _scrollWheel;

    public InputData()
    {
        _scrollWheel = 0;
        _keyMap = new List<int>();
    }

    public void Update(GraphicsDevice device)
    {
        var io = ImGuiNET.ImGui.GetIO();
        var mouse = Mouse.GetState();
        var keyboard = Keyboard.GetState();

        foreach (var t in _keyMap)
            io.KeysDown[t] = keyboard.IsKeyDown((Keys)t);

        io.KeyShift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);
        io.KeyCtrl = keyboard.IsKeyDown(Keys.LeftControl) || keyboard.IsKeyDown(Keys.RightControl);
        io.KeyAlt = keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt);
        io.KeySuper = keyboard.IsKeyDown(Keys.LeftWindows) || keyboard.IsKeyDown(Keys.RightWindows);

        io.DisplaySize = new Vector2(device.PresentationParameters.BackBufferWidth,
            device.PresentationParameters.BackBufferHeight);
        io.DisplayFramebufferScale = new Vector2(1f, 1f);

        io.MousePos = new Vector2(mouse.X, mouse.Y);

        io.MouseDown[0] = mouse.LeftButton == ButtonState.Pressed;
        io.MouseDown[1] = mouse.RightButton == ButtonState.Pressed;
        io.MouseDown[2] = mouse.MiddleButton == ButtonState.Pressed;
        io.MouseDown[3] = mouse.XButton1 == ButtonState.Pressed;
        io.MouseDown[4] = mouse.XButton2 == ButtonState.Pressed;

        var scrollDelta = mouse.ScrollWheelValue - _scrollWheel;
        io.MouseWheel = scrollDelta > 0 ? 1 : scrollDelta < 0 ? -1 : 0;
        _scrollWheel = mouse.ScrollWheelValue;
    }

    public InputData Initialize(Game game)
    {
        var io = ImGuiNET.ImGui.GetIO();

        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Tab] = (int)Keys.Tab);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Keys.Left);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Keys.Right);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Keys.Up);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Keys.Down);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.PageUp] = (int)Keys.PageUp);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.PageDown] = (int)Keys.PageDown);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Home] = (int)Keys.Home);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.End] = (int)Keys.End);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Delete] = (int)Keys.Delete);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Backspace] = (int)Keys.Back);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Enter] = (int)Keys.Enter);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Escape] = (int)Keys.Escape);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.A] = (int)Keys.A);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.C] = (int)Keys.C);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.V] = (int)Keys.V);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.X] = (int)Keys.X);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Y] = (int)Keys.Y);
        _keyMap.Add(io.KeyMap[(int)ImGuiKey.Z] = (int)Keys.Z);

        game.Window.TextInput += (_, args) =>
        {
            if (args.Character != '\t')
                io.AddInputCharacter(args.Character);
        };

        io.Fonts.AddFontDefault();
        return this;
    }
}