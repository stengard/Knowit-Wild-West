# Knowit Wild West

This demo was made for HoloLens using Unity and [HoloToolkit-Unity](https://github.com/Microsoft/HoloToolkit-Unity "HoloToolkit-Unity"). It is a demo using spatial mapping to place objects in the world, spatial audio and hand gestures and voice commands to interact with the objects.

The application was made for a exhibition to show of HoloLens and some of it's capabilities. The was 'Wild West', hence the cowboys and rattle snakes. The app was mad quite simple so people would be able to interact with it quickly.

## Getting Started
First, download the repo.

```
git clone https://github.com/stengard/Knowit-Wild-West.git
```
### Prerequisites

1. [HoloLens](https://www.microsoft.com/microsoft-hololens/en-us "HoloLense") or [HoloLens Emulator](https://developer.microsoft.com/sv-se/windows/holographic/install_the_tools "HoloLense Emulator")
2. [Unity HoloLens Technical Preview](https://unity3d.com/partners/microsoft/hololens "Unity HoloLense Technical Preview")
3. Visual Studio 2015

### Installing

1. If you're using a HoloLens. Skip this step. Otherwise, install the Emulator.
2. install Unity HoloLens Technical Preview.

Open the project in Unity. You can use unity's Game mode or export to Visual Studio and build to the HoloLens or HoloLens Emulator.

#### Export the project from Unity to Visual Studio

Follow these steps and you should be ready to go

1. In Unity select File > Build Settings.
2. Select Windows Store in the Platform list and click Switch Platform.
3. Set SDK to Universal 10 and Build Type to D3D.
4. Check Unity C# Projects.
5. Click Add Open Scenes to add the scene.
6. Click Player Settings....
7. In the Inspector Panel select the Windows Store logo. Then select Publishing Settings.
8. In the Capabilities section, select the Microphone and Spatial Perception capabilities.
9. Back in the Build Settings window, click Build.
10. Create a New Folder named "App".
11. Single click the App Folder.
12. Press Select Folder.
13. When Unity is done, a File Explorer window will appear.
14. Open the App folder.
15. Open the Knowit Wild West Visual Studio Solution.
16. Using the top toolbar in Visual Studio, change the target from Debug to Release and from ARM to X86.
    * Click on the arrow next to the Device button, and select the appropriate setting.
    * If not debugging. Change from Debug to Release (improves performance a lot).
    * Click Debug -> Start Without debugging or press Ctrl + F5.
    * After some time the app will start on the device.



## Using the app

Following information states which gestures are available and what can be done in the application. The first 15 seconds of the app, it will scan the environment for walls, ceiling and floor and create planes from it. Try to look around so we get a good mesh of the room. You will then be able to place stuff in the room.

### Gestures

#### Menu
The menu can be toggled on and off by tapping on the hat. The hat can be moved around and be placed on walls - the menu will
follow it around.

##### Spawn objects
You can select different categories (currently 1,2 and actors) from the menu. The object will be spawned on the location of the
brown "stone". The stone can be placed anywhere on the floor to change the spawn location.

```
Example: Tap TNT box button in the menu to spawn a TNT Box.
```
##### Resize objects

The spawned objects can be resized by tapping on '-' or '+' at the bottom of the menu. The number between the buttons shows the multiplier used to scale to objects form their original size.


#### Action
The initialize an action on an object the tap gesture is used. The action is different for each object and some objects cant't to enything expect form being moved.

```
Example: To blow up a the TNT. First look at it and then make the tap gesture with your fingers.
Example: To change radio station. First look at it and then make the tap gesture with your fingers.

etc...
```

#### Move Objects
To move an object the hold gesture is used.
```
Example: To move the radio to another place, first look at it and then make the tap and hold gesture with your fingers.
Then you can move it around with your gaze; where you look is where the object is placed. A greenish box around the
object will appear when it's being moved. The green color indicats that there is enough room for the object to be
placed there. If the color of the box is red it's not possible to place it in the current location. IF The hold
gesture is released during an invalid placement, the object will go back to its' previous position.
```
##### Remove objects
In the menu, a trashcan can be spawned under category 2. Place it anywhere on the floor and then move any object to it by using the hold gesture. Release the hold gesture when the object is in the trashcan (not visible) to delete it from the scene. Its somewhat buggy though.

#### Voice Commands
TNT's can be blown up by looking at one and simply saying "Blow up".

## Built With
* [Unity HoloLens Technical Preview](https://unity3d.com/partners/microsoft/hololens "Unity HoloLense Technical Preview")
* [HoloToolkit-Unity](https://github.com/Microsoft/HoloToolkit-Unity "HoloToolkit-Unity")


## Authors

* **Martin Stengård** - *Coding, design, etc.*

