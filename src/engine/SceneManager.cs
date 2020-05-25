using System;
using Godot;

/// <summary>
///   Singleton managing changing game scenes
/// </summary>
public class SceneManager : Node
{
    private static SceneManager instance;

    private Node internalRootNode;

    private SceneManager()
    {
        instance = this;
    }

    public static SceneManager Instance => instance;

    public override void _Ready()
    {
        internalRootNode = GetTree().Root;
    }

    /// <summary>
    ///   Switches to a game state
    /// </summary>
    /// <param name="state">The game state to switch to, this automatically looks up the right scene</param>
    public void SwitchToScene(MainGameState state)
    {
        SwitchToScene(LoadScene(state).Instance());
    }

    public void SwitchToScene(string scenePath)
    {
        SwitchToScene(LoadScene(scenePath).Instance());
    }

    public Node SwitchToScene(Node newSceneRoot, bool keepOldRoot = false)
    {
        var oldRoot = GetTree().CurrentScene;
        GetTree().CurrentScene = null;

        if (oldRoot != null)
        {
            internalRootNode.RemoveChild(oldRoot);
        }

        internalRootNode.AddChild(newSceneRoot);
        GetTree().CurrentScene = newSceneRoot;

        if (!keepOldRoot)
        {
            oldRoot?.QueueFree();
            return null;
        }
        else
        {
            return oldRoot;
        }
    }

    /// <summary>
    ///   Switches a scene to the main menu
    /// </summary>
    public void ReturnToMenu()
    {
        var scene = LoadScene("res://src/gui_common/MainMenu.tscn");

        var mainMenu = (MainMenu)scene.Instance();

        mainMenu.IsReturningToMenu = true;

        SwitchToScene(mainMenu);
    }

    public PackedScene LoadScene(MainGameState state)
    {
        switch (state)
        {
            case MainGameState.MicrobeStage:
                return LoadScene("res://src/microbe_stage/MicrobeStage.tscn");
            case MainGameState.MicrobeEditor:
                return LoadScene("res://src/microbe_stage/editor/MicrobeEditor.tscn");
            default:
                throw new ArgumentException("unknown scene path for given game state");
        }
    }

    public PackedScene LoadScene(string scenePath)
    {
        return GD.Load<PackedScene>(scenePath);
    }
}