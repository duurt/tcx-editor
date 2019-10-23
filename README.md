# Hello, and Welcome to the TCX Editor project!
## Why this TCX Editor?
I wanted to be able to have turn-by-turn navigation instructions on my Wahoo Elemnt Bolt (a bike computer) for routes that are created with the Strava route builder. This is not supported by default, but this app allows you to add the navigation cues, or any other points of interest on the route yourself.

## Getting started
Go to [https://github.com/duurt/tcx-editor/releases/tag/v1.0.0](https://github.com/duurt/tcx-editor/releases/tag/v1.0.0) and download the `.zip`. Extract it anywhere you like and run the file `TcxEditor.UI.exe`.

## How does it work?
In short, here are the steps to follow:
1.  Create a route with the Strava route builder and export it as a `.tcx` file
2.  Add the navigation cues to the route with the TCX Editor
3.  Save the file and upload it to the Wahoo Elemnt Bolt...

Enjoy the ride!

# User Manual
## Set your geographic location
Open the file `TcxEditor.UI.exe.config` and edit the line 
`<add key="Location" value="Groningen, Netherlands"/>` 
Make sure you preserve the double quotes surrounding your location!

## Open a route
Click `Open route file...` button, select a `.tcx` and open it.

## Move and zoom the map
To move the map, drag it with the *right* mouse button pressed.

To zoom in and out, simply scroll with the mouse wheel or touchpad. Note that the position of the mouse pointer is ignored: the center of the map remains te the center, unless you drag it.

## Add navigation cues
At every point of the route where you need directions, for instance "turn left", you can add them in the following way:

1. Click on the map near the position you want to add a cue to; a blue pinpoint will appear.
2. Press one of the arrow keys to add a que: LEFT for left, RIGHT for right, UP for straight.
   OR
   Choose a Type and write a custom Description. Finally, click `Add`.

## Update a navigation cue
This is not yeet supported. However, you can simply remove a point and add it again.

## Remove a navigation cue
Click on a point you want to remove, and click the `Remove point`.

## Save the result
Click `Save Route`

## Dealing with overlapping route segments
If the route passes a certain section more than once, you cannot tell whether you have selected the correct point. You can work around this by going to a point in the route, before or after the overlapping part, and move the pinpoint with the `Route scrolling` buttons.