ShadowHumanoid
==============
The goal of this project is to create a humanoid robot that can follow a humans movements; thus shadowing a human. 
I was inspired by the movie Real Steel to make this project. It takes motion skeletal data from the kinect sensor and converts it to vectors then gets the angle betweek these vectors. . This project was made during my internship at EduKinect and I wrote it in C#, which I also learnt during thus internship (2013 summer).
What this program does-

1) It gets the skeleton data from the kinect using the kinect SDK. 

2) Finds the angles between the joints of the skeleton using position data and drawing vectors. Using vector maths we find the angles between these vectors.

3) These angles are sent to the respective motors on the humanoid robot (with dynamixel motors), through the CM-530 microcontroller. 

Requires Kinect SDK And Dynamixel and prefers the Visual Studio IDE.

TODO:

Update for universal winodws platform app.

