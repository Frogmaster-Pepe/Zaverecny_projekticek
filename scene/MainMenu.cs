using Godot;
using System;

public partial class MainMenu : Control
{
    [Export]
    public NodePath StartButtonPath { get; set; } = "StartButton";

    [Export]
    public NodePath QuitButtonPath { get; set; } = "QuitButton";

    [Export]
    public string GameScenePath { get; set; } = "res://scene/lv1.tscn";

    //lowkey černá magie tohle, už nemam tušení jak a proč to funguje a bojim se na to šáhnout
    public override void _Ready()
    {
        var startBtn = GetNodeOrNull<Button>(StartButtonPath);
        if (startBtn != null)
        {
             
            startBtn.Pressed -= _on_start_pressed;
            startBtn.Pressed += _on_start_pressed;
        }
        else
        {
            GD.PrintErr($"Start button not found at '{StartButtonPath}'");
        }

        var quitBtn = GetNodeOrNull<Button>(QuitButtonPath);
        if (quitBtn != null)
        {
            quitBtn.Pressed -= OnQuitPressed;
            quitBtn.Pressed += OnQuitPressed;
        }
        else
        {
            GD.PrintErr($"Quit button not found at '{QuitButtonPath}'");
        }
    }
    private void _on_start_pressed()
    {
        if (string.IsNullOrEmpty(GameScenePath))
        {
            GD.PrintErr("GameScenePath není nastavený.");
            return;
        }
        var err = GetTree().ChangeSceneToFile(GameScenePath);
        if (err != Error.Ok)
            GD.PrintErr($"Nelze načíst scénu '{GameScenePath}': {err}");
    }
    private void OnQuitPressed()
    {
        GetTree().Quit();
    }
    public override void _Process(double delta)
    {
    }
}