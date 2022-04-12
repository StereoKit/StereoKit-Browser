using StereoKit;
using StereoKit.Framework;
using System;

// Initialize StereoKit
SKSettings settings = new SKSettings
{
	appName = "StereoKit Browser",
	assetsFolder = "Assets",
	displayPreference = DisplayMode.MixedReality,
};
if (!SK.Initialize(settings))
	Environment.Exit(1);

Browser browser    = new Browser("http://stereokit.net");
Pose    windowPose = new Pose(0, 0, -0.5f, Quat.LookDir(0, 0, 1));

Material floorMaterial = new Material(Shader.FromFile("floor.hlsl"));
floorMaterial.Transparency = Transparency.Blend;

// Core application loop
SK.Run(() => {
	if (SK.System.displayType == Display.Opaque)
		Default.MeshCube.Draw(floorMaterial, World.HasBounds
			? World.BoundsPose.ToMatrix(new Vec3(30, 0.1f, 30))
			: Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30)));

	UI.WindowBegin("Browser", ref windowPose, V.XY(0.6f,0));
	UI.PushEnabled(browser.HasBack);
	if (UI.Button("Back")) browser.Back();
	UI.PopEnabled();

	UI.SameLine();
	UI.PushEnabled(browser.HasForward);
	if (UI.Button("Forward")) browser.Forward();
	UI.PopEnabled();

	UI.SameLine();
	UI.Label(browser.Url, V.XY(UI.LayoutRemaining.x, 0));

	UI.HSeparator();

	browser.StepAsUI();
	UI.WindowEnd();

	if (Input.Key(Key.N1).IsJustActive()) browser.Url = "https://stereokit.net";
	if (Input.Key(Key.N2).IsJustActive()) browser.Url = "https://www.google.com";
	if (Input.Key(Key.N3).IsJustActive()) browser.Url = "https://youtu.be/24wY236Kl-4";
});
