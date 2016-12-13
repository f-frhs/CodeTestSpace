// collectDataFroXmlFiles.cs <= mgetFileName.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Office.Interop.Excel;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

class Program
{
    private static List<Tuple<string, string, string, double>> list;

    static void Main(string[] args)
    {
        double CHY1_X = 0; 
        double CHY1_Y = 0; 
        double CHY1_Z = 0; 
        double CHY1_I = 0; 
        double CHY1_J = 0; 
        double CHY1_K = 0; 
        double CHY1_D = 0; 
        double FHY1_X = 0; 
        double FHY1_Y = 0; 
        double FHY1_Z = 0; 
        double FHY1_I = 0; 
        double FHY1_J = 0; 
        double FHY1_K = 0; 
        double FHY1_D = 0; 
        double CHY2_X = 0; 
        double CHY2_Y = 0; 
        double CHY2_Z = 0; 
        double CHY2_I = 0; 
        double CHY2_J = 0; 
        double CHY2_K = 0; 
        double CHY2_D = 0; 
        double FHY2_X = 0; 
        double FHY2_Y = 0; 
        double FHY2_Z = 0; 
        double FHY2_I = 0; 
        double FHY2_J = 0; 
        double FHY2_K = 0; 
        double FHY2_D = 0; 
        double CHY3_X = 0; 
        double CHY3_Y = 0; 
        double CHY3_Z = 0; 
        double CHY3_I = 0; 
        double CHY3_J = 0; 
        double CHY3_K = 0; 
        double CHY3_D = 0; 
        double FHY3_X = 0; 
        double FHY3_Y = 0; 
        double FHY3_Z = 0; 
        double FHY3_I = 0; 
        double FHY3_J = 0; 
        double FHY3_K = 0; 
        double FHY3_D = 0; 
        double HNY1_X = 0; 
        double HNY1_Y = 0; 
        double HNY1_Z = 0; 
        double HNY1_I = 0; 
        double HNY1_J = 0; 
        double HNY1_K = 0; 
        double HNY1_D = 0; 
        double HNY2_X = 0; 
        double HNY2_Y = 0; 
        double HNY2_Z = 0; 
        double HNY2_I = 0; 
        double HNY2_J = 0; 
        double HNY2_K = 0; 
        double HNY2_D = 0;        

        double CHN1_X = 0; 
        double CHN1_Y = 0; 
        double CHN1_Z = 0; 
        double CHN1_I = 0; 
        double CHN1_J = 0; 
        double CHN1_K = 0; 
        double CHN1_D = 0; 
        double FHN1_X = 0; 
        double FHN1_Y = 0; 
        double FHN1_Z = 0; 
        double FHN1_I = 0; 
        double FHN1_J = 0; 
        double FHN1_K = 0; 
        double FHN1_D = 0; 
        double CHN2_X = 0; 
        double CHN2_Y = 0; 
        double CHN2_Z = 0; 
        double CHN2_I = 0; 
        double CHN2_J = 0; 
        double CHN2_K = 0; 
        double CHN2_D = 0; 
        double FHN2_X = 0; 
        double FHN2_Y = 0; 
        double FHN2_Z = 0; 
        double FHN2_I = 0; 
        double FHN2_J = 0; 
        double FHN2_K = 0; 
        double FHN2_D = 0; 
        double CHN3_X = 0; 
        double CHN3_Y = 0; 
        double CHN3_Z = 0; 
        double CHN3_I = 0; 
        double CHN3_J = 0; 
        double CHN3_K = 0; 
        double CHN3_D = 0; 
        double FHN3_X = 0; 
        double FHN3_Y = 0; 
        double FHN3_Z = 0; 
        double FHN3_I = 0; 
        double FHN3_J = 0; 
        double FHN3_K = 0; 
        double FHN3_D = 0; 
        double HNN1_X = 0; 
        double HNN1_Y = 0; 
        double HNN1_Z = 0; 
        double HNN1_I = 0; 
        double HNN1_J = 0; 
        double HNN1_K = 0; 
        double HNN1_D = 0; 
        double HNN2_X = 0; 
        double HNN2_Y = 0; 
        double HNN2_Z = 0; 
        double HNN2_I = 0; 
        double HNN2_J = 0; 
        double HNN2_K = 0; 
        double HNN2_D = 0;    

        //var lengthX1_2 = new List<Tuple<double, double>>(
        var lengthX1_2 = new List<double>();
        var lengthX1_3 = new List<Tuple<double, double>>();
        var lengthX2_3 = new List<Tuple<double, double>>();

        foreach (string file in GetFiles(@"C:\Users\hayashi\Desktop\csvtesrt"))
        {
            XmlRead(file);
            DirectoryInfo dirInfo = Directory.GetParent(file);

            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "X")) { CHY1_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Y")) { CHY1_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Z")) { CHY1_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Orientation I")) { CHY1_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Orientation J")) { CHY1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Orientation K")) { CHY1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1").FindAll(c => c.Item3 == "Diameter")) { CHY1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "X")) { FHY1_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Y")) { FHY1_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Z")) { FHY1_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Orientation I")) { FHY1_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Orientation J")) { FHY1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Orientation K")) { FHY1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1").FindAll(c => c.Item3 == "Diameter")) { FHY1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "X")) { CHY2_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Y")) { CHY2_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Z")) { CHY2_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Orientation I")) { CHY2_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Orientation J")) { CHY2_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Orientation K")) { CHY2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2").FindAll(c => c.Item3 == "Diameter")) { CHY2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "X")) { FHY2_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Y")) { FHY2_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Z")) { FHY2_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Orientation I")) { FHY2_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Orientation J")) { FHY2_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Orientation K")) { FHY2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2").FindAll(c => c.Item3 == "Diameter")) { FHY2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "X")) { CHY3_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Y")) { CHY3_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Z")) { CHY3_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Orientation I")) { CHY3_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Orientation J")) { CHY3_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Orientation K")) { CHY3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3").FindAll(c => c.Item3 == "Diameter")) { CHY3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "X")) { FHY3_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Y")) { FHY3_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Z")) { FHY3_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Orientation I")) { FHY3_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Orientation J")) { FHY3_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Orientation K")) { FHY3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3").FindAll(c => c.Item3 == "Diameter")) { FHY3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "X")) { HNY1_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Y")) { HNY1_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Z")) { HNY1_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Orientation I")) { HNY1_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Orientation J")) { HNY1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Orientation K")) { HNY1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1").FindAll(c => c.Item3 == "Diameter")) { HNY1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "X")) { HNY2_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Y")) { HNY2_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Z")) { HNY2_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Orientation I")) { HNY2_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Orientation J")) { HNY2_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Orientation K")) { HNY2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2").FindAll(c => c.Item3 == "Diameter")) { HNY2_K = (l.Item4); };

            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "X")) { CHN1_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Y")) { CHN1_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Z")) { CHN1_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Orientation I")) { CHN1_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Orientation J")) { CHN1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Orientation K")) { CHN1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole1_No").FindAll(c => c.Item3 == "Diameter")) { CHN1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "X")) { FHN1_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Y")) { FHN1_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Z")) { FHN1_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Orientation I")) { FHN1_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Orientation J")) { FHN1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Orientation K")) { FHN1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole1_No").FindAll(c => c.Item3 == "Diameter")) { FHN1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "X")) { CHN2_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Y")) { CHN2_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Z")) { CHN2_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Orientation I")) { CHN2_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Orientation J")) { CHN2_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Orientation K")) { CHN2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole2_No").FindAll(c => c.Item3 == "Diameter")) { CHN2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "X")) { FHN2_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Y")) { FHN2_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Z")) { FHN2_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Orientation I")) { FHN2_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Orientation J")) { FHN2_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Orientation K")) { FHN2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole2_No").FindAll(c => c.Item3 == "Diameter")) { FHN2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "X")) { CHN3_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Y")) { CHN3_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Z")) { CHN3_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Orientation I")) { CHN3_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Orientation J")) { CHN3_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Orientation K")) { CHN3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "CubeHole3_No").FindAll(c => c.Item3 == "Diameter")) { CHN3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "X")) { FHN3_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Y")) { FHN3_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Z")) { FHN3_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Orientation I")) { FHN3_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Orientation J")) { FHN3_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Orientation K")) { FHN3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "FrangeHole3_No").FindAll(c => c.Item3 == "Diameter")) { FHN3_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "X")) { HNN1_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Y")) { HNN1_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Z")) { HNN1_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Orientation I")) { HNN1_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Orientation J")) { HNN1_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Orientation K")) { HNN1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut1_No").FindAll(c => c.Item3 == "Diameter")) { HNN1_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "X")) { HNN2_X = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Y")) { HNN2_Y = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Z")) { HNN2_Z = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Orientation I")) { HNN2_I = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Orientation J")) { HNN2_J = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Orientation K")) { HNN2_K = (l.Item4); };
            foreach (var l in list.FindAll(c => c.Item2 == "HoleNut2_No").FindAll(c => c.Item3 == "Diameter")) { HNN2_K = (l.Item4); };

            var resoltCHX1_2 = CHY1_X - CHY2_X;
            var resoltCHX1_3 = CHY1_X - CHY3_X;
            var resoltCHX2_3 = CHY2_X - CHY3_X;
            var resoltCHY1_2 = CHY1_Y - CHY2_Y;
            var resoltCHY1_3 = CHY1_Y - CHY3_Y;
            var resoltCHY2_3 = CHY2_Y - CHY3_Y;
            var resoltCHZ1_2 = CHY1_Z - CHY2_Z;
            var resoltCHZ1_3 = CHY1_Z - CHY3_Z;
            var resoltCHZ2_3 = CHY2_Z - CHY3_Z;     

            var resoletCH1_2 = CalcSquare(resoltCHX1_2, resoltCHY1_2, resoltCHZ1_2);
            var resoletCH1_3 = CalcSquare(resoltCHX1_3, resoltCHY1_3, resoltCHZ1_3);
            var resoletCH2_3 = CalcSquare(resoltCHX2_3, resoltCHY2_3, resoltCHZ2_3);

            lengthX1_2.Add(resoletCH1_2);

        }

        Console.WriteLine("----------結果---------------");
        foreach (var item in lengthX1_2)
        {
            Console.WriteLine($"{item}");
        }
        foreach (var item in lengthX1_3)
        {
            Console.WriteLine($"{item}");
        }
        foreach (var item in lengthX2_3)
        {
            Console.WriteLine($"{item}");
        }

        Console.ReadLine();

    }

    //ToDo: list全体をエクセルに吐き出す

    //ToDo: Listから任意の要素を抜き出す部分の関数化

    //引数の総和の平方根を返す
    private static double CalcSquare(double x, double y, double z)
    {
        double length3D = Math.Sqrt(x * x + y * y + z * z );
        return length3D;
    }

    // List<doubel>を受取り、その平均と標準偏差計算を返す
    private static dynamic CalcSD(double[] x, double[] y, double[] z)
    {
        //距離： x y z の和の平方根
        double[] length3D = (((x.Zip(y, (a, b) => (a + b)).ToArray()).Zip(z, (a, b) => (a + b))).Select(i => Math.Sqrt(i)).ToArray());

        //距離： 平均
        Double l_Avg = length3D.Average();

        //σの二乗×データ数
        Double l_calcSD = 0;
        foreach (Double data in length3D)
        {
            l_calcSD += (data - l_Avg) * (data - l_Avg);
        }

        //σを算出して返却
        var l_SD = Math.Sqrt(l_calcSD / (length3D.Length - 1));

        return new
        {
            avg = l_Avg,
            sd = l_SD
        };
    }

    //xmlのパス受取り、Absoluteの値をListに書き出す
    private static void XmlRead(string file)
    {
        using (StreamReader reader = new StreamReader(file))
        {
            DirectoryInfo dirInfo = Directory.GetParent(file);


            XmlSerializer serializer = new XmlSerializer(typeof(dot));
            var value = (dot)serializer.Deserialize(reader);
            list = new List<Tuple<string, string, string, double>>();

            foreach (inspectionpoint IP in value.Inspectionpoint)
            {
                foreach (parttype PT in value.Parttype)
                    foreach (characteristic CH in IP.Characteristic)
                    {
                        foreach (measurement ME in CH.Measurement)
                        {
                            list.Add(Tuple.Create(dirInfo.Name, IP.Name, CH.Defaultname, double.Parse(ME.Absolute)));
                        }
                    }
            }
        }
    }

    //指定階層下のフォルダにあるxmlのパスを返す
    static IEnumerable<string> GetFiles(string path)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(path);
        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            try
            {
                foreach (string subDir in Directory.GetDirectories(path))
                {
                    queue.Enqueue(subDir);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            string[] files = null;
            try
            {
                files = Directory.GetFiles(path, "*.xml");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    yield return files[i];
                }
            }
        }
    }
}

//以下xmlのデシリアライズ
[XmlRootAttribute(Namespace = "", IsNullable = false)]
public class dot
{
    //[XmlElement("cell")]
    //public cell[] Cell { get; set; }

    [XmlElement("parttype")]
    public parttype[] Parttype { get; set; }

    [XmlElement("cycle")]
    public cycle[] Cycle { get; set; }

    public string STORED { get; set; }

    [XmlElement("inspectionpoint")]
    public inspectionpoint[] Inspectionpoint { get; set; }
}

//[XmlRoot(Namespace = "", IsNullable = false)]
//public class cell
//{
//    [XmlElement("name")]
//    public string Name { get; set; }
//}

[XmlRoot(Namespace = "", IsNullable = false)]
public class parttype
{
    [XmlElement("name")]
    public string Name { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class cycle
{
    [XmlElement("date")]
    public date[] Date { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class date
{
    [XmlElement("month")]
    public string Month { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class inspectionpoint
{
    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("characteristic")]
    public characteristic[] Characteristic { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class characteristic
{
    [XmlElement("defaultname")]
    public string Defaultname { get; set; }

    [XmlElement("measurement")]
    public measurement[] Measurement { get; set; }
}

[XmlRoot(Namespace = "", IsNullable = false)]
public class measurement
{
    [XmlElement("absolute")]
    public string Absolute { get; set; }
}
