ShadowHumanoid
==============
The goal of this project is to create a humanoid robot that can follow a humans movements; thus shadowing a human. 
I was inspired by the movie Real Steel to make this project. This is built upon the skeleton example provided by the kinect SDK for windows. 
What this program does-

1) It gets the skeleton data from the kinect using the kinect SDK. 

2) Finds the angles between the joints of the skeleton using position data and drawing vectors. using basic maths we find the angles between these vectors.

3) These angles are sent to the respective motors on the humanoid robot, this is done using the dynamixel library provided by ROBOTIS to communicate with the CM-530 microcontroller. 


For details please refer to the program. 
