# Knowit Wild West

This demo was made for HoloLense using Unity and [HoloToolkit-Unity](https://github.com/Microsoft/HoloToolkit-Unity "HoloToolkit-Unity"). It is a demo using spatial mapping to place objects in the world, spatial audio and hand gestures and voice commands to interact with the objects.

## Getting Started

1. Clone the repo: `git clone git@github.com:stengard/Knowit-Wild-West.git`  

### Prerequisites

1. HoloLense or [HoloLense Emulator](https://developer.microsoft.com/sv-se/windows/holographic/install_the_tools "HoloLense Emulator")
2. [Unity HoloLense Technical Preview](https://unity3d.com/partners/microsoft/hololens "Unity HoloLense Technical Preview")
3. Visual Studio 2015

### Installing

Open the project in Unity. You can use unitys Game mode or export to Visual Studio and build to the HoloLense or HoloLense Emulator.

#### Export the project from Unity to Visual Studio
1. In Unity select File > Build Settings.
2. Select Windows Store in the Platform list and click Switch Platform.
3. Set SDK to Universal 10 and Build Type to D3D.
4. Check Unity C# Projects.
5. Click Add Open Scenes to add the scene.
6. Click Player Settings....
7. In the Inspector Panel select the Windows Store logo. Then select Publishing Settings.
8. In the Capabilities section, select the Microphone and SpatialPerception capabilities.
9. Back in the Build Settings window, click Build.
10. Create a New Folder named "App".
11. Single click the App Folder.
12. Press Select Folder.
13. When Unity is done, a File Explorer window will appear.
14. Open the App folder.
15. Open the Origami Visual Studio Solution.
16. Using the top toolbar in Visual Studio, change the target from Debug to Release and from ARM to X86.
    * Click on the arrow next to the Device button, and select HoloLens Emulator.
    * Click Debug -> Start Without debugging or press Ctrl + F5.
    * After some time the emulator will start with the Origami project. When first launching the emulator, it can take as long as 15 minutes for the emulator to start up. Once it starts, do not close it.

```
Give the example
```

## Built With

* [HoloToolkit-Unity](https://github.com/Microsoft/HoloToolkit-Unity "HoloToolkit-Unity")

## Authors

* **Martin Stengård** - *Coding, design, etc.*
* **Hanne Nielsén** - *Project Leading*

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone who's code was used and to everyone who helped.
