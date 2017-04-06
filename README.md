# Logitech Bridge SDK

Draft version 0.1

Last update April 4th

Contact: bridgesdk@logitech.com

## Introduction:
The Bridge SDK is a Development kit that aims at helping app makers and other SW developers to solve some of the issues arising whenever a person needs to use a Keyboard in VR.

## Motivation:
The motivation comes from the belief that in some situations the user still needs to use a keyboard to interact with the applications, especially in productivity-driven scenarios but also in games, chat and content browsing. We believe that that keyboard has to be physically present, since it delivers the traditional tactile experience and feedback that people value.


## The Pieces:
The Bridge SDK is composed of the following elements:

- A Logitech G810 Keyboard (off-the-shelf)
- A Logitech “BRIDGE adapter”
- A HTC tracker 
- A SW installer that enables an “overlay” of a 3D VR keyboard


## Setup instructions:

### Bridge adapter
Follow the leaflet contained in the “bridge” box. You simply use the bridge embedded mount to attach the Vive tracker to the mount. Approach it to the top-left corner of the keyboard. Be sure to first align the left side of it then pull the top towards the keyboard and make sure is it well secured.


### Overlay 3D VR package
From this repository, clone or download the full content into your preferred folder. Head to the installer folder and follow the instructions there.

Double-click on the installer for the package, and it will install a custom package in this folder (TBC). The overlay will be running as a service with a Console windows for information and debug output.

### Pairing
First pair the tracker as per HTC instructions (http://link.vive.com/tracker/guideline). It should then appear as below in SteamVR.

 

In this very first version, our Keyboard overlay will scan the list of active trackers and use the FIRST ONE (make sure to have only one turned on when you launch the system).


## Description of use:

### Overlay 3d VR Keyboard

The overlay package is fully compatible with all application that is developed based on STEAM VR (©Valve).

It is the SW piece that supports the Bridge SDK and allows the user to visualize/overlay a view of the virtual representation of the keyboard in any VR application game: It acts in fact as an additional “virtual” Headset that has his own view that is virtually placed right in front of the user’s HMD’s view.

It will render a 3D representation of a G810 keyboard, complete with animations when the keys are pressed.
