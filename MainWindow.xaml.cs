

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    //Can remove drawing the seleton to optimise the program or run it on multiple threads
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using ROBOTIS;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media.Media3D;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static void Initialise()
        {
            if (dynamixel.dxl_initialize(8, 1) == 0)
            {

                Console.WriteLine("Check Bioloid connection");

            }

            else
            {

                // Succeeded to open a connection, continue
                Console.WriteLine("Check Bioloid connection");

            }
        }
        static void PrintErrorCode()
        {

            if (dynamixel.dxl_get_rxpacket_error(dynamixel.ERRBIT_VOLTAGE) == 1)

                Console.WriteLine("Input voltage error!");

            if (dynamixel.dxl_get_rxpacket_error(dynamixel.ERRBIT_ANGLE) == 1)

                Console.WriteLine("Angle limit error!");

            if (dynamixel.dxl_get_rxpacket_error(dynamixel.ERRBIT_OVERHEAT) == 1)

                Console.WriteLine("Overheat error!");

            if (dynamixel.dxl_get_rxpacket_error(dynamixel.ERRBIT_RANGE) == 1)

                Console.WriteLine("Out of range error!");

            if (dynamixel.dxl_get_rxpacket_error(dynamixel.ERRBIT_CHECKSUM) == 1)

                Console.WriteLine("Checksum error!");

            if (dynamixel.dxl_get_rxpacket_error(dynamixel.ERRBIT_OVERLOAD) == 1)

                Console.WriteLine("Overload error!");

            if (dynamixel.dxl_get_rxpacket_error(dynamixel.ERRBIT_INSTRUCTION) == 1)

                Console.WriteLine("Instruction code error!");

        }

    

        // These are the vectors, which we would calculate from the 3D positions
        System.Windows.Media.Media3D.Vector3D shoulder_right1;
        System.Windows.Media.Media3D.Vector3D shoulder_right2;
        System.Windows.Media.Media3D.Vector3D shoulder_left1;
        System.Windows.Media.Media3D.Vector3D shoulder_left2;
        System.Windows.Media.Media3D.Vector3D spine;
        System.Windows.Media.Media3D.Vector3D hip_center;
        System.Windows.Media.Media3D.Vector3D hip_right1;
        System.Windows.Media.Media3D.Vector3D hip_right3;
        System.Windows.Media.Media3D.Vector3D hip_left1;
        System.Windows.Media.Media3D.Vector3D hip_left3;
        System.Windows.Media.Media3D.Vector3D knee_left;
        System.Windows.Media.Media3D.Vector3D knee_right;
        System.Windows.Media.Media3D.Vector3D ankel_left;
        System.Windows.Media.Media3D.Vector3D ankel_right;
        System.Windows.Media.Media3D.Vector3D elbow_left;
        System.Windows.Media.Media3D.Vector3D elbow_right;

        //Positions [X,Y,Z]
        System.Windows.Media.Media3D.Vector3D kinectelbowleft;
        System.Windows.Media.Media3D.Vector3D kinectelbowright;
        System.Windows.Media.Media3D.Vector3D kinecthandleft;
        System.Windows.Media.Media3D.Vector3D kinecthandright;
        System.Windows.Media.Media3D.Vector3D kinectspine;
        System.Windows.Media.Media3D.Vector3D kinecthipcenter;
        System.Windows.Media.Media3D.Vector3D kinectshouldercenter;
        System.Windows.Media.Media3D.Vector3D kinectankleleft;
        System.Windows.Media.Media3D.Vector3D kinectankleright;
        System.Windows.Media.Media3D.Vector3D kinectkneeleft;
        System.Windows.Media.Media3D.Vector3D kinectkneeright;
        System.Windows.Media.Media3D.Vector3D kinectfootleft;
        System.Windows.Media.Media3D.Vector3D kinectfootright;
        System.Windows.Media.Media3D.Vector3D Kinecthead;
        System.Windows.Media.Media3D.Vector3D kinecthipleft;
        System.Windows.Media.Media3D.Vector3D kinecthipright;
        System.Windows.Media.Media3D.Vector3D kinectshoulderleft;
        System.Windows.Media.Media3D.Vector3D kinectshoulderright;
        System.Windows.Media.Media3D.Vector3D kinectwristleft;
        System.Windows.Media.Media3D.Vector3D kinectwristright;
        //Initialising to zero
        double angleshoulderleft1 = 0;
        double angleshoulderleft2 = 0;
        double angleshoulderright1 = 0;
        double angleshoulderright2 = 0;
        double anglespine1 = 0;
        double anglehipcenter1 = 0;
        double anglehipleft1 = 0;
        double anglehipleft2 = 0;
        double anglehipleft3 = 0;
        double anglehipright1 = 0;
        double anglehipright2 = 0;
        double anglehipright3 = 0;
        double anglekneeleft = 0;
        double anglekneeright = 0;
        double angleankleleft = 0;
        double angleankleright = 0;
        double angleelbowleft = 0;
        double angleelbowright = 0;
        Int32 Byteangleshoulderleft1 = 0;
        Int32 Byteangleshoulderleft2 = 0;
        Int32 Byteangleshoulderright1 = 0;
        Int32 Byteangleshoulderright2 = 0;
        Int32 Byteanglespine1 = 0;
        Int32 Byteanglehipcenter1 = 0;
        Int32 Byteanglehipleft1 = 0;
        Int32 Byteanglehipleft2 = 0;
        Int32 Byteanglehipleft3 = 0;
        Int32 Byteanglehipright1 = 0;
        Int32 Byteanglehipright2 = 0;
        Int32 Byteanglehipright3 = 0;
        Int32 Byteanglekneeleft = 0;
        Int32 Byteanglekneeright = 0;
        Int32 Byteangleankleleft = 0;
        Int32 Byteangleankleright = 0;
        Int32 Byteangleelbowleft = 0;
        Int32 Byteangleelbowright = 0;

        

        /// <summary>
        /// Width of output drawing
        /// </summary>
        private const float RenderWidth = 640.0f;

        /// <summary>
        /// Height of our output drawing
        /// </summary>
        private const float RenderHeight = 480.0f;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of body center ellipse
        /// </summary>
        private const double BodyCenterThickness = 10;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Brush used to draw skeleton center point
        /// </summary>
        private readonly Brush centerPointBrush = Brushes.Blue;

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently tracked
        /// </summary>
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);

        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Drawing group for skeleton rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping skeleton data
        /// </summary>
        /// <param name="skeleton">skeleton to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private static void RenderClippedEdges(Skeleton skeleton, DrawingContext drawingContext)
        {
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, RenderHeight - ClipBoundsThickness, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, RenderWidth, ClipBoundsThickness));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, RenderHeight));
            }

            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(RenderWidth - ClipBoundsThickness, 0, ClipBoundsThickness, RenderHeight));
            }
        }

        /// <summary>
        /// Execute startup tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // Display the drawing using our image control
            Image.Source = this.imageSource;

            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit (See components in Toolkit Browser).
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

            if (null == this.sensor)
            {
                this.statusBarText.Text = Properties.Resources.NoKinectReady;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {
                        RenderClippedEdges(skel, dc);

                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.DrawBonesAndJoints(skel, dc);
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                            this.centerPointBrush,
                            null,
                            this.SkeletonPointToScreen(skel.Position),
                            BodyCenterThickness,
                            BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }

        /// <summary>
        /// Draws a skeleton's bones and joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);
 
            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;                    
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;                    
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
                 if (joint.JointType == JointType.HandLeft )

                        {
                            kinecthandleft = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                        }
                if (joint.JointType == JointType.HandRight)
                {
             
                    kinecthandright = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.ShoulderLeft)
                {
                    kinectshoulderleft = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);

                }
                if (joint.JointType == JointType.ShoulderRight)
                {
                    kinectshoulderright = new Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.Spine)
                {
                    kinectspine = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.HipCenter)
                {
                    kinecthipcenter = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.ShoulderCenter)
                {
                    kinectshouldercenter = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.AnkleLeft)
                {
                    kinectankleleft = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.AnkleRight)
                {
                    kinectankleright = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.KneeLeft)
                {
                    kinectkneeleft = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.KneeRight)
                {
                    kinectkneeright = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }

                if (joint.JointType == JointType.FootLeft)
                {
                    kinectfootleft = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.FootRight)
                {
                    kinectfootright = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.Head)
                {
                    Kinecthead = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.HipLeft)
                {
                    kinecthipleft = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.HipRight)
                {
                    kinecthipright = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.WristLeft)
                {
                    kinectwristleft = new System.Windows.Media.Media3D.Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);

                }
                if (joint.JointType == JointType.WristRight)
                {
                   kinectwristright = new Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.ElbowLeft)
                {
                   kinectelbowleft = new Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                }
                if (joint.JointType == JointType.ElbowRight)
                {
                    kinectelbowright = new Vector3D(joint.Position.X, joint.Position.Y, joint.Position.Z);
                     
                }

              }
            
            
            shoulder_left1 = kinectshoulderleft -kinectelbowleft;
            shoulder_right1 = kinectshoulderright - kinectelbowright;
            // added elbow tracking to 2FAAST
            elbow_left = kinectelbowleft - kinectwristleft;
            elbow_right = kinectelbowleft - kinectwristright;
            spine = kinectshoulderleft - kinectshoulderright; // just as the x axis
            hip_center = kinecthipcenter - Kinecthead;// 
            hip_right1 = kinecthipright - kinectkneeright;
            hip_left1 = kinecthipleft - kinectkneeleft;
            hip_left3 = kinectankleleft - kinectfootleft;
            hip_right3 = kinectankleright - kinectfootright;
            knee_left = kinectankleleft - kinectkneeleft;
            //ankel_left = kinectankleleft - kinectfootleft;
            //ankel_right = kinectankleright - kinectfootright;
            knee_right = kinectankleright - kinectkneeright;
            
            
            /*define new axis system so that the axises are considered relative to the body of the person and not the global axis*/
            Vector3D newx = kinectshoulderleft - kinectshoulderright;
            Vector3D newy = kinectshouldercenter - kinecthipcenter;
            Vector3D newz = System.Windows.Media.Media3D.Vector3D.CrossProduct(newy, newx);
            angleshoulderleft1 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newx, shoulder_left1);
            Console.WriteLine("Shoulder Left:{0}", angleshoulderleft1);
            double Percent1 = (angleshoulderleft1 / 300) * 100;
            double Byte1 = ((Percent1 / 100) * 1024);
            Byteangleshoulderleft1 = Convert.ToInt32(Byte1);
            //now Byteangleshoulder will be assigned to the left shoulder motor with ID 18.
            angleshoulderleft2 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, shoulder_left1);
            Console.WriteLine("ShouderZ Left:", angleshoulderleft2);
            double Percent8 = (angleshoulderleft1 / 300) * 100;
            double Byte8 = ((Percent8 / 100) * 1024);
            Byteangleshoulderright1 = Convert.ToInt32(Byte8);
            angleshoulderleft2 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newx, shoulder_right1);
            Console.WriteLine("Shoulder Right:", angleshoulderright1);
            double Percent2 = (angleshoulderleft1 / 300) * 100;
            double Byte2 = ((Percent2 / 100) * 1024);
            Byteangleshoulderright1 = Convert.ToInt32(Byte2);
            angleelbowleft = System.Windows.Media.Media3D.Vector3D.AngleBetween(shoulder_left1, elbow_left);
            double Percent3 = (angleshoulderleft1 / 300) * 100;
            double Byte3 = ((Percent3 / 100) * 1024);
            Byteangleelbowleft = Convert.ToInt32(Byte3);
            Console.WriteLine("Elbow Left:{0}", angleelbowleft);
            angleelbowright = System.Windows.Media.Media3D.Vector3D.AngleBetween(shoulder_right1, elbow_right);
            double Percent4 = (angleshoulderleft1 / 300) * 100;
            double Byte4 = ((Percent4 / 100) * 1024);
            Byteangleelbowright = Convert.ToInt32(Byte4);
            Console.WriteLine("Elbow Right:{0}", angleelbowright);
            angleshoulderright2 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, shoulder_right1);
            double Percent5 = (angleshoulderright2 / 300) * 100;
            double Byte5 = ((Percent5 / 100) * 1024);
            Byteangleshoulderright2 = Convert.ToInt32(Byte5);
            //anglespine1 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newx, spine);
            anglehipcenter1 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newy, hip_center);
            anglehipleft1 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newx, hip_left1);
            anglehipright1 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newx, hip_right1);
            anglehipleft2 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, hip_left1);
            anglehipright2 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, hip_right1);
            anglehipleft3 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newx, hip_left3);
            anglehipright3 = System.Windows.Media.Media3D.Vector3D.AngleBetween(newx, hip_right3);
            anglekneeleft = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, knee_left);
            Console.WriteLine("Knee left:{0}", anglekneeleft);
            double Percent6 = (angleshoulderleft1 / 300) * 100;
            double Byte6 = ((Percent6 / 100) * 1024);
            Byteanglekneeleft = Convert.ToInt32(Byte6);
            anglekneeright = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, knee_right);
            Console.WriteLine("Knee Right:{0}", anglekneeright);
            double Percent7 = (angleshoulderleft1 / 300) * 100;
            double Byte7 = ((Percent7 / 100) * 1024);
            Byteanglekneeright = Convert.ToInt32(Byte7);
            angleankleleft = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, ankel_left);
            angleankleright = System.Windows.Media.Media3D.Vector3D.AngleBetween(newz, ankel_right);
            // Calibrartion angles for robot----Important 
            dynamixel.dxl_write_word(14, Byteangleshoulderleft1, 30);// Motor ID = 18 the left shoulder of the robot 
            dynamixel.dxl_write_word(3, Byteangleshoulderright1, 30);//and 30 refers to the address to write the goal position(Byteangle) on.
            dynamixel.dxl_write_word(11, Byteangleelbowleft, 30);
            dynamixel.dxl_write_word(12, Byteangleelbowright, 30);
            dynamixel.dxl_write_word(13, Byteanglekneeleft, 30);
            dynamixel.dxl_write_word(15,579 , 30);// Byteanglekneeright
            dynamixel.dxl_write_word(14, Byteangleshoulderleft2, 30);
            dynamixel.dxl_write_word(17, Byteangleshoulderright2, 30);
         }  

            
            

            
           
            
           
           
            

        


        //Takes percent of angles X/300 *100. Take the percentage lets say Y, Y/100 * 1024= P. Then send P bytes to control
        //the dynamixel servos. 
        
       
            
         
            

            




        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        /// <summary>
        /// Draws a bone line between two joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw bones from</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="jointType0">joint to start drawing from</param>
        /// <param name="jointType1">joint to end drawing at</param>
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }

        /// <summary>
        /// Handles the checking or unchecking of the seated mode combo box
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void CheckBoxSeatedModeChanged(object sender, RoutedEventArgs e)
        {
            if (null != this.sensor)
            {
                if (this.checkBoxSeatedMode.IsChecked.GetValueOrDefault())
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    //make the robot sit
                }
                else
                {
                    this.sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Default;
                    // calibrate pose and make the robot stand 
                }
            }
        }
    }
}