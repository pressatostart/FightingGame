using SakugaEngine.Resources;
using UnityEngine;

public partial class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    private GameObject LoadingScreenSimple;
    private GameObject LoadingScreenMatchStart;

    private string SimpleDir = "res://Scenes/Loading Screens/LoadingScreen_Simple.tscn";
    private string MatchDir = "res://Scenes/Loading Screens/LoadingScreen_Match.tscn";

    private string currentLoadingScene;

    /*public void Awake()
    {
        Instance = this;

        LoadingScreenSimple = GD.Load<PackedScene>(SimpleDir).Instantiate() as Control;
        LoadingScreenMatchStart = GD.Load<PackedScene>(MatchDir).Instantiate() as Control;
        AddChild(LoadingScreenSimple);
        AddChild(LoadingScreenMatchStart);
        LoadingScreenSimple.Visible = false;
        LoadingScreenMatchStart.Visible = false;
    }

    public void Update()
    {
        if (!LoadingScreenSimple.Visible && !LoadingScreenMatchStart.Visible) return;
        base._Process(delta);

        //Array progress = new Array();

        var status = ResourceLoader.LoadThreadedGetStatus(currentLoadingScene);
        //var progressValue = (float)progress[0] * 100;

        switch (status)
        {
            case ResourceLoader.ThreadLoadStatus.InProgress:
                break;
            case ResourceLoader.ThreadLoadStatus.Failed:
                break;
            case ResourceLoader.ThreadLoadStatus.InvalidResource:
                break;
            case ResourceLoader.ThreadLoadStatus.Loaded:
                SceneLoaded();
                break;
        }
    }

    public void LoadScene(string nextScene)
    {
        currentLoadingScene = nextScene;
        ResourceLoader.LoadThreadedRequest(currentLoadingScene);
        LoadingScreenSimple.SetDeferred(Control.PropertyName.Visible, true);
    }

    public void LoadMatch(string nextScene, FighterProfile player1Profile, FighterProfile player2Profile)
    {
        currentLoadingScene = nextScene;
        ResourceLoader.LoadThreadedRequest(currentLoadingScene);
        CallDeferred("MatchSceneSetup", player1Profile, player2Profile);
        LoadingScreenMatchStart.SetDeferred(Control.PropertyName.Visible, true);
    }

    public void SceneLoaded()
    {
        //ResourceLoader.LoadThreadedGet(currentLoadingScene).NativeInstance;
        GetTree().ChangeSceneToFile(currentLoadingScene);
        GD.Print($"Scene {currentLoadingScene} loaded successfully!");
        LoadingScreenSimple.Visible = false;
        LoadingScreenMatchStart.Visible = false;
    }

    private void MatchSceneSetup(FighterProfile player1Profile, FighterProfile player2Profile)
    {
        LoadingScreenMatchStart.GetNode<TextureRect>("P1SelectedRender").Texture = player1Profile.Render;
        LoadingScreenMatchStart.GetNode<TextureRect>("P2SelectedRender").Texture = player2Profile.Render;
        LoadingScreenMatchStart.GetNode<Label>("P1SelectedName").Text = player1Profile.FighterName;
        LoadingScreenMatchStart.GetNode<Label>("P2SelectedName").Text = player2Profile.FighterName;
    }*/
}
